using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
	/// <summary>
	/// The command that leads the user back to the login view
	/// </summary>
    public class GoBackToConnectionCommand : ICommand
    {
		#region Properties

		/// <summary>
		/// The attribute that helps us to communicate with the behavior of the view <c>RegisterView</c>
		/// </summary>
		private RegisterViewModel registerViewModel;
		public RegisterViewModel RegisterViewModel
		{
			get { return registerViewModel; }
			set { registerViewModel = value; }
		}

		/// <summary>
		/// The event of the <c>ICommand</c> interface which fires the command event
		/// </summary>
		public event EventHandler CanExecuteChanged;

		#endregion

		#region Constructor

		/// <summary>
		/// Initialises our <c>RegisterViewModel</c> attribute so we can communicate with him and the <c>RegisterView</c>
		/// </summary>
		/// <param name="registerViewModel">The actual used view-model</param>
		public GoBackToConnectionCommand(RegisterViewModel registerViewModel)
		{
			this.registerViewModel = registerViewModel;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Determines whether the command will be executed or not
		/// </summary>
		/// <param name="parameter">Data used by the command</param>
		/// <returns>true if this command can be executed, false otherwise</returns>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// The actions needed to be executed when the command occurs
		/// </summary>
		/// <param name="parameter">The parameter sent by the view to help the command to process if needed</param>
		/// <remarks>
		/// Here, we are leading the user back to the login view
		/// </remarks>
		public void Execute(object parameter)
		{
			var registerView = parameter as RegisterView;

			registerViewModel.WrongInformations = "Collapsed";

			// Closing the view
			registerView.Visibility = System.Windows.Visibility.Hidden;
		}

		#endregion

	}
}
