using ChatAppModelsLibrary.Models.Concrets;
using ChatAppService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Dtos
{
    public class GroupDto:ServiceINotifyPropertyChanged
    {
        private string imagePath;

        public int Id { get; set; }

        public string Name { get; set; }
        public string ImagePath { get => imagePath; set { imagePath = value; OnPropertyChanged(); } }
        public virtual ICollection<GroupAndUser>? GroupAndUsers { get; set; }

    }
}
