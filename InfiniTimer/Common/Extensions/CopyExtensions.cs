using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.Common.Extensions
{
    public static class CopyExtensions
    {
        public static TimerModel Copy(this TimerModel timerModel)
        {
            if (null == timerModel) return null;

            var copy = new TimerModel
            {
                IgnoreChanges = true,
                Id = timerModel.Id,
                AutoContinue = timerModel.AutoContinue,
                AutoRepeat = timerModel.AutoRepeat,
                Description = timerModel.Description,
                IsDirty = timerModel.IsDirty,
                IsSelected = timerModel.IsSelected,
                IsStaged = timerModel.IsStaged,
                Name = timerModel.Name,
                Timers = timerModel.Timers.Copy()
            };

            copy.IgnoreChanges = false;
            return copy;
        }

        public static SingleTimerSection Copy(this SingleTimerSection singleTimerSection)
        {
            if (null == singleTimerSection) return null;

            return new SingleTimerSection
            {
                DisplayText = singleTimerSection.DisplayText,
                Depth = singleTimerSection.Depth,
                Color = singleTimerSection.Color,
                Seconds = singleTimerSection.Seconds,
                Vibrate = singleTimerSection.Vibrate,
                Sound = singleTimerSection.Sound,
            };
        }

        public static TimerListSection Copy(this TimerListSection timerListSection)
        {
            if (null == timerListSection) return null;

            return new TimerListSection
            {
                Cycles = timerListSection.Cycles,
                Depth = timerListSection.Depth,
                TimerSections = timerListSection.TimerSections.Copy()
            };
        }

        public static ObservableCollection<TimerSection> Copy(this ObservableCollection<TimerSection> timerSections)
        {
            if (null == timerSections) return null;

            return new ObservableCollection<TimerSection>
                (timerSections.Select(timerSection => timerSection.Copy()));
        }

        public static Dictionary<Guid, TimerModel> Copy(this Dictionary<Guid, TimerModel> timers)
        {
            if (null == timers) return null;

            return timers.ToDictionary(t => t.Key, t => t.Value.Copy());
        }

        public static TimerSection Copy(this TimerSection timerSection)
        {
            if (timerSection is TimerListSection timerListSection)
            {
                return timerListSection.Copy();
            }
            else if (timerSection is SingleTimerSection singleTimerSection)
            {
                return singleTimerSection.Copy();
            }

            return null;
        }
    }
}
