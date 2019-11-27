using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL.Common
{
    public class ExecuteResult
    {
        public ExecuteState State { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool IsSuccess => State == ExecuteState.Success;

        public static ExecuteResult Success()
        {
            return new ExecuteResult { State = ExecuteState.Success };
        }

        public static ExecuteResult Success(string message)
        {
            return new ExecuteResult { State = ExecuteState.Success, Message = message };
        }

        public static ExecuteResult Error(string errorMessage)
        {
            return new ExecuteResult { State = ExecuteState.Error, Message = errorMessage };
        }

        public static ExecuteResult Error(Exception exception)
        {
            return Error(exception.Message);
        }
    }

  
}
