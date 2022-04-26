using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnUygun.Solution.Models
{
    public class DevJobVidewData
    {
        public string DevName { get; set; }
        public string TaskName { get; set; }
        public int TaskTime { get; set; }
        public int TaskLevel { get; set; }
        public int Weeks { get; set; }
        public int TotalTime { get; set; }
    }
}