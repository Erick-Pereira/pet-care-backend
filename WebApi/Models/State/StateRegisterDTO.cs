using System.ComponentModel.DataAnnotations;

namespace web_api.Models.State
{
    public class StateRegisterDTO
    {
        [Required]
        public string Abbreviation { get; set; }
    }
}