using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class TaskViewModel
    {
		private string stringTest = "ça fonctionne !";

		public string StringTest
		{
			get { return stringTest = "ça fonctionne !"; }
			set { stringTest = value; }
		}

	}
}
