using System.ComponentModel.DataAnnotations;
using Entities;
using web_api.Models.City;

namespace web_api.Models.Neighborhood
{
    public class NeighborhoodRegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public CityRegisterDTO City { get; set; }
    }
}
