using System.ComponentModel.DataAnnotations;

namespace SchoolMgmtSystem.ViewModels;

public class QualificationViewModel
{
    [Required]
    [Display(Name = "Course")]
    public string CourseName { get; set; }

    [Required]
    [Display(Name = "University")]
    public string University { get; set; }

    [Required]
    [Range(1950, 2100)]
    [Display(Name = "Year")]
    public int PassingYear { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal Percentage { get; set; }
}
