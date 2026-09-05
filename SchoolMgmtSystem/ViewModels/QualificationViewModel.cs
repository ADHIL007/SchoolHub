using System.ComponentModel.DataAnnotations;

namespace SchoolMgmtSystem.ViewModels;

public class QualificationViewModel : IValidatableObject
{
    [Display(Name = "Course")]
    public string CourseName { get; set; }

    [Display(Name = "University")]
    public string University { get; set; }

    [Range(1950, 2100, ErrorMessage = "Year must be between 1950 and 2100")]
    [Display(Name = "Year")]
    public int? PassingYear { get; set; }

    [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
    public decimal? Percentage { get; set; }

    public bool IsEmpty =>
        string.IsNullOrWhiteSpace(CourseName) &&
        string.IsNullOrWhiteSpace(University) &&
        PassingYear == null &&
        Percentage == null;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsEmpty)
        {
            yield break;
        }

        if (string.IsNullOrWhiteSpace(CourseName))
        {
            yield return new ValidationResult("Course is required", [nameof(CourseName)]);
        }

        if (string.IsNullOrWhiteSpace(University))
        {
            yield return new ValidationResult("University is required", [nameof(University)]);
        }

        if (PassingYear == null)
        {
            yield return new ValidationResult("Year is required", [nameof(PassingYear)]);
        }

        if (Percentage == null)
        {
            yield return new ValidationResult("Percentage is required", [nameof(Percentage)]);
        }
    }
}
