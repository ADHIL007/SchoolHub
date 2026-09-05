using System.ComponentModel.DataAnnotations;

namespace SchoolMgmtSystem.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(1, 120)]
    public int? Age { get; set; }

    [Required(ErrorMessage = "DOB is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime? DOB { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [Display(Name = "Email Id")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    [Display(Name = "Phone Number")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    public List<QualificationViewModel> Qualifications { get; set; } = new List<QualificationViewModel>();
}
