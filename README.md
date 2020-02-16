# .Net-Unit-Testing-Solution-For-Data-Access-Layer-
.Net Unit Testing Solution For Data Access Layer  (#dotnetcore  #SQL #RepositoryPattern)

# Problem:
Want to make sure that your variant Data-Access-Layer's implementations are operating as expected!
In simple words, Testing the Data-Access-Layer.

# Solution:
Add a new .Net test project to your solution.  
Add a new folder to the test project and name it as Scripts.  
Add a Sql script file and name it as `CreateDatabase.sql`.  
This script is responsible for creating Test database files.  
```
USE master
GO

IF DB_ID('Database_Test') IS NOT NULL
BEGIN
ALTER DATABASE Database_Test SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
END

GO

IF DB_ID('Database_Test') IS NOT NULL
BEGIN
DROP DATABASE Database_Test;
END

GO

CREATE DATABASE Database_Test

GO
```
