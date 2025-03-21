using Common.Entities.BulkQuery;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.DTO.Queries;
using Common.DTO;
using Common.Utils;
using Common.Services.Infrastructure.Services.Files;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Common.DataAccess.EFCore.Repositories.Queries
{
    public class BulkQueryAdditionalRepository : IBulkQueryAdditionalRepository
    {
        private readonly DataContext _context;
        private readonly IFileShare fileShare;
        protected IConfigurationSection configuration;

        public BulkQueryAdditionalRepository(DataContext context, IFileShare fileShare, IConfiguration configuration)
        {
            _context = context;
            this.fileShare = fileShare;
            this.configuration = configuration.GetSection("ConnectionStrings");
        }
        public async Task<BulkQueryServicesAdditionalResponseDTO> getBulkQueryServiceAdditional(int QueryId, ContextSession session)
        {
            var userId = session.UserId;
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Common.WebApiCore"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("localDb");

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(connectionString);

            using (var dbContext = new DataContext(builder.Options))
            {
                var currentUSer = dbContext.Users.Find(userId);

                var image = (from sw in dbContext.Companies where sw.Id == currentUSer.CompanyId select sw.Image).FirstOrDefault();

                //BulkQueryServicesAdditionalResponseDTO responseDto = FilesHelper.getBulkQueryServicesAddiotional(QueryId);
                BulkQueryServicesAdditionalResponseDTO responseDto = await fileShare.FileDownloadAsync<BulkQueryServicesAdditionalResponseDTO>(QueryId, true);
                if (responseDto != null && responseDto.QueryServiceAdditional.CompanyId != currentUSer.CompanyId)
                {
                    responseDto = null;
                }
                responseDto.QueryServiceAdditional.User = currentUSer.MapTo<UserDTO>();
                responseDto.Image = image;

                return await Task.FromResult<BulkQueryServicesAdditionalResponseDTO>(responseDto);
            }
        }
        public async Task<BulkQueryServicesAdditional> AddFileTable(BulkQueryServicesAdditional bulkQueryServicesAdditional, int userId)
        {
            try
            {
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Common.WebApiCore"))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("localDb");

                var builder = new DbContextOptionsBuilder<DataContext>();
                builder.UseSqlServer(connectionString);
                //var userId = session.UserId;
                //var user = _context.Users.Find(userId);//_context.Users.Where(x => x.Id == userId).FirstOrDefault();
                using (var dbContext = new DataContext(builder.Options))
                {
                    var user = dbContext.Users.Find(userId);
                    bulkQueryServicesAdditional.CompanyId = user.CompanyId;
                    bulkQueryServicesAdditional.UserId = userId;

                    BulkQueryServicesAdditional queryServicesAdditional = new BulkQueryServicesAdditional();

                    if (bulkQueryServicesAdditional.Id != 0)
                        queryServicesAdditional = dbContext.BulkQueryServicesAdditional.
                               FromSqlInterpolated($"dbo.UpdateFieldTableBulkQueryServicesAdditional {bulkQueryServicesAdditional.Id},{bulkQueryServicesAdditional.CurrentConsulting},{bulkQueryServicesAdditional.TotalConsulting}").AsEnumerable().FirstOrDefault();
                    else
                        queryServicesAdditional = dbContext.BulkQueryServicesAdditional.
                               FromSqlInterpolated($"dbo.AddFieldTableBulkQueryServicesAdditional {bulkQueryServicesAdditional.attorneyService},{bulkQueryServicesAdditional.judicialBranchService},{bulkQueryServicesAdditional.jempsJudicialBranchService},{bulkQueryServicesAdditional.UserId},{bulkQueryServicesAdditional.CompanyId},{bulkQueryServicesAdditional.ConsultingStatus},{bulkQueryServicesAdditional.CurrentConsulting},{bulkQueryServicesAdditional.TotalConsulting},{bulkQueryServicesAdditional.CreatedAt}").AsEnumerable().FirstOrDefault(); ;


                    return queryServicesAdditional;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<BulkQueryServicesAdditionalDTO>> getBulkQueryServiceAdditionalTable(ContextSession session)
        {
            try
            {
                var userId = session.UserId;                
                var currentUSer = _context.Users.Find(userId);
                var companyId = currentUSer.CompanyId;

                var bulkQueryServicesAdditional = await _context.BulkQueryServicesAdditional.Where(obj => obj.CompanyId == companyId).AsNoTracking().ToListAsync();

                var bulkQueryServicesAdditionalDTO = bulkQueryServicesAdditional.MapTo<List<BulkQueryServicesAdditionalDTO>>();

                return await Task.FromResult<List<BulkQueryServicesAdditionalDTO>>(bulkQueryServicesAdditionalDTO);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
