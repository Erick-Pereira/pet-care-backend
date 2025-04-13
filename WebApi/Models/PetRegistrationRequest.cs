using Entities;

namespace web_api.Models
{
    public class PetRegistrationRequest
    {
        public Pet Pet { get; set; }
        public User Owner { get; set; }

        public Entities.PetRegistrationRequest ToEntity()
        {
            return new Entities.PetRegistrationRequest
            {
                Pet = this.Pet,
                Owner = this.Owner
            };
        }
    }
}