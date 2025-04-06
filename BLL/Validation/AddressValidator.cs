using Entities;

namespace BLL.Validation
{
    public static class AddressValidator
    {
        public static void Validate(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(address.Street))
            {
                throw new ArgumentException("Street cannot be empty.", nameof(address.Street));
            }

            if (string.IsNullOrWhiteSpace(address.Neighborhood.City.Name))
            {
                throw new ArgumentException("City cannot be empty.", nameof(address.Neighborhood.City.Name));
            }

            if (string.IsNullOrWhiteSpace(address.Neighborhood.City.State.Name))
            {
                throw new ArgumentException("State cannot be empty.", nameof(address.Neighborhood.City.State.Name));
            }

            if (string.IsNullOrWhiteSpace(address.ZipCode))
            {
                throw new ArgumentException("Zip code cannot be empty.", nameof(address.ZipCode));
            }
        }
    }
}