using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services.Files
{
    public interface IFileShare
    {
        Task FileUploadAsync<T>(T objResponse, int idQuery, bool isAditional = false);
        Task<T> FileDownloadAsync<T>(int idQuery, bool isAditional = false);
        //Task<IndividualQueryResponseDTO> FileDownloadAsync(int idQuery);
    }
}
