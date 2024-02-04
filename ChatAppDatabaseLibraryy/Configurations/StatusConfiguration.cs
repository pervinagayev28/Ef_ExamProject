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
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
            new Status()
            {
                Id = 1,
                UserId = 2,
                VideoPath = @"\Database\Statues\video1.mp4"
            },
            new Status()
            {
                Id = 2,
                UserId = 2,
                VideoPath = @"\Database\Statues\video2.mp4"
            },
             new Status()
             {
                 Id = 3,
                 UserId = 2,
                 VideoPath = @"\Database\Statues\video3.mp4"
             },
              new Status()
              {
                  Id = 4,
                  UserId = 2,
                  VideoPath = @"\Database\Statues\video4.mp4"
              },
               new Status()
               {
                   Id = 5,
                   UserId = 2,
                   VideoPath = @"\Database\Statues\video5.mp4"
               });
        }
    }
}
