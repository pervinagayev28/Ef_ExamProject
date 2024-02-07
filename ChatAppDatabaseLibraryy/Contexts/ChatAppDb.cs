using ChatAppModelsLibrary.Models;
using ChatAppModelsLibrary.Models.Concrets;
using ChatAppService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppDatabaseLibraryy.Contexts
{
    public partial class ChatAppDb : DbContext
    {
        public ChatAppDb()
        {

        }

        public ChatAppDb(DbContextOptions<ChatAppDb> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<User> UsersTbs { get; set; }
        public virtual DbSet<UserConnection> ConnectionsTb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlServer(Configuration.GetValue(),
                            builder => builder.EnableRetryOnFailure());
                //.UseLazyLoadingProxies();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
