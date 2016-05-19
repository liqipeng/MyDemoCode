using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAllocation.Core.Common
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException: Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(string message)
            : base(message)
        {

        }

        public BusinessException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}