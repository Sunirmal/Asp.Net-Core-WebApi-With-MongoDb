using api.enums;
using api.Schemas;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace api.Entities
{
    public class Book: BaseModel
    {
        [JsonProperty(PropertyName = "title")]
        [BsonElement("title")]
        public string Title { get; set; }
        
        [JsonProperty(PropertyName = "author")]
        [BsonElement("author")]
        public string Author { get; set; }
        
        [JsonProperty(PropertyName = "url")]
        [BsonElement("url")]
        public string Url { get; set; }
        
        [JsonProperty(PropertyName = "info")]
        [BsonElement("info")]
        public string Info { get; set; }
        
        [JsonProperty(PropertyName = "image_url")]
        [BsonElement("image_url")]
        public string ImageUrl { get; set; }
        
        [JsonProperty(PropertyName = "status")]
        [BsonElement("status")]
        public BookType Status { get; set; }

        public ValidateResult Validate()
        {
            if (string.IsNullOrEmpty(Title))
            {
                return new ValidateResult(true, "Title is required");
            }

            if (string.IsNullOrEmpty(Author))
            {
                return new ValidateResult(true, "Author is required");
            }

            if (string.IsNullOrEmpty(Url))
            {
                return new ValidateResult(true, "Url is required");
            }

            if (string.IsNullOrEmpty(Info))
            {
                return new ValidateResult(true, "Info is required");
            }

            if (string.IsNullOrEmpty(ImageUrl))
            {
                return new ValidateResult(true, "Image Url is required");
            }

            if (string.IsNullOrEmpty(Status.ToString()))
            {
                Status = BookType.NONE;
            }
            
            return new ValidateResult(false, "");
        }
    }
}