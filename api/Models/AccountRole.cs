using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AccountRole
    {
        public string NIK { get; set; }
        public int Role_id { get; set; }
        public Role Role;
        public Account Account;
    }
}
