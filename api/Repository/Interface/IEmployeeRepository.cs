using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee GetById(string NIK);
        Employee GetByName(string FirstName);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);

    }
}
