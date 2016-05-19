using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAllocation.Core.Models
{
    public class ResponseObject
    {
        public ResponseObject()
        {
            this.ResponseState = ResponseState.Failed;
        }

        public ResponseState ResponseState { get; set; }

        public String Message { get; set; }
    }
}