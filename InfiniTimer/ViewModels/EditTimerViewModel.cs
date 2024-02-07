using InfiniTimer.Enums;
using InfiniTimer.Models;
using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            EditTimerModel.PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditTimerModel.TimerType))
            {
                _timerLayout.Children.Clear();

                var timerName = EditTimerModel.TimerModel.Name;

                if (EditTimerModel.TimerType == Enum.GetName(typeof(TimerType), TimerType.Simple))
                {
                    EditTimerModel.TimerModel = new SimpleTimerModel()
                    {
                        Name = timerName
                    };
                }
                else if (EditTimerModel.TimerType == Enum.GetName(typeof(TimerType), TimerType.Advanced))
                {
                    EditTimerModel.TimerModel = new AdvancedTimerModel()
                    {
                        Name = timerName
                    };
                }

                FillTimerLayout();
            }
        }

        public ObservableCollection<String> TimerTypes { get; private set; }

        public EditTimerModel EditTimerModel { get; set; }

        public SingleTimerView SingleTimerView { get; set; }
        public AdvancedTimerView AdvancedTimerView { get; set; }

        private void FillTimerLayout()
        {
            if (EditTimerModel.TimerModel is SimpleTimerModel simpleTimerModel)
            {
                simpleTimerModel.Timer ??= new SingleTimerSection(1);
                _timerLayout.Children.Add(new SingleTimerView(simpleTimerModel.Timer));
            }
            else if (EditTimerModel.TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _timerLayout.Children.Add(new AdvancedTimerView(advancedTimerModel));
            }
        }
    }
}
