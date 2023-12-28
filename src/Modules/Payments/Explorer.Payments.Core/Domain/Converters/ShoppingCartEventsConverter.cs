using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Explorer.Payments.Core.Domain.ShoppingSession;

namespace Explorer.Payments.Core.Domain.Converters;

public class ShoppingCartEventsConverter : JsonConverter<List<DomainEvent>>
{
    public static List<DomainEvent> Read(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var events = new List<DomainEvent>();

        foreach (var element in root.EnumerateArray())
        {
            var eventTypeProperty = "";

            if (element.TryGetProperty("SessionStarted", out _))
                eventTypeProperty = "ShoppingSessionStarted";

            else if (element.TryGetProperty("SessionAbandoned", out _))
                eventTypeProperty = "ShoppingSessionAbandoned";

            else if (element.TryGetProperty("SessionEnded", out _))
                eventTypeProperty = "ShoppingSessionEnded";

            else if (element.TryGetProperty("CouponUsed", out _))
                eventTypeProperty = "ShoppingCouponUsed";

            else if (element.TryGetProperty("TourAdded", out _))
                eventTypeProperty = "TourAddedToCart";

            else if (element.TryGetProperty("TourRemoved", out _))
                eventTypeProperty = "TourRemovedFromCart";

            else if (element.TryGetProperty("BundleAdded", out _))
                eventTypeProperty = "BundleAddedToCart";

            else if (element.TryGetProperty("BundleRemoved", out _))
                eventTypeProperty = "BundleRemovedFromCart";

            if (eventTypeProperty != "")
            {
                switch (eventTypeProperty)
                {
                    case "ShoppingSessionStarted":
                        events.Add(JsonSerializer.Deserialize<ShoppingSessionStarted>(element.GetRawText())!);
                        break;
                    case "ShoppingSessionAbandoned":
                        events.Add(JsonSerializer.Deserialize<ShoppingSessionAbandoned>(element.GetRawText())!);
                        break;
                    case "ShoppingSessionEnded":
                        events.Add(JsonSerializer.Deserialize<ShoppingSessionEnded>(element.GetRawText())!);
                        break;
                    case "ShoppingCouponUsed":
                        events.Add(JsonSerializer.Deserialize<ShoppingCouponUsed>(element.GetRawText())!);
                        break;
                    case "TourAddedToCart":
                        events.Add(JsonSerializer.Deserialize<TourAddedToCart>(element.GetRawText())!);
                        break;
                    case "TourRemovedFromCart":
                        events.Add(JsonSerializer.Deserialize<TourRemovedFromCart>(element.GetRawText())!);
                        break;
                    case "BundleAddedToCart":
                        events.Add(JsonSerializer.Deserialize<BundleAddedToCart>(element.GetRawText())!);
                        break;
                    case "BundleRemovedFromCart":
                        events.Add(JsonSerializer.Deserialize<BundleRemovedFromCart>(element.GetRawText())!);
                        break;
                }
            }
        }

        return events;
    }


    public static string Write(List<DomainEvent> value)
    {
        using var stream = new MemoryStream();
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

    public override List<DomainEvent> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<DomainEvent> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}