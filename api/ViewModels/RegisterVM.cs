using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string password { get; set; }
        public int University_id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
    public enum Degree
    {
        D3,
        D4,
        S1,
        S2,
        S3
    }
}
