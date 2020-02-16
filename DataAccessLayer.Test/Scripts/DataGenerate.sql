USE Database_Test;
GO
BEGIN TRANSACTION;
INSERT INTO dbo.Users(Username, Password)
VALUES('TestUser1', 'Password1'),
    ('TestUser2', 'Password2');
COMMIT TRANSACTION;