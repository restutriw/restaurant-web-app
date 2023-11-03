﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantWebApp.Models
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options)
            : base(options)
        {

        }

        public DbSet<Food> Food { get; set; }
    }
}