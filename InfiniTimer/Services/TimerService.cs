using InfiniTimer.Models.Timers;
using System.Text.Json;

namespace InfiniTimer.Services
{
    public class TimerService : ITimerService
    {
        private const string FileName = "InfiniTimerTimers.json";

        public List<TimerModel> GetSavedTimers()
        {
            var rawData = string.Empty;

            if (File.Exists(FileName))
            {
                rawData = File.ReadAllText(FileName);
            }


            if (string.IsNullOrWhiteSpace(rawData))
            {
                return new List<TimerModel>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<TimerModel>>(rawData);
            }
        }

        public bool SaveTimers(List<TimerModel> timers)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(timers);
                File.WriteAllText(FileName, serializedData);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }
    }
}
