using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Responses
{
    public class Response<T>
    {

        public int StatusCode { get; set; }
        public T result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
