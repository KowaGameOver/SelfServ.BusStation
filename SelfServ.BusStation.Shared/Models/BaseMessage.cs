using System.Text.Json.Serialization;

namespace SelfServ.BusStation.Shared.Models
{
    public class BaseMessage<T>
    {
        public string MessageType { get; }

        [JsonPropertyName("payload")]
        public T Payload { get; }
        public string HandlerType { get; }  
        public BaseMessage(T payload, string handlerType)
        {
            MessageType = typeof(T).Name;
            Payload = payload;
            HandlerType = handlerType;
        }
    }
}
