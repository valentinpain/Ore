using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    /// <summary>
    /// The view-model class of the views that decides how a register user must behave
    /// </summary>
    public class UserViewModel
    {
        #region Properties

        /// <summary>
        /// The user name
        /// </summary>
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// The password of the user account
        /// </summary>
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// The id of the user in the database and the code
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor to create an user
        /// </summary>
        public UserViewModel()
        {
            this.username = "";
            this.password = "";
            this.id = 0;
        }

        #endregion

        #region Methods

        #endregion

    }
}