using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Account
    {
        [Key]

        public string NIK { get; set; }
        public string password { get; set; }
        public int OTP { get; set; }
        public bool OTPActive { get; set; }
        public DateTime Expired_Date { get; set; }
        public Employee employee { get; set; }
        public Profiling Profiling { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
