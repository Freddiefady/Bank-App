USE master;
GO

-- Drop database if it exists (for development purposes)
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BankingAppDB')
BEGIN
    ALTER DATABASE BankingAppDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BankingAppDB;
END
GO

-- Create the database
CREATE DATABASE BankingAppDB;
GO

-- Switch to the new database
USE BankingAppDB;
GO

PRINT 'Database BankingAppDB created successfully!';
GO
