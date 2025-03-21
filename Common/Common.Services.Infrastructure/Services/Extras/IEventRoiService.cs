using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IEventRoiService
    {
        Task<ResponseDTO<EventRoiDTO>> Create(EventRoiDTO roiDto);
        Task<ResponseDTO<List<EventRoiOperationType>>> GetAllOperationTypes();
        Task<ResponseDTO<List<EventRoiOperationStatus>>> GetAllOperationStatuses();
    }
}