USE BankingAppDB;
GO

-- Table 3: Transactions (all client transactions)
CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY IDENTITY(1,1),
    ClientId INT NOT NULL,
    TransactionType NVARCHAR(20) NOT NULL, -- 'Deposit' or 'Withdraw'
    TransactionAmount DECIMAL(18,2) NOT NULL,
    BalanceAfterTransaction DECIMAL(18,2) NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(255),
    CONSTRAINT FK_Transactions_Clients FOREIGN KEY (ClientId) REFERENCES Clients(ClientId),
    CONSTRAINT CHK_TransactionType CHECK (TransactionType IN ('Deposit', 'Withdraw')),
    CONSTRAINT CHK_TransactionAmount CHECK (TransactionAmount > 0)
);
GO

-- Create indexes for better performance
CREATE INDEX IX_Transactions_ClientId ON Transactions(ClientId);
CREATE INDEX IX_Transactions_Date ON Transactions(TransactionDate DESC);
GO

PRINT 'Transactions table created successfully!';
GO
