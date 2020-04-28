﻿using Ore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class DeleteCommand : ICommand
    {
        private ShellViewModel shellViewModel = new ShellViewModel();

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach(TaskViewModel task in ShellViewModel.Tasks)
            {
                if (int.Parse(task._id.ToString()) == int.Parse(parameter.ToString()))
                {
                    ShellViewModel.Tasks.Remove(task);
                    ShellModel.deleteTask(task._id);
                    break;
                }
            }
        }
    }
}
