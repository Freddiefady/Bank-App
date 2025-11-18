USE BankingAppDB;
GO

CREATE OR ALTER PROCEDURE sp_RecordTransaction
    @ClientId INT,
    @TransactionType NVARCHAR(20),
    @TransactionAmount DECIMAL(18,2),
    @Description NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @CurrentBalance DECIMAL(18,2);
        DECLARE @MaxCreditBalance DECIMAL(18,2);
        DECLARE @NewBalance DECIMAL(18,2);

        -- Get current balance and max credit
        SELECT
            @CurrentBalance = CurrentBalance,
            @MaxCreditBalance = MaxCreditBalance
        FROM Clients
        WHERE ClientId = @ClientId;

        -- Calculate new balance
        IF @TransactionType = 'Deposit'
        BEGIN
            SET @NewBalance = @CurrentBalance + @TransactionAmount;
        END
        ELSE IF @TransactionType = 'Withdraw'
        BEGIN
            SET @NewBalance = @CurrentBalance - @TransactionAmount;

            -- Check if withdrawal exceeds credit limit
            IF @NewBalance < -@MaxCreditBalance
            BEGIN
                RAISERROR('Insufficient funds. Withdrawal would exceed credit limit.', 16, 1);
                ROLLBACK TRANSACTION;
                RETURN;
            END
        END

        -- Insert transaction record
        INSERT INTO Transactions (ClientId, TransactionType, TransactionAmount, BalanceAfterTransaction, Description)
        VALUES (@ClientId, @TransactionType, @TransactionAmount, @NewBalance, @Description);

        -- Update client balance
        UPDATE Clients
        SET CurrentBalance = @NewBalance
        WHERE ClientId = @ClientId;

        COMMIT TRANSACTION;

        -- Return the new transaction
        SELECT
            TransactionId,
            ClientId,
            TransactionType,
            TransactionAmount,
            BalanceAfterTransaction,
            TransactionDate,
            Description
        FROM Transactions
        WHERE TransactionId = SCOPE_IDENTITY();

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO
