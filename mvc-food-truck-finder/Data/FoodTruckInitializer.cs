using System;
using mvc_food_truck_finder.Models;
using Newtonsoft.Json;

namespace mvc_food_truck_finder.Data
{
	public class FoodTruckInitializer
	{
		public FoodTruckInitializer()
		{
		}

        public static async Task SeedTrucks(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FoodTruckContext>();

                if (context != null && !context.Trucks.Any())
                {
                    List<FoodTrucks>? trucks = null;
                    string baseUrl = "https://data.sfgov.org/api/id/rqzj-sfat.json?$query=select%20*,%20:id";
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(baseUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        trucks = await response.Content.ReadAsAsync<List<FoodTrucks>>();
                    }

                    if (trucks != null)
                    {
                        context.Trucks.AddRange(trucks);
                        context.SaveChanges();
                    }
                }
            }
        }

    }
}

