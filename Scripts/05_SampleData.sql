USE SchoolMgmtSystemDB;
GO

DECLARE @NewId INT, @NewCode NVARCHAR(20), @RC INT, @Hash CHAR(40);

SET @Hash = CONVERT(CHAR(40), HASHBYTES('SHA1', 'Pass@123'), 2);

EXEC dbo.InsertStudent
    @FirstName = 'Rahul',
    @LastName = 'Nair',
    @Age = 22,
    @DOB = '2003-04-12',
    @Gender = 'Male',
    @Email = 'rahul.nair@example.com',
    @Phone = '9876500001',
    @Username = 'rahulnair',
    @PasswordHash = @Hash,
    @QualificationsXml = '<Qualifications><Qualification><CourseName>BSc Computer Science</CourseName><University>Kerala University</University><PassingYear>2023</PassingYear><Percentage>78.50</Percentage></Qualification></Qualifications>',
    @StudentId = @NewId OUTPUT, @StudentCode = @NewCode OUTPUT, @ReturnCode = @RC OUTPUT;

EXEC dbo.InsertStudent
    @FirstName = 'Anjali',
    @LastName = 'Menon',
    @Age = 21,
    @DOB = '2004-08-25',
    @Gender = 'Female',
    @Email = 'anjali.menon@example.com',
    @Phone = '9876500002',
    @Username = 'anjalimenon',
    @PasswordHash = @Hash,
    @QualificationsXml = '<Qualifications><Qualification><CourseName>Plus Two</CourseName><University>Board of Higher Secondary</University><PassingYear>2021</PassingYear><Percentage>88.00</Percentage></Qualification><Qualification><CourseName>BCom</CourseName><University>MG University</University><PassingYear>2024</PassingYear><Percentage>74.25</Percentage></Qualification></Qualifications>',
    @StudentId = @NewId OUTPUT, @StudentCode = @NewCode OUTPUT, @ReturnCode = @RC OUTPUT;

EXEC dbo.InsertStudent
    @FirstName = 'Sarath',
    @LastName = 'Kumar',
    @Age = 23,
    @DOB = '2002-11-05',
    @Gender = 'Male',
    @Email = 'sarath.kumar@example.com',
    @Phone = '9876500003',
    @Username = 'sarathkumar',
    @PasswordHash = @Hash,
    @QualificationsXml = '<Qualifications><Qualification><CourseName>BTech Computer Science</CourseName><University>Calicut University</University><PassingYear>2023</PassingYear><Percentage>81.75</Percentage></Qualification></Qualifications>',
    @StudentId = @NewId OUTPUT, @StudentCode = @NewCode OUTPUT, @ReturnCode = @RC OUTPUT;

EXEC dbo.InsertStudent
    @FirstName = 'Fathima',
    @LastName = 'Beevi',
    @Age = 20,
    @DOB = '2005-02-18',
    @Gender = 'Female',
    @Email = 'fathima.beevi@example.com',
    @Phone = '9876500004',
    @Username = 'fathimabeevi',
    @PasswordHash = @Hash,
    @QualificationsXml = NULL,
    @StudentId = @NewId OUTPUT, @StudentCode = @NewCode OUTPUT, @ReturnCode = @RC OUTPUT;

EXEC dbo.InsertStudent
    @FirstName = 'Vishnu',
    @LastName = 'Prasad',
    @Age = 24,
    @DOB = '2001-06-30',
    @Gender = 'Male',
    @Email = 'vishnu.prasad@example.com',
    @Phone = '9876500005',
    @Username = 'vishnuprasad',
    @PasswordHash = @Hash,
    @QualificationsXml = '<Qualifications><Qualification><CourseName>Diploma in Electronics</CourseName><University>Government Polytechnic</University><PassingYear>2020</PassingYear><Percentage>70.00</Percentage></Qualification><Qualification><CourseName>BTech ECE</CourseName><University>Kerala University</University><PassingYear>2024</PassingYear><Percentage>76.60</Percentage></Qualification></Qualifications>',
    @StudentId = @NewId OUTPUT, @StudentCode = @NewCode OUTPUT, @ReturnCode = @RC OUTPUT;
GO
