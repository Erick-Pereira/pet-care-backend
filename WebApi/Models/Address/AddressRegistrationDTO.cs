using System.ComponentModel.DataAnnotations;
using Entities;
using web_api.Models.Neighborhood;

namespace web_api.Models.Address
{
    public class AddressRegistrationDTO
    {
        [Required]
        public string Street { get; set; }
        public string? Complement { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public NeighborhoodRegisterDTO Neighborhood { get; set; }
    }
}
