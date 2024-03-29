﻿using ChatAppModelsLibrary.Models.Concrets;
using ChatAppModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppService.Services;

namespace Whatsapp.Dtos
{
    public class UserDto:ServiceINotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Password { get; set; } = null!;
        public string? Bio { get; set; } = null!;
        public string Gmail { get => gmail; set { gmail = value; OnPropertyChanged(); } }
        public bool IsUsing { get; set; }
        public string? ImagePath { get => imagePath; set { imagePath = value; OnPropertyChanged(); } }

        public virtual ICollection<Message>? MessagesTo { get; set; }

        public virtual ICollection<Message>? MessagesFroms { get; set; }
        public virtual ICollection<UserConnection>? ConnectionTos { get; set; }
        public virtual ICollection<UserConnection>? ConnectionFroms { get; set; }
        public virtual ICollection<Status>? Status { get => status; set { status = value; OnPropertyChanged(); } }

        public virtual ICollection<GroupAndUser>? GroupAndUsers { get; set; }


        //Not mapped propts.
        [NotMapped]
        public string? LastMessage { get => lastMessage; set { lastMessage = value; OnPropertyChanged(); } }
        private string? lastMessage;
        [NotMapped]
        public DateTime? LastMessageDate { get => lastMessageDate; set { lastMessageDate = value; OnPropertyChanged(); } }
        private DateTime? lastMessageDate;

        //back fields
        private string gmail = null!;
        private string? imagePath;
        private ICollection<Status>? status;
    }
}
