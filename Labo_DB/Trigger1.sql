CREATE TRIGGER [Trigger_ChangementStatut_RetirerProduit]
ON Product
INSTEAD OF DELETE
AS BEGIN
UPDATE p
SET p.IsActif = 0 
FROM Product p
WHERE p.ProductID = (SELECT d.ProductID FROM deleted d);
END;