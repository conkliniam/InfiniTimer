using InfiniTimer.Models.Timers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniTimer.Services.JsonConverters
{
    public class TimerModelConverter : JsonConverter<TimerModel>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(TimerModel).IsAssignableFrom(typeToConvert);

        public override TimerModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;

            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new TimerSectionConverter()
                }
            };

            TimerModel timerModel = JsonSerializer.Deserialize<TimerModel>(ref reader, serializeOptions)!;

            timerModel.IsDirty = false;
            timerModel.IgnoreChanges = false;

            return timerModel;
        }

        public override void Write(Utf8JsonWriter writer, TimerModel timerModel, JsonSerializerOptions options)
        {
            timerModel.IgnoreChanges = true;

            writer.WriteStartObject();

            writer.WritePropertyName("Timers");
            JsonSerializer.Serialize(writer, timerModel.Timers, typeof(TimerListSection), options);
            writer.WriteString("Description", timerModel.Description);
            writer.WriteBoolean("AutoContinue", timerModel.AutoContinue);
            writer.WriteBoolean("AutoRepeat", timerModel.AutoRepeat);
            writer.WriteString("Id", timerModel.Id);
            writer.WriteString("Name", timerModel.Name);
            writer.WriteBoolean("IsDirty", false);
            writer.WriteBoolean("IsStaged", timerModel.IsStaged);

            writer.WriteEndObject();
        }
    }
}
