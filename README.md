

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
