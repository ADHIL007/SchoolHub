# School Management System

## Database Setup

The `Scripts` folder contains the SQL scripts required to set up the backend database. Please run them in the following order:
1. `01_CreateDatabase.sql` - Creates the database schema.
2. `02_Tables.sql` - Creates the necessary tables.
3. `03_Views.sql` - Sets up the database views used for querying data.
4. `04_StoredProcedures.sql` - Creates the stored procedures used by the DAL (e.g., `InsertStudent`, `ValidateLogin`).
5. `05_SampleData.sql` - (Optional) Inserts initial sample data for testing.

## Getting Started

1. Clone or download the repository.
2. Ensure you have the [.NET 9.0 SDK](https://dotnet.microsoft.com/download) installed.
3. Open SQL Server Management Studio (SSMS) and execute the scripts in the `Scripts` folder to create the database.
4. Update the connection string (if necessary) in the appropriate configuration files (like `appsettings.json`).
5. Build and run the `SchoolMgmtSystem` project via Visual Studio or the .NET CLI:
   ```bash
   cd SchoolMgmtSystem
   dotnet run
   ```
6. Navigate to `https://localhost:<port>` to access the application.
