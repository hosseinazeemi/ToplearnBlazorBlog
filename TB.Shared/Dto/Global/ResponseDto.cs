using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Shared.Dto.Global
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {

        }
        public ResponseDto(bool status, string message, T data)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
