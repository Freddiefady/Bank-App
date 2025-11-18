USE BankingAppDB;
GO

CREATE OR ALTER PROCEDURE sp_GetClientById
    @ClientId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ClientId,
        Name,
        NationalId,
        Age,
        AccountNumber,
        MaxCreditBalance,
        CurrentBalance,
        CreatedDate,
        IsActive
    FROM Clients
    WHERE ClientId = @ClientId;
END
GO
