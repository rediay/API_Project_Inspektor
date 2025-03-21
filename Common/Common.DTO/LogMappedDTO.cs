using System.Collections.Generic;
using System.Linq;
using Common.DTO.Users;
using Common.Entities;

#nullable enable
namespace Common.DTO
{
    public class LogMappedDTO<T> : BaseLogDTO
    {
        public T? Record { get; set; }
        public T? CurrentRecord { get; set; }
        public UserManagementDto? User { get; set; }

        public static LogMappedDTO<T> FromLogDTO<R>(LogDTO<R> record)
            where R : BaseEntity
        {
            var newRecord = new LogMappedDTO<T>
            {
                Id = record.Id,
                UserId = record.UserId,
                Action = record.Action,
                ModelName = record.ModelName,
                ModelId = record.ModelId,
            };

            return newRecord;
        }
    }
}