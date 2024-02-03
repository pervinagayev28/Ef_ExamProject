using ChatAppModelsLibrary.Models.BaseModels;
using ChatAppService.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppModelsLibrary.Models
{
    public class Message:IPrimaryKey<int>
    {
        public int Id { get; set; }
        public string message { get; set; }

        public DateTime Date { get; set; }


        public int ToId { get; set; }

        public virtual User To { get; set; }

        public int FromId { get; set; }
        public virtual User From { get; set; } = null!;



    }
}
