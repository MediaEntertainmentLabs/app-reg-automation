using System;
using System.ComponentModel.DataAnnotations;

namespace AppRegShared.Model
{
    public class AppRegistrationRequest : DataModelBase
    {
        [Required]
        public Guid RequestId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required.")]
        public string? RequestorUserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id is required")]
        public string? RequestorUserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "User email is required")]
        public string? RequestorEmailAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Application name is required")]
        public string? ApplicationName { get; set; }
    }
}
