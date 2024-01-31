using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.ViewModels
{
    public class AddTimerButtonsViewModel
    {
        public Action AddTimer { get; set; }
        public Action AddList { get; set; }
        public Action AddAlternates { get; set; }
        public AddTimerButtonsViewModel(Action HandleAddTimer, Action HandleAddList, Action HandleAddAlternates)
        {
            AddTimer = HandleAddTimer;
            AddList = HandleAddList;
            AddAlternates = HandleAddAlternates;
        }

        private void RunAddTimer()
        {
            AddTimer();
        }

        private void RunAlternates()
        {
            AddAlternates();
        }

        private void RunAddList()
        {
            AddList();
        }
    }
}
