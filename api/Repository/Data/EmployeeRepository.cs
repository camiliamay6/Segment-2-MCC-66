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
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this._Context = myContext;
        }
        private readonly MyContext _Context;

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public bool CheckEmailExist(string email)
        {
            var result = (from e in _Context.Employees
                            where e.Email == email
                            select e).FirstOrDefault<Employee>();
            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPhoneExist(string phone)
        {
            var result = (from e in _Context.Employees
                          where e.Phone == phone
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
        public int Register(RegisterVM register)
        {
            if (CheckEmailExist(register.Email))
            {
                return -1;
            } else if (CheckPhoneExist(register.Phone))
            {
                return -2;
            } 
            else
            {
                Employee employee = new Employee();
                var count = (from s in _Context.Employees
                             orderby s.NIK
                             select s.NIK).LastOrDefault();
                int last_id;
                string new_count;
                if (count == null)
                {
                    last_id = 1;
                }
                else
                {
                    last_id = Convert.ToInt32(count.Substring(count.Length - 4)) + 1;
                }
                

                if (last_id < 10)
                {
                    new_count = "000" + last_id;
                }
                else if (last_id  < 100)
                {
                    new_count = "00" + last_id;
                }
                else if (last_id < 1000)
                {
                    new_count = "0" + last_id;
                }
                else
                {
                    new_count = last_id.ToString();
                }

                var NIK = DateTime.Now.ToString("MMddyy") + new_count;

                var password = BCrypt.Net.BCrypt.HashPassword(register.password, GetRandomSalt());
                employee.NIK = NIK;
                employee.FirstName = register.FirstName;
                employee.LastName = register.LastName;
                employee.Phone = register.Phone;
                employee.BirthDate = register.BirthDate;
                employee.Salary = register.Salary;
                employee.Email = register.Email;
                employee.Gender = (Models.Gender)Enum.Parse(typeof(Models.Gender), register.Gender);
                employee.account = new Account
                {
                    password = password,
                    Profiling = new Profiling
                    {
                        Education = new Education
                        {
                            University_Id = register.University_id,
                            Degree = (Models.Degree)Enum.Parse(typeof(Models.Degree), register.Degree),
                            GPA = register.GPA
                        }
                    }
                };

                AccountRole role = new AccountRole
                {
                    NIK = NIK,
                    Role_id = 3
                };

                _Context.Employees.Add(employee);
                _Context.AccountRoles.Add(role);
                var result = _Context.SaveChanges();
                return result;        
            } 
        }

        public IEnumerable<GetRegisteredDataVM> GetRegisteredData()
        {
            var result = _Context.Employees.ToList();
            List<GetRegisteredDataVM> listData = new List<GetRegisteredDataVM>();
            foreach(var r in result)
            {
                GetRegisteredDataVM data = new GetRegisteredDataVM();
                data.FullName = r.FirstName + " " + r.LastName;
                data.Phone = r.Phone;
                data.BirthDate = r.BirthDate;
                Gender gender = (Gender)r.Gender;
                data.Gender = gender.ToString();
                data.Salary = r.Salary;
                data.Email = r.Email;

                var education = (from e in _Context.Educations
                                 join p in _Context.Profilings
                                 on e.Id equals p.Education_ID
                                 join u in _Context.Universities
                                 on e.University_Id equals u.Id
                                 where p.NIK == r.NIK
                                 select new
                                 {
                                     e.Degree,
                                     e.GPA,
                                     u.Name
                                 }).FirstOrDefault();

                Degree degree = (Degree)education.Degree;
                data.Degree = degree.ToString();
                data.GPA = education.GPA;
                data.University_Name = education.Name;

                listData.Add(data);
            }
            return listData;
        }

        
    }
}
