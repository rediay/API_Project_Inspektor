using Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration.System
{
    public class NotificationConfig : BaseEntityConfig<Notification>
    {
        public NotificationConfig() : base("Notifications")
        {
            
        }

        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);
            
            builder.Property(obj => obj.Subject);
            builder.Property(obj => obj.To);
            builder.Property(obj => obj.Detail);
            builder.Property(obj => obj.Status);
            builder.Property(obj => obj.CompanyId);
            builder.Property(obj => obj.UserId);
        }
    }
}