﻿CREATE TABLE [dbo].[Product]
(
	[ProductID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Nom] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(200) NOT NULL,
	[Stock] INT NOT NULL,
	[PrixHTVA] MONEY NOT NULL,
	[Image] VARCHAR(MAX) NOT NULL,
	[CategorieID] INT NOT NULL,
	[IsActif] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Product_Categories] FOREIGN KEY ([CategorieID]) REFERENCES [Categories]([CategorieId])
)
