using System.ComponentModel.DataAnnotations;

namespace DonateMe.Web.Models
{
    public class ProductModel
    {
        [Required(ErrorMessage = "*Please enter a name")]
        public string Name { get; set; }
    }
}