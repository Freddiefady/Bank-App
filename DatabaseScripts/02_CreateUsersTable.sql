USE BankingAppDB;
GO

-- Table 1: Users (for login authentication)
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);
GO

PRINT 'Users table created successfully!';
GO
