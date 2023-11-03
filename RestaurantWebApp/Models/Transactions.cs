using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantWebApp.Models
{
    public class Transactions
    {
        [Key]
        public int transactionId { get; set; }
        public int customerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        public int foodId { get; set; }

        [JsonIgnore]
        public Food Food { get; set; }

        public int totalPrice { get; set; }
        public DateTime transactionDate { get; set; }
    }
}