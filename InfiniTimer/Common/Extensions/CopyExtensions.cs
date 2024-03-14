using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.Common.Extensions
{
    public static class CopyExtensions
    {
        public static AdvancedTimerModel Copy(this AdvancedTimerModel advancedTimerModel)
        {
            if (null == advancedTimerModel) return null;

            var copy = new AdvancedTimerModel
            {
                IgnoreChanges = true,
                Id = advancedTimerModel.Id,
                AutoContinue = advancedTimerModel.AutoContinue,
                AutoRepeat = advancedTimerModel.AutoRepeat,
                Description = advancedTimerModel.Description,
                IsDirty = advancedTimerModel.IsDirty,
                IsSelected = advancedTimerModel.IsSelected,
                IsStaged = advancedTimerModel.IsStaged,
                Name = advancedTimerModel.Name,
                TimerSection = advancedTimerModel.TimerSection.Copy()
            };

            copy.IgnoreChanges = false;
            return copy;
        }

        public static SimpleTimerModel Copy(this SimpleTimerModel simpleTimerModel)
        {
            if (null == simpleTimerModel) return null;

            var copy = new SimpleTimerModel
            {
                IgnoreChanges = true,
                Id = simpleTimerModel.Id,
                IsDirty = simpleTimerModel.IsDirty,
                IsSelected = simpleTimerModel.IsSelected,
                IsStaged = simpleTimerModel.IsStaged,
                Name = simpleTimerModel.Name,
                Timer = simpleTimerModel.Timer.Copy()
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

        public static AlternatingTimerSection Copy(this AlternatingTimerSection alternatingTimerSection)
        {
            if (null == alternatingTimerSection) return null;

            return new AlternatingTimerSection
            {
                Cycles = alternatingTimerSection.Cycles,
                Depth = alternatingTimerSection.Depth,
                MainTimerSection = alternatingTimerSection.MainTimerSection.Copy(),
                AlternateTimerSection = alternatingTimerSection.AlternateTimerSection.Copy()
            };
        }

        public static SequentialTimerSection Copy(this SequentialTimerSection sequentialTimerSection)
        {
            if (null == sequentialTimerSection) return null;

            return new SequentialTimerSection
            {
                Depth = sequentialTimerSection.Depth,
                TimerSections = sequentialTimerSection.TimerSections.Copy()
            };
        }

        public static ObservableCollection<TimerSection> Copy(this ObservableCollection<TimerSection> timerSections)
        {
            if (null == timerSections) return null;

            return new ObservableCollection<TimerSection>
                (timerSections.Select(timerSection => timerSection.Copy()));
        }

        public static TimerModel Copy(this TimerModel timer)
        {
            if (timer is AdvancedTimerModel advancedTimerModel)
            {
                return advancedTimerModel.Copy();
            }
            else if (timer is SimpleTimerModel simpleTimerModel)
            {
                return simpleTimerModel.Copy();
            }

            return null;
        }

        public static Dictionary<Guid, TimerModel> Copy(this Dictionary<Guid, TimerModel> timers)
        {
            if (null == timers) return null;

            return timers.ToDictionary(t => t.Key, t => t.Value.Copy());
        }

        public static TimerSection Copy(this TimerSection timerSection)
        {
            if (timerSection is SequentialTimerSection sequentialTimerSection)
            {
                return sequentialTimerSection.Copy();
            }
            else if (timerSection is AlternatingTimerSection alternatingTimerSection)
            {
                return alternatingTimerSection.Copy();
            }
            else if (timerSection is SingleTimerSection singleTimerSection)
            {
                return singleTimerSection.Copy();
            }

            return null;
        }
    }
}
