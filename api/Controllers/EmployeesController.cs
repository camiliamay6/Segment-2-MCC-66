using api.Base;
using api.Models;
using api.Repository.Data;
using api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            
            var result = employeeRepository.Register(registerVM);
            if(result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email telah digunakan" });
            } else if(result == -2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Nomor telepon telah digunakan" });
            } else if(result >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data berhasil diinsert" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data gagal diinputkan" });
            }
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpGet("getregistereddata")]
        public ActionResult GetRegisteredData()
        {
            var result = employeeRepository.GetRegisteredData();
            if(result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Pengambilan data berhasil", result });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Pengambilan data gagal"});
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }

    }
}
