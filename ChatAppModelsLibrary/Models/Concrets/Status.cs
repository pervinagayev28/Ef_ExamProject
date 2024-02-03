using ChatAppModelsLibrary.Models.BaseModels;
using ChatAppService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppModelsLibrary.Models.Concrets
{
    public  class Status : ServiceINotifyPropertyChanged, IPrimaryKey<int>
    {
        private string videoPath;

        public int Id{ get; set; }

        public string ? Title { get; set; }
        public string VideoPath { get => videoPath; set { videoPath = value; OnPropertyChanged(); } }
        public int UserId { get; set; }
        public User User{ get; set; }
    }
}
