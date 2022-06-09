using api.Context;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        public MyContext context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int SignManager(SignManagerVM signManager)
        {
            
            AccountRole account = new AccountRole
            {
                NIK = signManager.NIK,
                Role_id = 2
            };

            context.AccountRoles.Add(account);
            var result = context.SaveChanges();
            return result;
        }

      
    }
}
