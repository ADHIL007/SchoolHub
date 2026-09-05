using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SchoolMgmtSystem.BLL;

public class PasswordHasher
{
    public static string Hash(string plainTextPassword)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainTextPassword);

        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] hashBytes = sha1.ComputeHash(inputBytes);

            string hash = string.Concat(hashBytes.Select(b => b.ToString("x2")));
            return hash;
        }
    }
}
