using InfiniTimer.Enums;
using InfiniTimer.Models;
using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class EditTimerViewModel
    {
        private readonly StackLayout _timerLayout;
        public EditTimerViewModel(StackLayout timerLayout, TimerModel timerModel = null)
        {
            _timerLayout = timerLayout;
            timerModel ??= new SimpleTimerModel();
            EditTimerModel = new EditTimerModel(timerModel);
            TimerTypes = new ObservableCollection<string>(Enum.GetNames(typeof(TimerType)).ToList());
            FillTimerLayout();
        }

        public ObservableCollection<String> TimerTypes { get; private set; }

        public EditTimerModel EditTimerModel { get; set; }

        public SingleTimerView SingleTimerView { get; set; }
        public AdvancedTimerView AdvancedTimerView { get; set; }

        public void FillTimerLayout()
        {
            if (EditTimerModel.TimerModel is SimpleTimerModel simpleTimerModel)
            {
                simpleTimerModel.Timer ??= new SingleTimerSection();
                _timerLayout.Children.Add(new SingleTimerView(simpleTimerModel.Timer));
            }
            else if (EditTimerModel.TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _timerLayout.Children.Add(new AdvancedTimerView(advancedTimerModel));
            }
        }
    }
}
