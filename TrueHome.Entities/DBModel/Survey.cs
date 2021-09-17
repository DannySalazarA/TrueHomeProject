using System;
using System.Collections.Generic;
using System.Text;

namespace TrueHome.Entities.DBModel
{
    public class Survey
    {
        public int id { get; set; }
        public int activity_id { get; set; }
        public string answers { get; set; }
        public DateTime created_at { get; set; }
    }
}
