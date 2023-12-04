using InfiniTimer.Models.Timers;
using System.Text.Json;

namespace InfiniTimer.ViewModels
{
    public class TimersViewModel
    {
        public const string FileName = "InfiniTimerTimers.json";

        public List<TimerModel> TimerModels { get; set; }

        public TimersViewModel()
        {
            GetTimers();
        }

        private void GetTimers()
        {
            var rawData = string.Empty;

            if (File.Exists(FileName))
            {
                rawData = File.ReadAllText(FileName);
            }


            if (string.IsNullOrWhiteSpace(rawData))
            {
                TimerModels = new List<TimerModel>();
            }
            else
            {
                TimerModels = JsonSerializer.Deserialize<List<TimerModel>>(rawData);
            }
        }
    }
}
