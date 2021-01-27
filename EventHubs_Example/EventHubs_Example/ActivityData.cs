using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubs_Example
{
    class ActivityData
    {
        public string id { get; set; }
        public string Correlationid { get; set; }
        public string Operationname { get; set; }
        public string status { get; set; }
        public string EventCategory { get; set; }
        public string Level { get; set; }
        public DateTime dttime { get; set; }

        public string subscription { get; set; }
        public string InitiatedBy { get; set; }

        public string resourcetype { get; set; }
        public string resourcegroup { get; set; }
        public string resource { get; set; }

        public override string ToString()
        {
            return ($"Id : {id}, Operation Name : {Operationname}");
        }
    }
}
