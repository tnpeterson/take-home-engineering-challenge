using food_truck_finder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoCoordinatePortable;

namespace food_truck_finder.Controllers
{
    [Route("api/[controller]/[action]")]

    public class FoodTruckController : Controller
    {
        private readonly FoodTruckContext _context;

        public FoodTruckController(FoodTruckContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public Object? GetAll()
        {
            var foodTrucks = _context.Trucks;

            if (foodTrucks == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(foodTrucks)).Value;
        }

        [HttpGet()]
        public Object? GetTruckByLocation(Double latitude, Double longitude)
        {
            var foodTrucks = _context.Trucks;

            if (foodTrucks == null)
            {
                return new JsonResult(NotFound());
            }

            var currentLocation = new GeoCoordinate(latitude, longitude);

            foreach (FoodTrucks foodTruck in foodTrucks)
            {
                var foodTruckLocation = new GeoCoordinate(foodTruck.latitude, foodTruck.longitude);
                var distance = currentLocation.GetDistanceTo(foodTruckLocation);
                foodTruck.distanceFromCurrentLocation = distance;
            }

            var closestFoodTrucks = foodTrucks.ToList().OrderBy(foodTruck => foodTruck.distanceFromCurrentLocation).Take(5);

            return new JsonResult(Ok(closestFoodTrucks)).Value;
        }


    }
}

