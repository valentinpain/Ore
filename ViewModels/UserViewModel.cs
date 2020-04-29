using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class UserViewModel
    {
        private string username;
        private string password;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public UserViewModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public UserViewModel()
        {

        }
    }
}