using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniTimer.Services.JsonConverters
{
    public class TimerSectionConverter : JsonConverter<TimerSection>
    {
        enum TypeDiscriminator
        {
            SingleTimer = 1,
            TimerList = 2,
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(TimerSection).IsAssignableFrom(typeToConvert);

        public override TimerSection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            string propertyName = reader.GetString();
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
            TimerSection timerSection = typeDiscriminator switch
            {
                TypeDiscriminator.SingleTimer => new SingleTimerSection(),
                TypeDiscriminator.TimerList => new TimerListSection(),
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
                        case "Sound":
                            TimerSound sound = (TimerSound)reader.GetInt32();
                            ((SingleTimerSection)timerSection).Sound = sound;
                            break;
                        case "Vibrate":
                            bool vibrate = reader.GetBoolean();
                            ((SingleTimerSection)timerSection).Vibrate = vibrate;
                            break;
                        case "Cycles":
                            int cycles = reader.GetInt32();
                            ((TimerListSection)timerSection).Cycles = cycles;
                            break;
                        case "TimerSections":
                            if (reader.TokenType != JsonTokenType.StartArray)
                            {
                                throw new JsonException();
                            }
                            reader.Read();

                            ObservableCollection<TimerSection> timerSections = new();

                            while (reader.TokenType != JsonTokenType.EndArray)
                            {
                                timerSections.Add(JsonSerializer.Deserialize<TimerSection>(ref reader, options)!);

                                reader.Read();
                            }

                            ((TimerListSection)timerSection).TimerSections = timerSections;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, TimerSection timerSection, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (timerSection is SingleTimerSection singleTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.SingleTimer);
                writer.WriteString("DisplayText", singleTimerSection.DisplayText);
                writer.WriteNumber("Seconds", singleTimerSection.Seconds);
                writer.WriteNumber("Color", (int)singleTimerSection.Color);
                writer.WriteBoolean("Vibrate", singleTimerSection.Vibrate);
                writer.WriteNumber("Sound", (int)singleTimerSection.Sound);
            }
            else if (timerSection is TimerListSection timerListSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.TimerList);
                writer.WriteNumber("Cycles", timerListSection.Cycles);
                writer.WritePropertyName("TimerSections");
                writer.WriteStartArray();

                foreach (TimerSection section in timerListSection.TimerSections)
                {
                    JsonSerializer.Serialize(writer, section, typeof(TimerSection), options);
                }

                writer.WriteEndArray();
            }

            writer.WriteNumber("Depth", timerSection.Depth);

            writer.WriteEndObject();
        }
    }
}
