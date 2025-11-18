USE BankingAppDB;
GO

-- Table 2: Clients (bank customers)
CREATE TABLE Clients (
    ClientId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    NationalId NVARCHAR(50) NOT NULL UNIQUE,
    Age INT NOT NULL,
    AccountNumber NVARCHAR(50) NOT NULL UNIQUE,
    MaxCreditBalance DECIMAL(18,2) NOT NULL,
    CurrentBalance DECIMAL(18,2) DEFAULT 0.00,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    CONSTRAINT CHK_Age CHECK (Age >= 18 AND Age <= 120),
    CONSTRAINT CHK_MaxCredit CHECK (MaxCreditBalance >= 0)
);
GO

-- Create indexes for better performance
CREATE INDEX IX_Clients_AccountNumber ON Clients(AccountNumber);
GO

PRINT 'Clients table created successfully!';
GO
