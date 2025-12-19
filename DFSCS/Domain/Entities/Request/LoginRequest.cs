using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class LoginRequest
    {

        [Required(ErrorMessage = "User Id is required.")]
        public string? loginID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? loginPassword { get; set; }
    }
}
