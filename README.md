# Running Group21ProjectMVC
1. Change FILENAME on line 35 and 37 in 560ProjectDatabase.sql to be the location you want the database stored.
For Example:
```
	 (NAME = N'560Project', FILENAME = N'/var/opt/mssql/data/560Project.mdf', SIZE = 8192 KB, MAXSIZE = UNLIMITED, FILEGROWTH = 65536 KB)
	 LOG ON
	 (NAME = N'560Project_log', FILENAME = N'/var/opt/mssql/data/560Project_log.ldf', SIZE = 8192 KB, MAXSIZE = 2048 GB, FILEGROWTH = 65536 KB)
```
Change to:
```
	 (NAME = N'560Project', FILENAME = N'C:\Users\Sam\560Project.mdf', SIZE = 8192 KB, MAXSIZE = UNLIMITED, FILEGROWTH = 65536 KB)
	 LOG ON
	 (NAME = N'560Project_log', FILENAME = N'C:\Users\Sam\560Project_log.ldf', SIZE = 8192 KB, MAXSIZE = 2048 GB, FILEGROWTH = 65536 KB)
```

2. Execute 560ProjectDatabase.sql
3. Execute All files in StoredProcedures and StoredProcedures/Identity
4. Open and run Group21ProjectMVC

## Setting yourself as Admin
1. Run Group21ProjectMVC
2. Click Login -> Click Don't have an account? [Sign Up]
3. Register an account
4. Replace ** EMAIL HERE ** with the email you used and run the script

```
WITH CTE AS(
SELECT
U.Id AS 'UserId',
R.Id AS 'RoleId'
FROM [Flights].[ApplicationUser] U,[Flights].[ApplicationRole] R
WHERE U.[UserName] = '** EMAIL HERE **'
AND R.[Name] = 'Admin'
)
INSERT INTO Flights.[ApplicationUserRole]([RoleId], [UserId])
SELECT * FROM CTE
GO
```
### To mass insert flights for testing 
1. Once logged in click your name in the top right
2. Click "Manage"
3. Click "Add Flights"
4. **In the Airline input make sure you use a name of an Airline in the database**
