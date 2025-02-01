using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public ApiResponse()
        {
            
        }
        //Succeed
        public ApiResponse(T data, string message = null)
        {
            Message = message;
            Succeeded = true;
            Data = data;
        }

        //failed

        public ApiResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}
