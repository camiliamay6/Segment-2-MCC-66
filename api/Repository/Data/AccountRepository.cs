using api.Context;
using api.Models;
using api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace api.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public string GetNIK(string email)
        {
            var result = (from e in myContext.Employees
                          where e.Email == email
                          select e.NIK).FirstOrDefault();
            return result;
        }

        public List<string> GetRole(string NIK)
        {
            var result = (from ar in myContext.AccountRoles
                          join r in myContext.Roles
                          on ar.Role_id equals r.Id
                          where ar.NIK == NIK
                          select r.Name).ToList();
            return result;
        }
        public bool CheckEmailExist(string email)
        {
            var result = (from e in myContext.Employees
                          where e.Email == email
                          select e).FirstOrDefault<Employee>();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
             
        }
        public int Login(LoginVM login)
        {
            if (!CheckEmailExist(login.Email))
            {
                return -1;
            }
            else
            {
                var password_hash = (from e in myContext.Employees
                                     join a in myContext.Accounts
                                     on e.NIK equals a.NIK
                                     where e.Email == login.Email
                                     select a.password).FirstOrDefault();
                var IsPasswordCorrect = BCrypt.Net.BCrypt.Verify(login.Password, password_hash);
                if (IsPasswordCorrect)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int ChangePassword(ChangePasswordVM changePassword)
        {
            Account account = (from e in myContext.Employees
                           join a in myContext.Accounts
                           on e.NIK equals a.NIK
                           where e.Email == changePassword.Email
                           select a).FirstOrDefault();

            if (account.OTPActive)
            {
                return -1;
            } else if(DateTime.Now > account.Expired_Date)
            {
                return -2;
            } else if(account.OTP != changePassword.OTP)
            {
                return -3;
            } else
            {
                account.OTPActive = true;
                account.password = BCrypt.Net.BCrypt.HashPassword(changePassword.new_password, GetRandomSalt());

                myContext.Entry(account).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                return result;
            }
        }
        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (!CheckEmailExist(forgotPasswordVM.Email))
            {
                return -1;
            }
            else
            {
                Random otp_int = new Random();
                string otp = otp_int.Next(100000, 999999).ToString();

                Account account = (from e in myContext.Employees
                                   join a in myContext.Accounts
                                   on e.NIK equals a.NIK
                                   where e.Email == forgotPasswordVM.Email
                                   select a).FirstOrDefault();

                account.OTP = Convert.ToInt32(otp);
                account.Expired_Date = DateTime.Now.AddMinutes(5);

                myContext.Entry(account).State = EntityState.Modified;
                var result = myContext.SaveChanges();

                string address_from = "admin@gmail.com";
                MailMessage message = new MailMessage(address_from, forgotPasswordVM.Email);

                message.Subject = "Test Forgot Password OTP";
                message.Body = "Here is your OTP code: " + otp;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("8cf2a450d3c981", "b0e8727923e41e"),
                    EnableSsl = true
                };

                client.Send(message);

                return result;
            }
        }
    }
}
