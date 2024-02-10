using ChatAppModelsLibrary.Models.BaseModels;
using ChatAppModelsLibrary.Models.Concrets;
using ChatAppService.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppModelsLibrary.Models
{
    public class User:  IPrimaryKey<int>
    {
        public int Id { get; set; }

        public string Password { get; set; } = null!;
        public string? Bio { get; set; } = null!;
        public string Gmail { get; set; }
        public bool IsUsing{ get; set; }
        public string? ImagePath { get; set; }

        public virtual ICollection<Message>? MessagesTo { get; set; }

        public virtual ICollection<Message>? MessagesFroms { get; set; }
        public virtual ICollection<UserConnection>? ConnectionTos { get; set; }
        public virtual ICollection<UserConnection>? ConnectionFroms { get; set; }
        public virtual ICollection<Status>? Status { get; set; }
        public virtual ICollection<GroupAndUser>? GroupAndUsers{ get; set; }
        public virtual ICollection<GroupMessages>? GroupMessages{ get; set; }
    }
}
