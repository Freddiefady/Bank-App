USE BankingAppDB;
GO

CREATE OR ALTER PROCEDURE sp_AddClient
    @Name NVARCHAR(100),
    @NationalId NVARCHAR(50),
    @Age INT,
    @AccountNumber NVARCHAR(50),
    @MaxCreditBalance DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Clients (Name, NationalId, Age, AccountNumber, MaxCreditBalance, CurrentBalance)
        VALUES (@Name, @NationalId, @Age, @AccountNumber, @MaxCreditBalance, 0.00);

        SELECT SCOPE_IDENTITY() AS ClientId;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO
