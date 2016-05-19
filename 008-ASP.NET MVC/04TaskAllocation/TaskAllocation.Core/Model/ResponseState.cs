using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAllocation.Core.Models
{
    public enum ResponseState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败，一般无信息
        /// </summary>
        Failed = 1,

        /// <summary>
        /// 错误，一般是有错误信息
        /// </summary>
        Error = 2
    }
}