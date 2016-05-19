using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAllocation.Core.Common
{
    /// <summary>
    /// 权限不足异常
    /// </summary>
    public class InsufficientPermissionException: Exception
    {
        public InsufficientPermissionException()
        {

        }

        public InsufficientPermissionException(string message)
            : base(message)
        {

        }

        public InsufficientPermissionException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}