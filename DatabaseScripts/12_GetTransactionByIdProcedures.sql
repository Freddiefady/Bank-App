USE BankingAppDB;
GO

CREATE OR ALTER PROCEDURE sp_GetTransactionsByClientId
    @ClientId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t.TransactionId,
        t.ClientId,
        c.Name AS ClientName,
        c.AccountNumber,
        t.TransactionType,
        t.TransactionAmount,
        t.BalanceAfterTransaction,
        t.TransactionDate,
        t.Description
    FROM Transactions t
    INNER JOIN Clients c ON t.ClientId = c.ClientId
    WHERE t.ClientId = @ClientId
    ORDER BY t.TransactionDate DESC;
END
GO

PRINT 'All stored procedures created successfully!';
GO
