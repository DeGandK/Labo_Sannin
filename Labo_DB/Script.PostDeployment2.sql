/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
CREATE TRIGGER [Trigger_ChangementStatut_RetirerProduit]
ON Product
INSTEAD OF DELETE
AS BEGIN
UPDATE p
SET p.IsActif = 0 
FROM Product p
WHERE p.ProductID = (SELECT d.ProductID FROM deleted d);
END;