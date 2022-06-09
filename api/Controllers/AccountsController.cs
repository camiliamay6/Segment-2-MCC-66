using api.Base;
using api.Models;
using api.Repository.Data;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        public IConfiguration configuration;
        public AccountRepository repository;
        public AccountsController(AccountRepository repository, IConfiguration configuration) : base(repository)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM change)
        {
            var result = repository.ChangePassword(change);
            if(result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP telah aktif!" });
            } else if(result == -2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP telah kadaluarsa!" });
            }
            else if (result == -3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP salah!" });
            }
            else if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Pengubahan password berhasil dilakukan!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Pengubahan password gagal dilakukan" });
            }
        }
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            var result = repository.ForgotPassword(forgotPassword);
            if (result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak ada!" });
            }
            else if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Kode OTP telah dikirm" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Request gagal" });
            }
        }
        [HttpPost("login")]
        public ActionResult Login(LoginVM login)
        {
            var result = repository.Login(login);
            if(result > 0)
            {
                var NIK = repository.GetNIK(login.Email);
                var cekrole = repository.GetRole(NIK);
                var claims = new List<Claim>();
                claims.Add(new Claim("Email", login.Email));
                foreach (var role in cekrole)
                {
                    claims.Add(new Claim("roles", role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("Token Security", idtoken.ToString()));
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Berhasil login", idtoken });
            }
            if (result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Password salah" });
            }

        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }
    }
}
