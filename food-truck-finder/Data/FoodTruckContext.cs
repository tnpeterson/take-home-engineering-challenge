using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace food_truck_finder
{
	public class FoodTruckContext : DbContext
	{
		public DbSet<FoodTrucks> Trucks { get; set; }
        public FoodTruckContext(DbContextOptions<FoodTruckContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FoodTrucks>();

			base.OnModelCreating(modelBuilder);
		}
	}
}

