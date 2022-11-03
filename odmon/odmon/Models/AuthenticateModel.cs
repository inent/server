using System.ComponentModel.DataAnnotations;

namespace odmon.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string userid { get; set; }

        [Required]
        public string userpw { get; set; }
    }
}