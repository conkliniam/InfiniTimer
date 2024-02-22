using InfiniTimer.Models.Timers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniTimer.Services.JsonConverters
{
    public class TimerModelConverter : JsonConverter<TimerModel>
    {
        enum TypeDiscriminator
        {
            SimpleTimer = 1,
            AdvancedTimer = 2
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(TimerModel).IsAssignableFrom(typeToConvert);

        public override TimerModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;

            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = readerClone.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)readerClone.GetInt32();

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new TimerSectionConverter()
                }
            };

            TimerModel timerModel = typeDiscriminator switch
            {
                TypeDiscriminator.SimpleTimer => JsonSerializer.Deserialize<SimpleTimerModel>(ref reader, serializeOptions)!,
                TypeDiscriminator.AdvancedTimer => JsonSerializer.Deserialize<AdvancedTimerModel>(ref reader, serializeOptions)!,
                _ => throw new JsonException()
            };

            timerModel.IgnoreChanges = false;

            return timerModel;
        }

        public override void Write(Utf8JsonWriter writer, TimerModel timerModel, JsonSerializerOptions options)
        {
            timerModel.IgnoreChanges = true;

            writer.WriteStartObject();

            if (timerModel is SimpleTimerModel simpleTimerModel)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.SimpleTimer);
                writer.WritePropertyName("Timer");
                JsonSerializer.Serialize(writer, simpleTimerModel.Timer, typeof(SingleTimerSection), options);
            }
            else if (timerModel is AdvancedTimerModel advancedTimerModel)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.AdvancedTimer);
                writer.WritePropertyName("TimerSection");
                JsonSerializer.Serialize(writer, advancedTimerModel.TimerSection, typeof(TimerSection), options);
                writer.WriteString("Description", advancedTimerModel.Description);
                writer.WriteBoolean("AutoContinue", advancedTimerModel.AutoContinue);
                writer.WriteBoolean("AutoRepeat", advancedTimerModel.AutoRepeat);
            }

            writer.WriteString("Id", timerModel.Id);
            writer.WriteString("Name", timerModel.Name);
            writer.WriteBoolean("IsDirty", timerModel.IsDirty);

            timerModel.IgnoreChanges = false;

            writer.WriteEndObject();
        }
    }
}
