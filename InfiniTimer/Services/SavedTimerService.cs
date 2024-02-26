using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common.Extensions;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services.JsonConverters;
using InfiniTimer.Services.Messages;
using System.Text.Json;

namespace InfiniTimer.Services
{
    public class SavedTimerService : ISavedTimerService
    {
        #region Private Variables
        private const string FileName = "InfiniTimerTimers.json";
        private string _filePath;
        private JsonSerializerOptions _serializerOptions;
        private Dictionary<Guid, TimerModel> _savedTimers;
        private Dictionary<Guid, TimerModel> _sessionTimers;
        #endregion

        #region Constructors
        public SavedTimerService()
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

            _savedTimers = GetTimersDictionary();
            _sessionTimers = _savedTimers.Copy();
        }
        #endregion

        #region ISavedTimerService Methods
        public void ResetTimer(Guid timerId)
        {
            _sessionTimers.Remove(timerId);
            _savedTimers.TryGetValue(timerId, out TimerModel timerModel);

            var timerCopy = timerModel.Copy();

            if (null != timerCopy)
            {
                _sessionTimers[timerId] = timerCopy;
                WeakReferenceMessenger.Default.Send(new TimerReplacedMessage(timerCopy));
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new TimerRemovedMessage(timerId));
            }
        }

        public bool DeleteTimer(Guid timerId)
        {
            try
            {
                _savedTimers.Remove(timerId);
                _sessionTimers.Remove(timerId);

                WeakReferenceMessenger.Default.Send(new TimerRemovedMessage(timerId));

                var serializedData = JsonSerializer.Serialize(_savedTimers, _serializerOptions);
                File.WriteAllText(_filePath, serializedData);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        public bool SaveTimers(List<Guid> timerIds)
        {
            try
            {
                foreach (var timerId in timerIds)
                {
                    if (timerId != Guid.Empty)
                    {
                        _sessionTimers.TryGetValue(timerId, out TimerModel timerModel);

                        if (null != timerModel)
                        {
                            timerModel.IsDirty = false;
                            _savedTimers[timerId] = timerModel.Copy();
                        }
                    }
                }

                var serializedData = JsonSerializer.Serialize(_savedTimers, _serializerOptions);
                File.WriteAllText(_filePath, serializedData);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        public bool DeleteAllTimers()
        {
            try
            {
                _sessionTimers.Clear();
                _savedTimers.Clear();

                WeakReferenceMessenger.Default.Send(new TimerRemovedMessage(Guid.Empty));

                var rawData = string.Empty;

                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encountered exception: " + ex.ToString());
                return false;
            }
        }

        public bool DeleteTimers(List<Guid> timerIds)
        {
            try
            {
                foreach (var timerId in timerIds)
                {
                    _savedTimers.Remove(timerId);
                    _sessionTimers.Remove(timerId);

                    WeakReferenceMessenger.Default.Send(new TimerRemovedMessage(timerId));
                }

                var serializedData = JsonSerializer.Serialize(_savedTimers, _serializerOptions);
                File.WriteAllText(_filePath, serializedData);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        public List<TimerModel> GetSessionTimers()
        {
            return _sessionTimers.Values.ToList();
        }

        public bool SaveTimer(Guid timerId)
        {
            try
            {
                if (timerId == Guid.Empty)
                {
                    return false;
                }

                _sessionTimers.TryGetValue(timerId, out var timer);

                if (timer == null)
                {
                    return false;
                }

                timer.IsDirty = false;
                _savedTimers[timerId] = timer.Copy();

                var serializedData = JsonSerializer.Serialize(_savedTimers, _serializerOptions);
                File.WriteAllText(_filePath, serializedData);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        public void AddSessionTimer(TimerModel timer)
        {
            if (timer == null)
                return;

            if (timer.Id == Guid.Empty)
            {
                timer.Id = Guid.NewGuid();
            }

            _sessionTimers[timer.Id] = timer;
            WeakReferenceMessenger.Default.Send(new TimerAddedMessage(timer));

        }
        #endregion

        #region Private Methods
        private Dictionary<Guid, TimerModel> GetTimersDictionary()
        {
            try
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return new Dictionary<Guid, TimerModel>();
            }
        }
        #endregion
    }
}
