using ChatAppModelsLibrary.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppModelsLibrary.Models.Concrets
{
    public class Group : IPrimaryKey<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImagePath{ get; set; }

        public virtual ICollection<GroupMessages> Messages { get; set; }
        public virtual ICollection<GroupAndUser>?GroupAndUsers { get; set; }

    }
}
