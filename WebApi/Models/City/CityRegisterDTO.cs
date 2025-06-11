using System.ComponentModel.DataAnnotations;
using Entities;
using web_api.Models.State;

namespace web_api.Models.City
{
    public class CityRegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public StateRegisterDTO State { get; set; }
    }
}
