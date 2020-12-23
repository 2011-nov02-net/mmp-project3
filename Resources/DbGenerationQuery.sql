-- Remove tables if they already exist
DROP TABLE IF EXISTS Trades
DROP TABLE IF EXISTS Assets
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
	UserName NVARCHAR(99) NOT NULL
)

CREATE TABLE Stocks (
	Id INT PRIMARY KEY IDENTITY,
	Symbol NVARCHAR(99) NOT NULL,
	Market NVARCHAR(99) NOT NULL,
	Name NVARCHAR(99) NOT NULL,
	Logo NVARCHAR(99),
	CONSTRAINT AK_StockSymbolMarket UNIQUE (Symbol, Market)
)

CREATE TABLE Portfolios (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(99) NOT NULL,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users (Id),
	Funds MONEY NOT NULL DEFAULT 500.00
)

CREATE TABLE Assets (
	Id INT PRIMARY KEY IDENTITY,
	PortfolioId INT NOT NULL FOREIGN KEY REFERENCES Portfolios (Id),
	StockId INT NOT NULL FOREIGN KEY REFERENCES Stocks (Id),
	Quantity INT NOT NULL,
	CONSTRAINT AK_AssetPortfolioIdStockId UNIQUE (PortfolioId, StockId)
)

CREATE TABLE Trades (
	Id INT PRIMARY KEY IDENTITY,
	PortfolioId INT NOT NULL FOREIGN KEY REFERENCES Portfolios (Id),
	StockId INT NOT NULL FOREIGN KEY REFERENCES Stocks (Id),
	Quantity INT NOT NULL,
	Price MONEY NOT NULL,
	TimeTraded DATETIME NOT NULL,
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
--	StockId INT FOREIGN KEY REFERENCES Stocks (Id)
--)

--CREATE TABLE GroupMembers (
--	GroupId INT NOT NULL FOREIGN KEY REFERENCES Groups (Id),
--	UserId INT NOT NULL FOREIGN KEY REFERENCES Users (Id),
--	PRIMARY KEY (GroupId, UserId)
--)

GO

-- Populate tables

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [UserName]) VALUES (1, N'Matt', N'Marnien', N'mgm215@gmail.com', N'mm215')
INSERT INTO [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [UserName]) VALUES (2, N'Matthew', N'Goodman', N'mg007@gmailcom', N'mg007')
INSERT INTO [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [UserName]) VALUES (3, N'Paul', N'Cortez', N'pb001@gmail.com', N'pb001')
SET IDENTITY_INSERT [dbo].[Users] OFF

INSERT INTO [dbo].[Stocks] ([Symbol], [Market], [Name], [Logo]) VALUES (N'APPL', N'NASDAQ', N'Apple', N'https://logos-world.net/wp-content/uploads/2020/04/Apple-Logo-700x394.png')
INSERT INTO [dbo].[Stocks] ([Symbol], [Market], [Name], [Logo]) VALUES (N'FMX', N'NYSE', N'FEMSA', N'https://logodownload.org/wp-content/uploads/2019/11/femsa-logo-0.png')
INSERT INTO [dbo].[Stocks] ([Symbol], [Market], [Name], [Logo]) VALUES (N'GE', N'NYSE', N'General Electric Company', N'https://goodlogo.com/images/logos/general_electric_logo_2489.gif')
INSERT INTO [dbo].[Stocks] ([Symbol], [Market], [Name], [Logo]) VALUES (N'XOM', N'NYSE', N'Exxon Mobil', N'https://logodownload.org/wp-content/uploads/2013/12/exxonmobil-logo-0.svg_.png')

SET IDENTITY_INSERT [dbo].[Portfolios] ON
INSERT INTO [dbo].[Portfolios] ([Id], [Name], [UserId], [Funds]) VALUES (1, N'Matt M Portfolio', 1, CAST(3000.0000 AS Money))
INSERT INTO [dbo].[Portfolios] ([Id], [Name], [UserId], [Funds]) VALUES (3, N'Matt G Portfolio', 2, CAST(3000.0000 AS Money))
INSERT INTO [dbo].[Portfolios] ([Id], [Name], [UserId], [Funds]) VALUES (5, N'Paul Portfolio', 3, CAST(3000.0000 AS Money))
SET IDENTITY_INSERT [dbo].[Portfolios] OFF

INSERT INTO [dbo].[Assets] ([PortfolioId], [StockId], [Quantity]) VALUES (1, 1, 4)
INSERT INTO [dbo].[Assets] ([PortfolioId], [StockId], [Quantity]) VALUES (3, 3, 10)
INSERT INTO [dbo].[Assets] ([PortfolioId], [StockId], [Quantity]) VALUES (5, 2, 7)

SET IDENTITY_INSERT [dbo].[Trades] ON
INSERT INTO [dbo].[Trades] ([Id], [PortfolioId], [StockId], [Quantity], [Price], [TimeTraded]) VALUES (3, 1, 1, 4, CAST(524.3600 AS Money), N'2020-12-21 00:00:00')
INSERT INTO [dbo].[Trades] ([Id], [PortfolioId], [StockId], [Quantity], [Price], [TimeTraded]) VALUES (4, 3, 3, 10, CAST(107.5000 AS Money), N'2020-12-21 00:00:00')
INSERT INTO [dbo].[Trades] ([Id], [PortfolioId], [StockId], [Quantity], [Price], [TimeTraded]) VALUES (5, 5, 2, 7, CAST(518.0000 AS Money), N'2020-12-21 00:00:00')
SET IDENTITY_INSERT [dbo].[Trades] OFF
