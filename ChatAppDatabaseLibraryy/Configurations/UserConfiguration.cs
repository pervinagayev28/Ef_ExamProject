using ChatAppModelsLibrary.Models;
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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Seed Datas
            builder.HasData(
            new User()
            {
                Id = 1,
                Gmail = "John",
                Password = "1234",
                Bio = "I am John",
                ImagePath= "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png"
            },
             new User()
             {
                 Id = 2,
                 Gmail = "BlackBoy",
                 Password = "1234",
                 Bio = "I am BlackBoy",
                 ImagePath = "\\Images\\3135715.png",
             },
              new User()
              {
                  Id = 3,
                  Gmail = "Cavanshir",
                  Password = "1234",
                  Bio = "I am Cavanshir",
                  ImagePath = "\\Images\\download (1).jpeg"
              },
               new User()
               {
                   Id = 4,
                   Gmail = "Qudret",
                   Password = "1234",
                   Bio = "I am Qudret",
                   ImagePath = "\\Images\\photo-little-brunet-boy-wear-260nw-2030792027.webp"
               },
                new User()
                {
                    Id = 5,
                    Gmail = "Michail",
                    Password = "1234",
                    Bio = "I am Michail",
                    ImagePath = "\\Images\\3135715.png"
                });


            //Constraints
            builder.Property(u => u.Password)
                .HasAnnotation("RegularExpression", @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$")
                .IsRequired();
            builder.Property(u => u.Bio)
                .HasMaxLength(50);
            builder.HasIndex(u => u.Gmail)
                .IsUnique();

            builder.Property(u => u.Gmail)
                .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@gmail\.com$");

            //Relations
            builder.HasMany(u=>u.MessagesFroms)
                .WithOne(m=>m.From)
                .HasForeignKey(m=>m.FromId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.MessagesTo)
                .WithOne(m => m.To)
                .HasForeignKey(m => m.ToId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u=>u.ConnectionFroms)
                .WithOne(u=>u.From)
                .HasForeignKey(u=>u.FromId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.ConnectionTos)
               .WithOne(u => u.To)
               .HasForeignKey(u => u.ToId)
               .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
