using System.ComponentModel.DataAnnotations;

namespace GestionBanquaire.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string AccountStatus { get; set; }

        [Required]
        public decimal Balance { get; set; }

        
    }
}
