USE BankingAppDB;
GO

-- Insert default admin user (password is hashed: AdminPassword@123)
INSERT INTO Users (Username, PasswordHash)
VALUES ('Admin', 'AdminPassword@123');
GO

PRINT 'Default admin user created: Username = Admin, Password = AdminPassword@123';
GO
