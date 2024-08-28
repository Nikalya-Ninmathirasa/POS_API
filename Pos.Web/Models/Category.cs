using System.ComponentModel.DataAnnotations;

namespace Pos.Web.Models
{
    public class Category
    {
        // create model with fields
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        // ICollection for one to many relationship
        public ICollection<Product> Products { get; set; }
    }
}
