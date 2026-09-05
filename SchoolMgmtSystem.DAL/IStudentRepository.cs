using SchoolMgmtSystem.Models;

namespace SchoolMgmtSystem.DAL;

public interface IStudentRepository
{
    StudentInsertResult Insert(Student student);

    List<StudentListItem> GetAll();

    Student GetById(int studentId);

    LoginResult ValidateLogin(string username, string passwordHash);
}
