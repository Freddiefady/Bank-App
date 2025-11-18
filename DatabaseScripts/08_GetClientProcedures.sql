USE BankingAppDB;
GO

CREATE OR ALTER PROCEDURE sp_GetAllClients
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
    WHERE IsActive = 1
    ORDER BY Name;
END
GO
