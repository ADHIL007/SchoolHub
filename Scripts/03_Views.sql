USE SchoolMgmtSystemDB;
GO

IF OBJECT_ID('dbo.vw_StudentList', 'V') IS NOT NULL DROP VIEW dbo.vw_StudentList;
GO

CREATE VIEW dbo.vw_StudentList
AS
SELECT
    s.StudentId,
    s.StudentCode,
    s.FirstName,
    s.LastName,
    s.Age,
    s.DOB,
    s.Gender,
    s.Email,
    s.Phone,
    s.Username,
    s.CreatedDate,
    COUNT(q.QualificationId) AS QualificationCount
FROM dbo.Students s
LEFT JOIN dbo.Qualifications q ON q.StudentId = s.StudentId
GROUP BY
    s.StudentId, s.StudentCode, s.FirstName, s.LastName, s.Age, s.DOB,
    s.Gender, s.Email, s.Phone, s.Username, s.CreatedDate;
GO

IF OBJECT_ID('dbo.vw_StudentQualifications', 'V') IS NOT NULL DROP VIEW dbo.vw_StudentQualifications;
GO

CREATE VIEW dbo.vw_StudentQualifications
AS
SELECT
    s.StudentId,
    s.StudentCode,
    s.FirstName,
    s.LastName,
    q.QualificationId,
    q.CourseName,
    q.University,
    q.PassingYear,
    q.Percentage
FROM dbo.Students s
JOIN dbo.Qualifications q ON q.StudentId = s.StudentId;
GO
