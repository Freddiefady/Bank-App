USE BankingAppDB;
GO

CREATE OR ALTER PROCEDURE sp_AuthenticateUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UserId, Username
    FROM Users
    WHERE Username = @Username AND PasswordHash = @Password;
END
GO
