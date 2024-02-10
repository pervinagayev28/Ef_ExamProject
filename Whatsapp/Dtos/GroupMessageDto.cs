using ChatAppModelsLibrary.Models;
using ChatAppModelsLibrary.Models.Concrets;
using ChatAppService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Dtos
{
    public  class GroupMessageDto:ServiceINotifyPropertyChanged
    {
        private string messageForVisual;

        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int FromId { get; set; }
        public virtual UserDto From { get; set; }
        public int RightOrLeft{ get; set; }
        public string MessageForVisual { get => messageForVisual; set { messageForVisual = value; OnPropertyChanged(); } }
    }
}
