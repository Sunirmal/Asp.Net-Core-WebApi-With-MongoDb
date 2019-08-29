using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace api.Entities
{
    public abstract class BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}