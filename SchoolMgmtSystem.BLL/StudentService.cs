using System.Linq;
using SchoolMgmtSystem.DAL;
using SchoolMgmtSystem.Models;

namespace SchoolMgmtSystem.BLL;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public StudentInsertResult Register(Student student)
    {
        student.Password = PasswordHasher.Hash(student.Password);

        return _studentRepository.Insert(student);
    }

    public List<StudentListItem> GetAll(string searchName)
    {
        List<StudentListItem> allStudents = _studentRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchName))
        {
            return allStudents;
        }

        return allStudents
            .Where(s => s.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                     || (s.LastName != null && s.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                     || s.StudentCode.Contains(searchName, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Student GetById(int studentId)
    {
        return _studentRepository.GetById(studentId);
    }

    public LoginResult Login(string username, string password)
    {
        string passwordHash = PasswordHasher.Hash(password);
        return _studentRepository.ValidateLogin(username, passwordHash);
    }
}
