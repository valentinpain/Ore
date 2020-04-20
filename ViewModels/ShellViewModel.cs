using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private BindableCollection<string> _displayModes = new BindableCollection<string>();
        private string _selectedDisplayMode;

        private string testBinding = "le binding fonctionne !";

        public string TestBinding
        {
            get { return testBinding = "le binding fonctionne !"; }
            set { testBinding = value; }
        }


        public string SelectedDisplayMode
        {
            get { return _selectedDisplayMode; }
            set 
            { 
                _selectedDisplayMode = value;
                NotifyOfPropertyChange(() => SelectedDisplayMode);
            }
        }

        public BindableCollection<string> DisplayModes
        {
            get { return _displayModes = new BindableCollection<string>(); }
            set { _displayModes = value; }
        }

        public ShellViewModel()
        {
            _displayModes.Add("Day");
            _displayModes.Add("Month");
            _displayModes.Add("Week");
        }

        public void LoadDailyCalendar()
        {
            // ActivateItem(new CalendarByDayViewModel());
        }

        public void LoadMonthlyCalendar()
        {
           ActivateItem(new CalendarByMonthViewModel());
        }

        public void LoadWeeklyCalendar()
        {
            ActivateItem(new CalendarByWeekViewModel());
        }

    }
}
