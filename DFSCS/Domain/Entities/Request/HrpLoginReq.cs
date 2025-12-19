using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class HrpLoginReq
    {
        [Required(ErrorMessage = "User Id is required.")]
        public string? loginID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? loginPassword { get; set; }

        [Required(ErrorMessage = "Captcha is required.")]
        public string? captchaText { get; set; }
    }
}
