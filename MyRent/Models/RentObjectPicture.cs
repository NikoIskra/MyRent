using System.Text.Json.Serialization;

namespace MyRent.Models
{
    public class RentObjectPicture
    {
        [JsonPropertyName("picture_link")]
        public string PictureLink { get; set; }
    }
}
