using Microsoft.Data.SqlClient;
using System.Data;
using SchoolMgmtSystem.Models;

namespace SchoolMgmtSystem.DAL;

public class StudentRepository : IStudentRepository
{
    private readonly string connStr;

    public StudentRepository(string connectionString)
    {
        connStr = connectionString;
    }

    public StudentInsertResult Insert(Student student)
    {
        string qualificationXml = null;
        if (student.Qualifications != null && student.Qualifications.Count > 0)
        {
            qualificationXml = QualificationXmlBuilder.Build(student.Qualifications);
        }

        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("dbo.InsertStudent", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", (object)student.LastName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Age", student.Age);
            cmd.Parameters.AddWithValue("@DOB", student.DOB);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Phone", student.Phone);
            cmd.Parameters.AddWithValue("@Username", student.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", student.Password);
            cmd.Parameters.AddWithValue("@QualificationsXml", (object)qualificationXml ?? DBNull.Value);

            SqlParameter studentIdOut = new SqlParameter("@StudentId", SqlDbType.Int);
            studentIdOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(studentIdOut);

            SqlParameter outStudentCode = new SqlParameter("@StudentCode", SqlDbType.NVarChar, 20);
            outStudentCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outStudentCode);

            SqlParameter outReturnCode = new SqlParameter("@ReturnCode", SqlDbType.Int);
            outReturnCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outReturnCode);

            con.Open();
            cmd.ExecuteNonQuery();

            StudentInsertResult result = new StudentInsertResult();
            result.Status = (int)outReturnCode.Value;

            if (studentIdOut.Value != DBNull.Value)
            {
                result.StudentId = (int)studentIdOut.Value;
            }

            if (outStudentCode.Value != DBNull.Value)
            {
                result.StudentCode = (string)outStudentCode.Value;
            }

            return result;
        }
    }

    public List<StudentListItem> GetAll()
    {
        List<StudentListItem> list = new List<StudentListItem>();

        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("dbo.GetAllStudents", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapListItem(reader));
            }
            reader.Close();
        }

        return list;
    }

    public Student GetById(int studentId)
    {
        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("dbo.GetStudentById", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                reader.Close();
                return null;
            }

            StudentListItem item = MapListItem(reader);

            Student student = new Student();
            student.StudentId = item.StudentId;
            student.StudentCode = item.StudentCode;
            student.FirstName = item.FirstName;
            student.LastName = item.LastName;
            student.Age = item.Age;
            student.DOB = item.DOB;
            student.Gender = item.Gender;
            student.Email = item.Email;
            student.Phone = item.Phone;
            student.Username = item.Username;
            student.Qualifications = new List<Qualification>();

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    Qualification q = new Qualification();
                    q.QualificationId = (int)reader["QualificationId"];
                    q.StudentId = studentId;
                    q.CourseName = (string)reader["CourseName"];
                    q.University = (string)reader["University"];
                    q.PassingYear = (int)reader["PassingYear"];
                    q.Percentage = (decimal)reader["Percentage"];

                    student.Qualifications.Add(q);
                }
            }

            reader.Close();
            return student;
        }
    }

    public LoginResult ValidateLogin(string username, string passwordHash)
    {
        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("dbo.ValidateLogin", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            LoginResult result = new LoginResult();

            if (reader.Read())
            {
                result.Success = true;
                result.StudentId = (int)reader["StudentId"];
                result.StudentCode = reader["StudentCode"] as string;
                result.FirstName = reader["FirstName"] as string;
                result.LastName = reader["LastName"] as string;
                result.Username = reader["Username"] as string;
            }
            else
            {
                result.Success = false;
            }

            reader.Close();
            return result;
        }
    }

    private StudentListItem MapListItem(SqlDataReader reader)
    {
        StudentListItem item = new StudentListItem();
        item.StudentId = (int)reader["StudentId"];
        item.StudentCode = reader["StudentCode"] as string;
        item.FirstName = (string)reader["FirstName"];
        item.LastName = reader["LastName"] as string;
        item.Age = (int)reader["Age"];
        item.DOB = (DateTime)reader["DOB"];
        item.Gender = (string)reader["Gender"];
        item.Email = (string)reader["Email"];
        item.Phone = (string)reader["Phone"];
        item.Username = (string)reader["Username"];
        item.CreatedDate = (DateTime)reader["CreatedDate"];
        item.QualificationCount = (int)reader["QualificationCount"];
        return item;
    }
}
