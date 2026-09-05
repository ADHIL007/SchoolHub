using SchoolMgmtSystem.Models;

namespace SchoolMgmtSystem.BLL;

public interface IStudentService
{
    StudentInsertResult Register(Student student);

    List<StudentListItem> GetAll(string searchName);

    Student GetById(int studentId);

    LoginResult Login(string username, string password);
}
