using System;
using System.Collections.Generic;

namespace Common.DTO
{
    public class PagedResponseDTO<T> : ResponseDTO<T>
    {
        public int PageNumber { get; }
        public int PerPage { get; }
        public int Total { get; }
        
        /*public Uri FirstPageUrl { get; set; }
        public Uri LastPageUrl { get; set; }
        public Uri NextPageUrl { get; set; }
        public Uri PreviousPageUrl { get; set; }*/

        public int FirstPage { get; } = 1;
        public int LastPage { get; }
        public int NextPage { get; }
        public int PreviousPage { get; }
        public int To { get; } = 1;
        public int From { get; }

        public PagedResponseDTO(List<System.Threading.Tasks.Task<QueryDetailDTO>> mappedRecords, T data) : base(data)
        {
        }

        public PagedResponseDTO(T data, int pageNumber, int perPage, int total) : base(data)
        {
            PageNumber = pageNumber;
            PerPage = perPage;
            Total = total;

            var totalPages = (double) total / PerPage;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            FirstPage = 1;
            NextPage = PageNumber >= 1 && PageNumber < roundedTotalPages ? PageNumber + 1 : 0;
            PreviousPage = PageNumber - 1 >= 1 && PageNumber <= roundedTotalPages ? PageNumber - 1 : 1;
            LastPage = roundedTotalPages;
            To = FirstPage;
            From = roundedTotalPages;
        }

        public PagedResponseDTO<TData> CopyWith<TData>(TData data)
        {
            var pageNumber = PageNumber;
            var total = Total;
            var perPage = PerPage;

            /*var pagedResponseDto = new PagedResponseDTO<TData>(data);
            pagedResponseDto.PageNumber = pageNumber;
            pagedResponseDto.Total = total;
            pagedResponseDto.PerPage = PerPage;
            */

            var pagedResponseDto = new PagedResponseDTO<TData>(data, pageNumber, perPage, total);

            return pagedResponseDto;
        }
    }
}