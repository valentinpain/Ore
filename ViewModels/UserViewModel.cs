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
        private int id;

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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public UserViewModel()
        {
            this.username = "";
            this.password = "";
            this.id = 0;
        }
    }
}