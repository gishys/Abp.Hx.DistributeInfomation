using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.Common
{
    public class DownloadResultDto<T>
    {
        public DownloadResultDto(string message)
        {
            Status = false;
            Message = message;
            Data = default;
        }
        public DownloadResultDto(T data)
        {
            Status = true;
            Data = data;
        }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}