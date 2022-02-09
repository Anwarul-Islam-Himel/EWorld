using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ResponseModel
{
    public class Result<T>
    {
        public int StatusCode { get; set; }
        public T Response { get; set; }
    }
}
