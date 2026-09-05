namespace SchoolMgmtSystem.Models;

public class LoginResult
{
    public bool Success { get; set; }
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
}
