using InfiniTimer.Models.Timers;
using InfiniTimer.Services.JsonConverters;
using System.Text.Json;

namespace InfiniTimer.Services
{
    public class TimerService : ITimerService
    {
        private const string FileName = "InfiniTimerTimers.json";
        private string _filePath;
        private JsonSerializerOptions _serializerOptions;

        public TimerService()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _filePath = Path.Combine(docFolder, FileName);
            _serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new TimerModelConverter(),
                    new TimerSectionConverter()
                }
            };
        }

        public List<TimerModel> GetSavedTimers()
        {
            return GetTimersDictionary().Values.ToList();
        }

        public bool SaveTimer(TimerModel timer)
        {
            try
            {
                var savedTimers = GetTimersDictionary();

                if (timer.Id == Guid.Empty)
                {
                    timer.Id = Guid.NewGuid();
                }

                savedTimers[timer.Id] = timer;

                var serializedData = JsonSerializer.Serialize(savedTimers, _serializerOptions);
                File.WriteAllText(_filePath, serializedData);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        private Dictionary<Guid, TimerModel> GetTimersDictionary()
        {
            var rawData = string.Empty;
            

            if (File.Exists(_filePath))
            {
                rawData = File.ReadAllText(_filePath);
            }


            if (string.IsNullOrWhiteSpace(rawData))
            {
                return new Dictionary<Guid, TimerModel>();
            }
            else
            {
                return JsonSerializer.Deserialize<Dictionary<Guid, TimerModel>>(rawData, _serializerOptions);
            }
        }

        public bool DeleteTimers()
        {
            try
            {
                var rawData = string.Empty;

                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Encountered exception: " + ex.ToString());
                return false; 
            }

        }
    }
}
