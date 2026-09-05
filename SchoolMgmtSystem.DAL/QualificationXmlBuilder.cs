using System.Linq;
using System.Xml.Linq;
using SchoolMgmtSystem.Models;

namespace SchoolMgmtSystem.DAL;

public class QualificationXmlBuilder
{
    public static string Build(List<Qualification> qualificationList)
    {
        XElement root = new XElement("Qualifications",
            qualificationList.Select(q =>
                new XElement("Qualification",
                    new XElement("CourseName", q.CourseName),
                    new XElement("University", q.University),
                    new XElement("PassingYear", q.PassingYear),
                    new XElement("Percentage", q.Percentage))
            ));

        return root.ToString();
    }
}
