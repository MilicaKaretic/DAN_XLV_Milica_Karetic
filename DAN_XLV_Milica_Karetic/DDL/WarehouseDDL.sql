-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WarehouseDB')
CREATE DATABASE WarehouseDB;
GO

IF OBJECT_ID('Product', 'U') IS NOT NULL DROP TABLE Product;

USE WarehouseDB
CREATE TABLE Product(
	ProductID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ProductName VARCHAR (255) NOT NULL,
	ProductCode VARCHAR (15) NOT NULL,
	Quantity INT NOT NULL,
	Price INT NOT NULL,
	Stored BIT NOT NULL
);

INSERT INTO Product VALUES ('Product1', '111', 10, 100, 0);