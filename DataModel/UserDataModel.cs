using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowReqres.DataModel
{
    public class UserDataModel
    {
        public string name { get; set; }
        public string job { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public UserDataModel(string name, string job)
        {
            this.name = name;
            this.job = job;
        }
    }
}
