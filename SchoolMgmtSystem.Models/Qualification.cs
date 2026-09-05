namespace SchoolMgmtSystem.Models;

public class Qualification
{
    public int QualificationId { get; set; }
    public int StudentId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public int PassingYear { get; set; }
    public decimal Percentage { get; set; }
}
