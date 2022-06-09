using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
