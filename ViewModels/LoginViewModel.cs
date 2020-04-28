using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyToolkit.Command;
using Ore.ViewModels.Commands;

namespace Ore.ViewModels
{
    public class LoginViewModel
    {
		private UserViewModel user = new UserViewModel("Valentip", "mdp");

		public UserViewModel User
		{
			get { return user; }
			set { user = value; }
		}

		public ICommand LoginAccessCommand { get { return new LoginAccessCommand(this); } }

		public Action CloseAction { get; set; }

	}
}
