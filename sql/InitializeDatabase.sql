USE master
GO
DROP DATABASE IF EXISTS CarvedRock
GO

CREATE DATABASE CarvedRock
GO 
USE CarvedRock
GO 
----------------------------------------------------------------------------
--- TABLE CREATION
----------------------------------------------------------------------------
CREATE TABLE [dbo].[Product](
	[Id] [INT] NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
	[Description] [NVARCHAR](MAX) NULL,
	[Price] [NUMERIC](14, 2) NOT NULL,
	[Category] [NVARCHAR](30) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [UNIQUEIDENTIFIER] NOT NULL,
	[CustomerId] [INT] NOT NULL,
	[ProductId] [INT] NOT NULL,
	[QuantityOrdered] [SMALLINT] NOT NULL,
	[OrderTotal] [NUMERIC](14, 2) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Inventory](
	[ProductId] [INT] NOT NULL,
	[QuantityOnHand] [INT] NOT NULL
) ON [PRIMARY]
GO

----------------------------------------------------------------------------
--- INITIAL DATA
----------------------------------------------------------------------------
INSERT INTO dbo.Product
( Id, Name, Description, Price, Category )
VALUES
( 1, 'Trailblazer', 'Great support in this high-top to take you to great heights and trails.' ,69.99, 'boots' )
INSERT INTO dbo.Inventory ( ProductId, QuantityOnHand ) VALUES ( 1, 100 )

INSERT INTO dbo.Product
( Id, Name, Description, Price, Category )
VALUES
( 2, 'Coastliner', 'Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats.' ,49.99, 'boots' )
INSERT INTO dbo.Inventory ( ProductId, QuantityOnHand ) VALUES ( 2, 50 )

INSERT INTO dbo.Product
( Id, Name, Description, Price, Category )
VALUES
( 3, 'Woodsman', 'All the insulation and support you need when wandering the rugged trails of the woods and backcountry.' ,64.99, 'boots' )
INSERT INTO dbo.Inventory ( ProductId, QuantityOnHand ) VALUES ( 3, 30 )

INSERT INTO dbo.Product
( Id, Name, Description, Price, Category )
VALUES
( 4, 'Billy', 'Get up and down rocky terrain like a billy-goat with these awesome high-top boots with outstanding support.' ,79.99, 'boots' )
INSERT INTO dbo.Inventory ( ProductId, QuantityOnHand ) VALUES ( 4, 20 )

----------------------------------------------------------------------------
--- DB USER CREATION
----------------------------------------------------------------------------
USE master;
GO
CREATE LOGIN [cr_dbuser] WITH PASSWORD=N'Sql1nContainersR0cks!', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE CarvedRock;
GO
CREATE USER [cr_dbuser] FOR LOGIN [cr_dbuser];
GO
EXEC sp_addrolemember N'db_owner', [cr_dbuser];
GO
