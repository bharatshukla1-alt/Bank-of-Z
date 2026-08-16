-- Legacy Modernization Seed Data (BNK1 Map Migrated)
INSERT INTO Customers (Company, CustomerNumber, Title, FirstName, MiddleInitials, LastName, AddressLine1, AddressLine2, City, PostCode, Country, DateOfBirth, SortCode, CreditScore, ScoreDate, CreatedAt)
VALUES 
('BNK1', 'CUST000101', 'Mr.', 'John', 'A', 'Smith', '120 High Street', 'Apt 4B', 'London', 'EC1A 1BB', 'UK', '1982-05-14', '10-20-30', 750, GETUTCDATE(), GETUTCDATE()),
('BNK1', 'CUST000102', 'Ms.', 'Jane', 'B', 'Doe', '45 Park Lane', 'Suite 10', 'Manchester', 'M1 4BT', 'UK', '1990-11-22', '10-20-30', 680, GETUTCDATE(), GETUTCDATE());

INSERT INTO Accounts (Company, AccountNumber, CustomerId, AccountType, InterestRate, OverdraftLimit, SortCode, OpenDate, LastStatementDate, NextStatementDate, AvailableBalance, ActualBalance)
VALUES
('BNK1', 'ACC10001', 1, 'CURR', 0.0150, 500.00, '10-20-30', GETUTCDATE(), GETUTCDATE(), DATEADD(month, 1, GETUTCDATE()), 2450.50, 2450.50),
('BNK1', 'ACC10002', 1, 'SAVG', 0.0425, 0.00, '10-20-30', GETUTCDATE(), GETUTCDATE(), DATEADD(month, 1, GETUTCDATE()), 12500.00, 12500.00),
('BNK1', 'ACC20001', 2, 'CURR', 0.0150, 250.00, '10-20-30', GETUTCDATE(), GETUTCDATE(), DATEADD(month, 1, GETUTCDATE()), 890.00, 890.00);

INSERT INTO Transactions (Company, FromAccountNumber, FromSortCode, ToAccountNumber, ToSortCode, Amount, Sign, TransactionType, TransactionDate, Message)
VALUES
('BNK1', 'ACC10001', '10-20-30', 'ACC20001', '10-20-30', 100.00, '-', 'Transfer', GETUTCDATE(), 'Funds transfer test'),
('BNK1', 'ACC10001', '10-20-30', '', '', 50.00, '+', 'Deposit', GETUTCDATE(), 'Cash deposit');
