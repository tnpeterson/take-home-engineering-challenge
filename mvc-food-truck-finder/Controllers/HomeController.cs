using System.Diagnostics;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using mvc_food_truck_finder.Models;

namespace mvc_food_truck_finder.Controllers;

public class HomeController : Controller
{
    private FoodTruckContext _context = new FoodTruckContext();

    public HomeController(FoodTruckContext context)
    {
        _context = context;
    }

    public IActionResult Index(string searchLatitude, string searchLongitude)
    {
        //ToDo Add More Error Handling
        Console.WriteLine(searchLatitude);
        Console.WriteLine(searchLongitude);
        if (!String.IsNullOrEmpty(searchLatitude) && !String.IsNullOrEmpty(searchLongitude))
        {
            var foodTrucks = _context.Trucks;
            var latitude = Convert.ToDouble(searchLatitude);
            var longitude = Convert.ToDouble(searchLongitude);

            var currentLocation = new GeoCoordinate(latitude, longitude);

            foreach (FoodTrucks foodTruck in foodTrucks)
            {
                var foodTruckLocation = new GeoCoordinate(foodTruck.latitude, foodTruck.longitude);
                var distance = currentLocation.GetDistanceTo(foodTruckLocation);
                foodTruck.distanceFromCurrentLocation = distance;
            }

            var closestFoodTrucks = foodTrucks.ToList().OrderBy(foodTruck => foodTruck.distanceFromCurrentLocation).Take(5);
            return View(closestFoodTrucks);
        }
        else
        {
            return View(_context.Trucks.ToList().OrderBy(foodTruck => foodTruck.applicant));
        }
    }

    [HttpPost]
    public string Index(FormCollection fc, string searchLatitude, string searchLongitude)
    {
        return "<h3> From [HttpPost]Index: " + searchLatitude + " " + searchLongitude + "</h3>";
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

