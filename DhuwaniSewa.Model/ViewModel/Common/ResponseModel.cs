using System;
using System.Collections.Generic;
using System.Text;
using DhuwaniSewa.Model.Enum;

namespace DhuwaniSewa.Model.ViewModel
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public static ResponseModel Success(string message)
        {
            return new ResponseModel { Message = message, Status = nameof(ResponseStatus.Success) };
        }
        public static ResponseModel Success(string message,object data)
        {
            return new ResponseModel { Message = message, Status = nameof(ResponseStatus.Success),Data=data};
        }

        public static ResponseModel Info(string message)
        {
            return new ResponseModel { Message = message, Status = nameof(ResponseStatus.Info)};
        }

        public static ResponseModel Info(string message,object data)
        {
            return new ResponseModel { Message = message, Status = nameof(ResponseStatus.Info),Data=data };
        }
        public static ResponseModel Error(string message)
        {
            return new ResponseModel { Message = message, Status = nameof(ResponseStatus.Error) };
        }
    }
}
