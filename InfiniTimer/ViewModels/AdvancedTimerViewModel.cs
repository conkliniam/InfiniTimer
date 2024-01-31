using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.ViewModels
{
    public class AdvancedTimerViewModel
    {
        private readonly StackLayout _timerLayout;

        public AdvancedTimerViewModel(AdvancedTimerModel advancedTimerModel, StackLayout timerLayout)
        {
            _timerLayout = timerLayout;
            AdvancedTimerModel = advancedTimerModel;
            FillTimerLayout();
        }

        public AdvancedTimerModel AdvancedTimerModel { get; set;}

        private void FillTimerLayout()
        {
            if (AdvancedTimerModel.TimerSection == null)
            {
                _timerLayout.Children.Add(new AddTimerButtonsView(HandleAddTimer, HandleAddList, HandleAddAlternates));
            }
            else if (AdvancedTimerModel.TimerSection is AlternatingTimerSection alternatingTimerSection)
            {
                _timerLayout.Children.Add(new AlternatingTimerView(alternatingTimerSection));
            }
            else if (AdvancedTimerModel.TimerSection is SequentialTimerSection sequentialTimerSection)
            {
                _timerLayout.Children.Add(new SequentialTimerView(sequentialTimerSection));
            }
            else if (AdvancedTimerModel.TimerSection is SingleTimerSection singleTimerSection)
            {
                _timerLayout.Children.Add(new SingleTimerView(singleTimerSection));
            }
        }

        private void HandleAddTimer()
        {
            _timerLayout.Children.Clear();
            AdvancedTimerModel.TimerSection = new SingleTimerSection(1);
            FillTimerLayout();
        }

        private void HandleAddList()
        {
            _timerLayout.Children.Clear();
            AdvancedTimerModel.TimerSection = new SequentialTimerSection(1);
            FillTimerLayout();
        }

        private void HandleAddAlternates()
        {
            _timerLayout.Children.Clear();
            AdvancedTimerModel.TimerSection = new AlternatingTimerSection(1);
            FillTimerLayout();
        }
    }
}
