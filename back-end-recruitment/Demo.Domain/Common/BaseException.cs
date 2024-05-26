using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Domain.Common
{
    public class BaseException
    {
        public int ErrorCode { get; set; }

        public string? DevMessage { get; set; }

        public string? UserMessage { get; set; }

        public object? Errors { get; set; }

        public BaseException(int errorCode, string? devMessage, string? userMessage, object? errors)
        {
            ErrorCode = errorCode;
            DevMessage = devMessage;
            UserMessage = userMessage;
            Errors = errors;
        }
    }
}
