using System;
using Newtonsoft.Json;

namespace food_truck_finder.Data
{
	public class FoodTruckInitializer
	{
		public FoodTruckInitializer()
		{
		}

        public static async Task SeedTrucks(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
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

