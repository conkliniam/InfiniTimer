namespace InfiniTimer.Models.Timers
{
    public class SimpleTimerModel : TimerModel
    {
        private SingleTimerSection _timer;

        public SingleTimerSection Timer
        {
            get
            {
                return _timer;
            }
            set
            {
                if (_timer != value)
                {
                    var old = _timer;
                    _timer = value;

                    if (old != null)
                    {
                        old.PropertyChanged -= TimerSection_PropertyChanged;
                    }

                    if (_timer != null)
                    {
                        _timer.PropertyChanged += TimerSection_PropertyChanged;
                    }
                }
            }
        }
    }
}
