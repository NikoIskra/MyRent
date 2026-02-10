using System.Text.Json.Serialization;

namespace MyRent.Models
{
    public class RentObject
    {
        [JsonPropertyName("id_hash")]
        public string IdHash { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("picutre_main_url")]
        public string PictureMainUrl { get; set; }
        [JsonPropertyName("instant_booking")]
        public string InstanBooking { get; set; }
        [JsonPropertyName("pay_card")]
        public string PayCard { get; set; }
        [JsonPropertyName("pay_casche")]
        public string PayCash { get; set; }
        [JsonPropertyName("pay_iban")]
        public string PayIban { get; set; }
        [JsonPropertyName("object_type")]
        public string ObjectType { get; set; }
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("adress")]
        public string Address { get; set; }
        [JsonPropertyName("city_name")]
        public string CityName { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("can_sleep_optimal")]
        public int CanSleepOptimal { get; set; }
        [JsonPropertyName("can_sleep_max")]
        public int CanSleepMax { get; set; }
        [JsonPropertyName("beds")]
        public double Beds { get; set; }
        [JsonPropertyName("bathrooms")]
        public double Bathrooms { get; set; }
        [JsonPropertyName("bedrooms")]
        public double Bedrooms { get; set; }
        [JsonPropertyName("check_in")]
        public string CheckIn { get; set; }
        [JsonPropertyName("check_out")]
        public string CheckOut { get; set; }
        [JsonPropertyName("classification_star")]
        public int ClassificationStar { get; set; }
        public List<RentObjectPicture> gallery { get; set; } = new List<RentObjectPicture>();
    }
}
