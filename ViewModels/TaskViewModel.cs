using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class TaskViewModel
    {
        public int _id { get; set; }

        public string _name { get; set; }

        public string _color { get; set; }

        public int _day { get; set; }

        public string _time { get; set; }

        public string _month { get; set; }

        public bool _complete { get; set; }
    }
}
