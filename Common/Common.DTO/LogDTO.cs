#nullable enable
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using Newtonsoft.Json;

namespace Common.DTO
{
    public class LogDTO<T> : BaseLogDTO
        where T : BaseEntity
    {
        public T? Record { get; set; }
        public T? CurrentRecord { get; set; }

        public static LogDTO<T> FromLog(Entities.Log log)
        {
            var jsonString = log.Json;
            var record = JsonConvert.DeserializeObject<T>(jsonString);

            var logDto = new LogDTO<T>
            {
                Id = log.Id,
                Action = log.Action,
                Record = record,
                ModelName = log.ModelName,
                //Json = log.Json,
                UserId = log.UserId,
                ModelId = log.ModelId,
                User = log.User,
                CreatedAt = log.CreatedAt
            };

            return logDto;
        }

        public static List<LogDTO<T>> GetLogsWithCurrentRelatedRecords(List<Entities.Log> records,
            List<T> currentRecords)
        {
            var logsDtos = records.Select(obj =>
                {
                    var newLogDto = FromLog(obj);
                    var currentRecord = currentRecords.FirstOrDefault(r => r.Id == newLogDto.ModelId);
                    newLogDto.CurrentRecord = currentRecord;
                    return newLogDto;
                })
                .ToList();

            return logsDtos;
        }
    }
}