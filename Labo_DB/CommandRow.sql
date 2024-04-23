CREATE TABLE [dbo].[CommandRow]
(
	[LigneCommandeID] INT NOT NULL PRIMARY KEY IDENTITY,
	[CommandID] INT NOT NULL,
	[ProductID] INT NOT NULL,
	[Quantite] INT NOT NULL, 
    CONSTRAINT [FK_CommandRow_Product] FOREIGN KEY ([ProductID]) REFERENCES [Product]([ProductID]) ON DELETE CASCADE, 
    CONSTRAINT [FK_CommandRow_Command] FOREIGN KEY ([CommandID]) REFERENCES [Command]([CommandID])
)
