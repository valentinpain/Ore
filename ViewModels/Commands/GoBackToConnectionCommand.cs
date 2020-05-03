using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class GoBackToConnectionCommand : ICommand
    {
		private RegisterViewModel registerViewModel;

		public RegisterViewModel RegisterViewModel
		{
			get { return registerViewModel; }
			set { registerViewModel = value; }
		}


		public event EventHandler CanExecuteChanged;

		public GoBackToConnectionCommand(RegisterViewModel registerViewModel)
		{
			this.registerViewModel = registerViewModel;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var registerView = parameter as RegisterView;

			registerViewModel.WrongInformations = "Collapsed";
			registerView.Visibility = System.Windows.Visibility.Hidden;
		}
	}
}
