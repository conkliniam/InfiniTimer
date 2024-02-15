using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniTimer.Services.JsonConverters
{
    public class TimerSectionConverter : JsonConverter<ITimerSection>
    {
        enum TypeDiscriminator
        {
            SingleTimer = 1,
            AlternatingTimers = 2,
            SequentialTimers = 3,
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(ITimerSection).IsAssignableFrom(typeToConvert);

        public override ITimerSection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            ITimerSection timerSection = typeDiscriminator switch
            {
                TypeDiscriminator.SingleTimer => new SingleTimerSection(),
                TypeDiscriminator.AlternatingTimers => new AlternatingTimerSection(),
                TypeDiscriminator.SequentialTimers => new SequentialTimerSection(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return timerSection;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "Depth":
                            int depth = reader.GetInt32();
                            timerSection.Depth = depth;
                            break;
                        case "DisplayText":
                            string displayText = reader.GetString();
                            ((SingleTimerSection)timerSection).DisplayText = displayText;
                            break;
                        case "Seconds":
                            int seconds = reader.GetInt32();
                            ((SingleTimerSection)timerSection).Seconds = seconds;
                            break;
                        case "Color":
                            TimerColor color = (TimerColor)reader.GetInt32();
                            ((SingleTimerSection)timerSection).Color = color;
                            break;
                        case "MainTimerSection":
                            ITimerSection mainTimerSection = (ITimerSection)JsonSerializer.Deserialize(ref reader, typeof(ITimerSection), options);
                            ((AlternatingTimerSection)timerSection).MainTimerSection = mainTimerSection;
                            break;
                        case "AlternateTimerSection":
                            ITimerSection alternateTimerSection = (ITimerSection)JsonSerializer.Deserialize(ref reader, typeof(ITimerSection), options);
                            ((AlternatingTimerSection)timerSection).AlternateTimerSection = alternateTimerSection;
                            break;
                        case "Cycles":
                            int cycles = reader.GetInt32();
                            ((AlternatingTimerSection)timerSection).Cycles = cycles;
                            break;
                        case "TimerSections":
                            if (reader.TokenType != JsonTokenType.StartArray)
                            {
                                throw new JsonException();
                            }
                            reader.Read();

                            ObservableCollection<ITimerSection> timerSections = new();

                            while (reader.TokenType != JsonTokenType.EndArray)
                            {
                                timerSections.Add(JsonSerializer.Deserialize<ITimerSection>(ref reader, options)!);

                                reader.Read();
                            }

                            ((SequentialTimerSection)timerSection).TimerSections = timerSections;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, ITimerSection timerSection, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (timerSection is SingleTimerSection singleTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.SingleTimer);
                writer.WriteString("DisplayText", singleTimerSection.DisplayText);
                writer.WriteNumber("Seconds", singleTimerSection.Seconds);
                writer.WriteNumber("Color", (int)singleTimerSection.Color);
            }
            else if (timerSection is AlternatingTimerSection alternatingTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.AlternatingTimers);
                writer.WritePropertyName("MainTimerSection");
                JsonSerializer.Serialize(writer, alternatingTimerSection.MainTimerSection, typeof(ITimerSection), options);
                writer.WritePropertyName("AlternateTimerSection");
                JsonSerializer.Serialize(writer, alternatingTimerSection.AlternateTimerSection, typeof(ITimerSection), options);
                writer.WriteNumber("Cycles", alternatingTimerSection.Cycles);
            }
            else if (timerSection is SequentialTimerSection sequentialTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.SequentialTimers);
                writer.WritePropertyName("TimerSections");
                writer.WriteStartArray();

                foreach (ITimerSection section in sequentialTimerSection.TimerSections)
                {
                    JsonSerializer.Serialize(writer, section, typeof(ITimerSection), options);
                }

                writer.WriteEndArray();
            }

            writer.WriteNumber("Depth", timerSection.Depth);

            writer.WriteEndObject();
        }
    }
}
