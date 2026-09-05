USE SchoolMgmtSystemDB;
GO
CREATE PROCEDURE dbo.InsertStudent
(
    @FirstName      NVARCHAR(50),
    @LastName       NVARCHAR(50)  = NULL,
    @Age            INT,
    @DOB            DATE,
    @Gender         NVARCHAR(10),
    @Email          NVARCHAR(100),
    @Phone          NVARCHAR(15),
    @Username       NVARCHAR(50),
    @PasswordHash   CHAR(40),
    @QualificationsXml NVARCHAR(MAX) = NULL,
    @StudentId      INT OUTPUT,
    @StudentCode    NVARCHAR(20) OUTPUT,
    @ReturnCode     INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @ReturnCode = 0;

    IF EXISTS (SELECT 1 FROM dbo.Students WHERE Email = @Email)
    BEGIN
        SET @ReturnCode = 1;
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM dbo.Students WHERE Username = @Username)
    BEGIN
        SET @ReturnCode = 2;
        RETURN;
    END

    DECLARE @Xml XML = TRY_CAST(@QualificationsXml AS XML);

    BEGIN TRANSACTION;

    INSERT INTO dbo.Students
        (FirstName, LastName, Age, DOB, Gender, Email, Phone, Username, PasswordHash)
    VALUES
        (@FirstName, @LastName, @Age, @DOB, @Gender, @Email, @Phone, @Username, @PasswordHash);

    SET @StudentId = SCOPE_IDENTITY();
    SET @StudentCode = 'STU' + RIGHT('00000' + CAST(@StudentId AS VARCHAR(5)), 5);

    UPDATE dbo.Students
    SET StudentCode = @StudentCode
    WHERE StudentId = @StudentId;

    IF @Xml IS NOT NULL
    BEGIN
        INSERT INTO dbo.Qualifications (StudentId, CourseName, University, PassingYear, Percentage)
        SELECT
            @StudentId,
            T.Q.value('(CourseName/text())[1]', 'NVARCHAR(100)'),
            T.Q.value('(University/text())[1]', 'NVARCHAR(150)'),
            T.Q.value('(PassingYear/text())[1]', 'INT'),
            T.Q.value('(Percentage/text())[1]', 'DECIMAL(5,2)')
        FROM @Xml.nodes('/Qualifications/Qualification') AS T(Q);
    END

    COMMIT TRANSACTION;
END
GO
CREATE PROCEDURE dbo.GetAllStudents
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        StudentId, StudentCode, FirstName, LastName, Age, DOB, Gender,
        Email, Phone, Username, CreatedDate, QualificationCount
    FROM dbo.vw_StudentList
    ORDER BY StudentId DESC;
END
GO

CREATE PROCEDURE dbo.GetStudentById
(
    @StudentId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        StudentId, StudentCode, FirstName, LastName, Age, DOB, Gender,
        Email, Phone, Username, CreatedDate, QualificationCount
    FROM dbo.vw_StudentList
    WHERE StudentId = @StudentId;

    SELECT
        QualificationId, CourseName, University, PassingYear, Percentage
    FROM dbo.Qualifications
    WHERE StudentId = @StudentId
    ORDER BY QualificationId;
END
GO
CREATE PROCEDURE dbo.ValidateLogin
(
    @Username     NVARCHAR(50),
    @PasswordHash CHAR(40)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        StudentId, StudentCode, FirstName, LastName, Username
    FROM dbo.Students
    WHERE Username = @Username
      AND PasswordHash = @PasswordHash;
END
GO
