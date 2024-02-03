using ChatAppModelsLibrary.Models;
using ChatAppService.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Dtos
{
    public  class MessageDto:ServiceINotifyPropertyChanged
    {
        private DateTime date;
        private string messagee = null!;
        private string messageForVisual;


        public int Id { get; set; }


        public string message { get => messagee; set { messagee = value; OnPropertyChanged(); } }

        public DateTime Date { get => date; set { date = value; OnPropertyChanged(); } }


        public int ToId { get; set; }

        public virtual UserDto To { get; set; }

        public int FromId { get; set; }
        public virtual UserDto From { get; set; } = null!;



        [NotMapped]
        public string MessageForVisual { get => messageForVisual; set { messageForVisual = value; OnPropertyChanged(); } }

        [NotMapped]

        public int RightOrLeft { get; set; }
    }
}
