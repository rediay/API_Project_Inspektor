using System.Collections.Generic;

namespace Common.DTO
{
    public class ResponseDTO<T>
    {
        public ResponseDTO(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public Dictionary<string, dynamic> Errors { get; set; }
        public string Message { get; set; }
    }
}