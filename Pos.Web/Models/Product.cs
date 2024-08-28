using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pos.Web.Models
{
    public class Product   
    {
       // create model with fields 
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }


        [JsonIgnore]
        public Category Category { get; set; }


    }
}
