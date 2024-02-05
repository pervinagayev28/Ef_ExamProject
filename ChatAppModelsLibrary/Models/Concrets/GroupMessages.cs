using ChatAppModelsLibrary.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppModelsLibrary.Models.Concrets
{
    public class GroupMessages : IPrimaryKey<int>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public int GroupId{ get; set; }
        public virtual Group Group{ get; set; }

        public int FromId { get; set; }
        public virtual User From{ get; set; }

    }
}
