using api.Context;
using api.Models;
using api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repository
{
    public class EmployeesRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeesRepository(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            int result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee GetById(string NIK)
        {
            // Implementasi Method SingleOrDefault()
            /*var result = (from e in context.Employees
                          where e.FirstName == FirstName
                          select e).SingleOrDefault<Employee>();
            return result;*/

            // Implementasi Method Single()
            /*            var result = (from e in context.Employees
                                      where e.FirstName == FirstName
                                      select e).Single<Employee>();
                        return result;*/

            // Implementasi Method First()
            /*var result = (from e in context.Employees
                          where e.FirstName == FirstName
                          select e).First<Employee>();
            return result;*/

            //Implementasi Method FirstOrDefault()
            /* var result = (from e in context.Employees
                           where e.FirstName == FirstName
                           select e).FirstOrDefault<Employee>();
             return result;*/

            // Implementasi Method Find()
            var result = context.Employees.Find(NIK);
            return result;
        }

        public Employee GetByName(string FirstName)
        {
            var result = (from e in context.Employees
                          where e.FirstName == FirstName
                          select e).First<Employee>();
            return result;
        }

        public int Insert(Employee employee)
        {
            context.Employees.Add(employee);
            int result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            int result = context.SaveChanges();
            return result;
        }
    }
}
