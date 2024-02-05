using ChatAppModelsLibrary.Models.Concrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppDatabaseLibraryy.Configurations
{
    public class GroupAndUserConfiguration : IEntityTypeConfiguration<GroupAndUser>
    {
        public void Configure(EntityTypeBuilder<GroupAndUser> builder)
        {
            builder.HasOne(gu => gu.User)
                .WithMany(u => u.GroupAndUsers)
                .HasForeignKey(gu => gu.UserId);

            builder.HasOne(gu => gu.Group)
              .WithMany(g => g.GroupAndUsers)
              .HasForeignKey(gu => gu.GroupId);
        }
    }
}
