using ESearch.BLL.Common;
using ESearch.BLL.Interfaces;
using ESearch.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL.Services
{
    public class BaseService
    {
        protected IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        protected ExecuteResult Execute(Func<ExecuteResult> func, string errorDecription = "")
        {
            try
            {
                return func();
            }
            catch (Exception exp)
            {
                return ExecuteResult.Error(errorDecription + exp.Message);
            }
        }
    }
}
