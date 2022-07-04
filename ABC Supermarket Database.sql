USE MASTER

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'ABC_Supermarket')
DROP DATABASE ABC_Supermarket

CREATE DATABASE ABC_Supermarket;
USE ABC_Supermarket

------------------------------------------------------------------------------------------------------------
CREATE TABLE Items(
Item_ID VARCHAR (30) PRIMARY KEY NOT NULL,
Item_Name VARCHAR (30) NOT NULL,
Item_Description VARCHAR (1000) NOT NULL,
Item_Price FLOAT NOT NULL
);

DROP TABLE Items;
-------------------------------------------------------------------------------------------------------------
--Stored procedures--
-------------------------------------------------------------------------------------------------------------
--View All Items--

CREATE PROCEDURE dbo.View_All_Items
AS
BEGIN
SELECT Items.Item_ID, Items.Item_Name, Items.Item_Description, Items.Item_Price
FROM Items
END

DROP PROCEDURE dbo.View_All_Items
--------------------------------------------------------------------------------------------------------------
--Create Items--

CREATE PROCEDURE dbo.Create_Items
(
@Item_ID VARCHAR (30),
@Item_Name VARCHAR (30),
@Item_Description VARCHAR (1000),
@Item_Price FLOAT
)
AS
BEGIN
INSERT INTO Items VALUES (@Item_ID, @Item_Name, @Item_Description, @Item_Price)
END

DROP PROCEDURE dbo.Create_Items

----------------------------------------------------------------------------------------------------------------
--Update Items--

CREATE PROCEDURE dbo.Update_Items
(
@Item_ID VARCHAR (30),
@Item_Name VARCHAR (30),
@Item_Description VARCHAR (1000),
@Item_Price FLOAT
)
AS
BEGIN
UPDATE Items
SET
Item_Name = @Item_Name,
Item_Description = @Item_Description,
Item_Price = @Item_Price
WHERE Item_ID = @Item_ID
END

DROP PROCEDURE dbo.Update_Items;

-----------------------------------------------------------------------------------------------------------------
--Delete Items--

CREATE PROCEDURE dbo.DeleteItembyID
(
@Item_ID VARCHAR (30)
)
AS
BEGIN
DELETE FROM Items WHERE Item_ID = @Item_ID
END

DROP PROCEDURE dbo.DeleteItembyID;
-----------------------------------------------------------------------------------------------------------------


