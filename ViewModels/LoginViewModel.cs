using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyToolkit.Command;
using Ore.ViewModels.Commands;

namespace Ore.ViewModels
{
	/// <summary>
	/// The view-model class of the login view that decides how a login view must behave
	/// </summary>
	public class LoginViewModel : INotifyPropertyChanged
    {
		#region Properties

		/// <summary>
		/// The user that wants to be connected
		/// </summary>
		private static UserViewModel user = new UserViewModel();
		public static UserViewModel User
		{
			get { return user; }
			set { user = value; }
		}

		/// <summary>
		/// Helps the user if he has entered wrong informations
		/// </summary>
		private string wrongInformations = "Collapsed";
		public string WrongInformations
		{
			get { return wrongInformations; }
			set
			{
				wrongInformations = value;
				NotifyPropertyChanged(nameof(WrongInformations));
			}
		}

		/// <summary>
		/// The attribute used to shut-down the view when the user is connecting to his session
		/// </summary>
		public Action CloseAction { get; set; }

		/// <summary>
		/// The events that prevents a new change in the properties values
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region The command properties

		/// <summary>
		/// The command to log the user in
		/// </summary>
		public ICommand LoginAccessCommand { get { return new LoginAccessCommand(this); } }

		/// <summary>
		/// The command that loads the register view
		/// </summary>
		public ICommand LoadRegisterViewCommand { get { return new LoadRegisterViewCommand(this); } }

		#endregion

		#region Constructor

		#endregion

		#region Methods

		/// <summary>
		/// The methods that prevents a change in the properties values
		/// </summary>
		/// <param name="name">The name of the property</param>
		private void NotifyPropertyChanged(String name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion

	}
}
