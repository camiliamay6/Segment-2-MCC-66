using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [ForeignKey("Education")]
        public int Education_ID { get; set; }
        public Education Education { get; set; }
        public Account Account { get; set; }
    }
}
