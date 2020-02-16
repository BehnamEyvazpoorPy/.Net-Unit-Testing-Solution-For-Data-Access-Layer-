# .Net-Unit-Testing-Solution-For-Data-Access-Layer-
.Net Unit Testing Solution For Data Access Layer  (#dotnetcore  #SQL #RepositoryPattern)

## Problem:
Want to make sure that your variant Data-Access-Layer's implementations are operating as expected!
In simple words, Testing the Data-Access-Layer.

## Solution:
Add a new .Net test project to your solution.  
Add a new folder to the test project and name it as Scripts.  
Add a Sql script file and name it as `CreateDatabase.sql`.  
This script is responsible for creating Test database files.  
```SQL
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
Now use the SQL Server Management Studio to generate the Database script and name it as `CreateTables.Sql`.  
Don't export the whole database script. export only the tables and the StoredProcedures script.  
Add another script file and name it as `DataGenerate.Sql`  
In this file write script to insert your test data into the target tables.
```SQL
USE Database_Test;
GO
BEGIN TRANSACTION;
INSERT INTO dbo.Users(Username, Password)
VALUES('TestUser1', 'Password1'),
    ('TestUser2', 'Password2');
COMMIT TRANSACTION;
```
Add a bat file to the scrips folder and name it as `RunScript.bat`:  
Copy the follow script and paste it into the bat file. This bat file executes the SQL scripts.  
```bat
echo off
sqlcmd -E -S . -i CreateDatabase.sql
sqlcmd -E -S . -i CreateTables.sql
sqlcmd -E -S . -i DataGenerate.sql
exit
```
Add a new Class to the test project and name it as `DatabaseInitializer.cs`:  
This class is responsible for executing the bat file.
```C#
using System;
using System.Diagnostics;
using System.IO;

namespace DataAccessLayer.Test
{
	public static class DatabaseInitializer
	{
		public static void InitDatabase()
		{
			Debug.WriteLine("Database and data are generating...");

			ExecuteCommand("/c RunScript.bat");

			Debug.WriteLine("Database and data are generated...");
		}

		#region Helpers
		private static void ExecuteCommand(string command)
		{
			ProcessStartInfo processInfo;
			Process process;

			processInfo = new ProcessStartInfo();
			processInfo.WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"Scripts\");
			processInfo.FileName = "cmd.exe";
			processInfo.Arguments = command;
			processInfo.CreateNoWindow = true;
			processInfo.UseShellExecute = false;
			// *** Redirect the output ***
			processInfo.RedirectStandardError = true;
			processInfo.RedirectStandardOutput = true;

			process = Process.Start(processInfo);
			process.WaitForExit();

			// *** Read the streams ***
			// Warning: This approach can lead to deadlocks, see Edit #2
			string output = process.StandardOutput.ReadToEnd();
			string error = process.StandardError.ReadToEnd();

			if (!string.IsNullOrEmpty(error))
				throw new Exception(error);

			process.Close();
		}
		#endregion
	}
}

```  
Just copy and past it in your class file.  
Ok add a new another folder to the test project and name it as `BaseRepositoryTest`.  
Now Add a class to the BaseRepositoryTest folder for example name it as `BaseUserRepositoryTest.cs`  
```c#
using DataAccessLayer.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer.Test.BaseRepositoryTest
{
	public class BaseUserRepositoryTest
	{
		protected IUsersRepository usersRepository;

		public virtual void TestInit()
		{
			DatabaseInitializer.InitDatabase();
		}

		public virtual void GetUsers_MustReturnUsersAsExpectedTest()
		{
			var expectedUsers = new List<User>
			{
				new User { Id = 1, Username = "TestUser1", Password = "Password1" },
				new User { Id = 2, Username = "TestUser2", Password = "Password2" }
			};

			var actualUsers = usersRepository.GetUsers();

			Assert.AreEqual(JsonConvert.SerializeObject(expectedUsers), JsonConvert.SerializeObject(actualUsers));
		}
	}
}
```
Ok, now based on your implementation of Data-Access-Layer add other folders.  
For example, suppose that you have an ADO implementation of the Data-Access-Layer, so you have to add a folder and name it as `AdoRepositoryTest` then add a Test class for the user repository which inherited  from the base user repository test class.  
```c#
using DataAccessLayer.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessLayer.Test.EFRepositoryTest
{
	[TestClass]
	public class UserRepositoryTest : BaseRepositoryTest.BaseUserRepositoryTest
	{
		[TestInitialize]
		public override void TestInit()
		{
			usersRepository = new UsersRepository(TestConfigs.ConnectionString);
			base.TestInit();
		}

		[TestMethod]
		public override void GetUsers_MustReturnUsersAsExpectedTest()
		{
			base.GetUsers_MustReturnUsersAsExpectedTest();
		}
	}
}

```
Clone or download the sample project and build it using visual studio or using the .net core CLI.  
Email address: **BehnamEyvazpoorkook@gmail.com**
