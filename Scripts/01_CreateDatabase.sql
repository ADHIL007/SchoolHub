IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'SchoolMgmtSystemDB')
BEGIN
    CREATE DATABASE SchoolMgmtSystemDB;
END
GO
