using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionEventsConverter : JsonConverter<List<DomainEvent>>
    {
        public static List<DomainEvent> Read(string json)
        {
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;

                List<DomainEvent> events = new List<DomainEvent>();

                foreach (JsonElement element in root.EnumerateArray())
                {
                    string eventTypeProperty = "";
                    if (element.TryGetProperty("StartDate", out var g))
                        eventTypeProperty = "TourExecutionStarted";
                    else if (element.TryGetProperty("Date", out var h))
                        eventTypeProperty = "TourExecutionActivityRegistered";
                    else if (element.TryGetProperty("EndDate", out var j))
                        eventTypeProperty = "TourExecutionFinished";
                    // Check if the "eventType" property exists in the JSON element
                    if (eventTypeProperty != "")
                    {
                        string eventType = eventTypeProperty;

                        switch (eventType)
                        {
                            case "TourExecutionActivityRegistered":
                                events.Add(JsonSerializer.Deserialize<TourExecutionActivityRegistered>(element.GetRawText()));
                                break;
                            case "TourExecutionStarted":
                                events.Add(JsonSerializer.Deserialize<TourExecutionStarted>(element.GetRawText()));
                                break;
                            case "TourExecutionFinished":
                                events.Add(JsonSerializer.Deserialize<TourExecutionFinished>(element.GetRawText()));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        // Handle the case when the "eventType" property is missing
                        // You can log a warning or take appropriate action based on your requirements.
                    }
                }

                return events;
            }
        }


        public static string Write(List<DomainEvent> value)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    writer.WriteStartArray();

                    foreach (var domainEvent in value)
                    {
                        JsonSerializer.Serialize(writer, domainEvent, domainEvent.GetType());
                    }

                    writer.WriteEndArray();
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
        public override List<DomainEvent> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<DomainEvent> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
