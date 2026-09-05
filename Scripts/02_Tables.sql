USE SchoolMgmtSystemDB;
GO

IF OBJECT_ID('dbo.Qualifications', 'U') IS NOT NULL DROP TABLE dbo.Qualifications;
IF OBJECT_ID('dbo.Students', 'U') IS NOT NULL DROP TABLE dbo.Students;
GO

CREATE TABLE dbo.Students
(
    StudentId       INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    StudentCode     NVARCHAR(20)        NULL,
    FirstName       NVARCHAR(50)        NOT NULL,
    LastName        NVARCHAR(50)        NULL,
    Age             INT                 NOT NULL,
    DOB             DATE                NOT NULL,
    Gender          NVARCHAR(10)        NOT NULL,
    Email           NVARCHAR(100)       NOT NULL,
    Phone           NVARCHAR(15)        NOT NULL,
    Username        NVARCHAR(50)        NOT NULL,
    PasswordHash    CHAR(40)            NOT NULL,
    CreatedDate     DATETIME            NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_Students_Email    UNIQUE (Email),
    CONSTRAINT UQ_Students_Username UNIQUE (Username)
);
GO

CREATE TABLE dbo.Qualifications
(
    QualificationId INT IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    StudentId       INT                 NOT NULL,
    CourseName      NVARCHAR(100)       NOT NULL,
    University      NVARCHAR(150)       NOT NULL,
    PassingYear   INT                 NOT NULL,
    Percentage      DECIMAL(5,2)        NOT NULL,
    CONSTRAINT FK_Qualifications_Students FOREIGN KEY (StudentId)
        REFERENCES dbo.Students (StudentId) ON DELETE CASCADE
);
GO
