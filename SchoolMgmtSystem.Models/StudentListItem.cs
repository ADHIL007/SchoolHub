namespace SchoolMgmtSystem.Models;

public class StudentListItem
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime DOB { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public DateTime CreatedDate { get; set; }
    public int QualificationCount { get; set; }
}
