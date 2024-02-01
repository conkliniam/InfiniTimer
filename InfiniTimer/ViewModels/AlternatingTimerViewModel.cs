using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class AlternatingTimerViewModel
    {
        private readonly ResourceDictionary _resources;
        private StackLayout _mainTimerLayout;
        private StackLayout _alternateTimerLayout;

        public AlternatingTimerViewModel(AlternatingTimerSection alternatingTimerSection, ResourceDictionary resources, StackLayout mainTimerLayout, StackLayout alternateTimerLayout)
        {
            _mainTimerLayout = mainTimerLayout;
            _alternateTimerLayout = alternateTimerLayout;
            AlternatingTimerSection = alternatingTimerSection;
            _resources = resources;
            CycleOptions = new ObservableCollection<int>(Enumerable.Range(1, AppConstants.CycleLimit));
            Margin = new Thickness(AlternatingTimerSection.Depth * 10, 0, 0, 0);
            GetColors();
            FillTimerLayout();
        }

        public AlternatingTimerSection AlternatingTimerSection { get; set; }
        public ObservableCollection<int> CycleOptions { get; private set; }
        public Thickness Margin { get; private set; }
        public Color MainColor { get; private set; }
        public Color AlternateColor { get; private set; }

        private void FillTimerLayout()
        {
            FillTimerSection(isMainSection: true);
            FillTimerSection(isMainSection: false);
        }

        private void FillTimerSection(bool isMainSection)
        {
            var stackLayout = isMainSection ? _mainTimerLayout : _alternateTimerLayout;
            ITimerSection timerSection = isMainSection ? AlternatingTimerSection.MainTimerSection : AlternatingTimerSection.AlternateTimerSection;

            if (timerSection == null)
            {
                stackLayout.Children.Add(new AddTimerButtonsView
                    (() => HandleAddTimer(isMainSection),
                    () => HandleAddList(isMainSection),
                    () => HandleAddAlternates(isMainSection)));
            }
            else if (timerSection is AlternatingTimerSection alternatingTimerSection)
            {
                stackLayout.Children.Add(new AlternatingTimerView(alternatingTimerSection));
            }
            else if (timerSection is SequentialTimerSection sequentialTimerSection)
            {
                stackLayout.Children.Add(new SequentialTimerView(sequentialTimerSection));
            }
            else if (timerSection is SingleTimerSection singleTimerSection)
            {
                stackLayout.Children.Add(new SingleTimerView(singleTimerSection));
            }
        }

        private void HandleAddTimer(bool isMainSection)
        {
            var stackLayout = isMainSection ? _mainTimerLayout : _alternateTimerLayout;
            stackLayout.Children.Clear();

            if (isMainSection)
            {
                AlternatingTimerSection.MainTimerSection = new SingleTimerSection(AlternatingTimerSection.Depth + 1);
            }
            else
            {
                AlternatingTimerSection.AlternateTimerSection = new SingleTimerSection(AlternatingTimerSection.Depth + 1);
            }

            FillTimerSection(isMainSection);
        }

        private void HandleAddList(bool isMainSection)
        {
            var stackLayout = isMainSection ? _mainTimerLayout : _alternateTimerLayout;
            stackLayout.Children.Clear();

            if (isMainSection)
            {
                AlternatingTimerSection.MainTimerSection = new SequentialTimerSection(AlternatingTimerSection.Depth + 1);
            }
            else
            {
                AlternatingTimerSection.AlternateTimerSection = new SequentialTimerSection(AlternatingTimerSection.Depth + 1);
            }

            FillTimerSection(isMainSection);
        }

        private void HandleAddAlternates(bool isMainSection)
        {
            var stackLayout = isMainSection ? _mainTimerLayout : _alternateTimerLayout;
            stackLayout.Children.Clear();

            if (isMainSection)
            {
                AlternatingTimerSection.MainTimerSection = new AlternatingTimerSection(AlternatingTimerSection.Depth + 1);
            }
            else
            {
                AlternatingTimerSection.AlternateTimerSection = new AlternatingTimerSection(AlternatingTimerSection.Depth + 1);
            }

            FillTimerSection(isMainSection);
        }

        private void GetColors()
        {
            string color = AlternatingTimerSection.Depth switch
            {
                1 => "Green",
                2 => "Red",
                3 => "Blue",
                4 => "Orange",
                _ => "Purple",
            };
            if (_resources.TryGetValue("Dark" + color, out var darkColor))
            {
                MainColor = (Color)darkColor;
            }
            if (_resources.TryGetValue("Light" + color, out var lightColor))
            {
                AlternateColor = (Color)lightColor;
            }
        }
    }
}
