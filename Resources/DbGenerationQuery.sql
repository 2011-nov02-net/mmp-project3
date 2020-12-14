-- Remove tables if they already exist
DROP TABLE IF EXISTS Trades
DROP TABLE IF EXISTS Portfolios
DROP TABLE IF EXISTS Stocks
DROP TABLE IF EXISTS Users

GO

-- Create tables
CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(99) NOT NULL,
	LastName NVARCHAR(99) NOT NULL,
	Email NVARCHAR(99) UNIQUE NOT NULL,
	UserName NVARCHAR(99) NOT NULL,
	Funds MONEY NOT NULL DEFAULT 500.00
)

CREATE TABLE Stocks (
	Symbol NVARCHAR(99),
	Market NVARCHAR(99),
	Name NVARCHAR(99) NOT NULL,
	Logo NVARCHAR(99),
	PRIMARY KEY (Symbol, Market)
)

CREATE TABLE Portfolios (
	UserId INT FOREIGN KEY REFERENCES Users (Id),
	StockSymbol NVARCHAR(99),
	StockMarket NVARCHAR(99),
	Quantity INT NOT NULL,
	FOREIGN KEY (StockSymbol, StockMarket) REFERENCES Stocks (Symbol, Market),
	PRIMARY KEY (UserId, StockSymbol, StockMarket)
)

CREATE TABLE Trades (
	Id INT PRIMARY KEY IDENTITY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users (Id),
	StockSymbol NVARCHAR(99) NOT NULL,
	StockMarket NVARCHAR(99) NOT NULL,
	Quantity INT NOT NULL,
	Price MONEY NOT NULL,
	TimeTraded DATETIME NOT NULL,
	FOREIGN KEY (StockSymbol, StockMarket) REFERENCES Stocks (Symbol, Market)
)

--CREATE TABLE Groups (
--	Id INT PRIMARY KEY IDENTITY,
--	Name NVARCHAR(99) NOT NULL,
--	OwnerId INT NOT NULL FOREIGN KEY REFERENCES Users (Id)
--)

--CREATE TABLE Posts (
--	Id INT PRIMARY KEY IDENTITY,
--	UserId INT NOT NULL FOREIGN KEY REFERENCES Users (Id),
--	TimePosted DATETIME NOT NULL,
--	BodyMessage NVARCHAR(240),
--	GroupId INT FOREIGN KEY REFERENCES Groups (Id),
--	StockSymbol NVARCHAR(99),
--	StockMarket NVARCHAR(99),
--	FOREIGN KEY (StockSymbol, StockMarket) REFERENCES Stocks (Symbol, Market)
--)

--CREATE TABLE GroupMembers (
--	GroupId INT NOT NULL FOREIGN KEY REFERENCES Groups (Id),
--	UserId INT NOT NULL FOREIGN KEY REFERENCES Users (Id),
--	PRIMARY KEY (GroupId, UserId)
--)

GO

-- Populate tables
