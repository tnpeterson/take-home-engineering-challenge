using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace food_truck_finder
{
    public class FoodTrucks
    {
        public string? objectid { get; set; }
        public string? applicant { get; set; }
        public string? facilitytype { get; set; }
        public string? cnn { get; set; }
        public string? locationdescription { get; set; }
        public string? address { get; set; }
        public string? blocklot { get; set; }
        public string? block { get; set; }
        public string? lot { get; set; }
        public string? permit { get; set; }
        public string? status { get; set; }
        public string? x { get; set; }
        public string? y { get; set; }
        public Double latitude { get; set; }
        public Double longitude { get; set; }
        public string? schedule { get; set; }
        public string? received { get; set; }
        public string? priorpermit { get; set; }
        public DateTime? expirationdate { get; set; }
        //public Location? location { get; set; }

        [JsonProperty(":id")]
        public string id { get; set; } = null!;
        public string? fooditems { get; set; }
        public DateTime? approved { get; set; }
        public string? dayshours { get; set; }

        public double distanceFromCurrentLocation { get; set; }
    }

    //[Keyless]
    //public class Location
    //{
    //    [NotMapped]
    //    public string? latitude { get; set; }
    //    [NotMapped]
    //    public string? longitude { get; set; }
    //    [NotMapped]
    //    public string? human_address { get; set; }
    //}

}

