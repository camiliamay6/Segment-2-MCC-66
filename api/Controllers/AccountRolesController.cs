using api.Base;
using api.Models;
using api.Repository.Data;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        public AccountRoleRepository repository;
        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("signmanager")]
        public ActionResult SignManager(SignManagerVM signManager)
        {
            var result = repository.SignManager(signManager);
            if(result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Pengubahan jabatan berhasil dilakukan" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "pengubahan jabatan gagal" });
            }
        }

      
    }
}
