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
UPDATE Product 
SET IsActif = 0 
FROM deleted 
WHERE ProductID = (SELECT ProductID FROM deleted);
END;

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Saurisserie', 'produit de la mer', 6)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Boucherie', 'maturation et découpe et vend au détail de la viande', 6)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Charcuterie', 'spécialités alimentaires obtenues suite à la transformation de viande', 6)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Cremerie', 'Vente des produits laitiers et des oeufs', 6)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Boulangerie', 'Vente de produits transformés à base de farine', 6)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Sec', 'Vente de produit sec, chips, farine, etc', 6)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Electronique', 'produit contenant des composants électronique', 21)

INSERT INTO Categories (Nom, Description, TauxTVA) VALUES ('Enfant', 'produit destiné pour les enfants, comme des jouets, couches, lait en poudre', 21)


INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Homard', 'Homards rouge', 3, 39.99,'https://thumbs.dreamstime.com/b/homard-d-isolement-sur-un-fond-blanc-69724100.jpg',1)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Crevettes','Crevettes grises', 10, 5.99,'https://jeanderoy.fr/storage/2021/04/IMG_1622.jpg',1)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Noix de St-Jacques', 'Petites noix de st-jacques', 5, 9.99,'https://www.alexya.fr/wp-content/uploads/2016/09/Recette-noix-de-saint-jacques-13.jpg',1)

INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES ('Entrecote', 'Viande de boeuf maturée 14 jours',5, 18, 'https://difalux.be/wp-content/uploads/2021/02/2278-entrecote-rubia-maturee-4-br.jpg',2)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES('Chipolata', 'Fine saucisse de porc avec des herbes et épices',42, 5, 'https://www.papillesetpupilles.fr/wp-content/uploads/2022/01/Chipolata-Dgraph88-CC0-Pixabay-.jpg',2)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES('Cotelette d''agneau', 'Petite cote d’agneau d’Ecosse',12, 15, 'https://www.boucherie-gillotjohn.fr/wp-content/uploads/2018/03/VISUELS-COTELETTES.jpg', 2)

INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES ('Bacon', 'tranche de poitrine de porc',6000, 3.50, 'https://lovefoodhatewaste.ca/wp-content/uploads/2021/03/bacon-1024x709.jpg',3)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES('Rosette', 'Large saucisson de lyon',45, 2, 'https://www.legourmeur.fr/wp-content/uploads/2020/05/rosette-de-lyon-2-scaled.jpg',3) 
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES('Guanciale', 'Joue de porc pour la vrai carbonara',12, 4, 'https://honest-food.net/wp-content/uploads/2011/07/guanciale-shutterstock.jpg',3)

INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Danette', 'Pudding au chocolat', 20, 3.99,'https://i.pinimg.com/originals/f5/5b/71/f55b712dfc8eaec54fda4fd30408b892.jpg',4)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Gouda', 'Tranches de gouda', 6, 2.99,'https://cdn.carrefour.eu/1200_06069219_3560070423309_00.jpeg',4)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Oeufs bio','Boite de 6 oeufs', 2, 2.50,'https://www.affinord.com/assets/uploads/images/products/zoom/2489-SiO0.JPG',4)

INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES ('Baguette', 'Long Pain blanc',65, 0.90, 'https://www.maspatule.com/blog/wp-content/uploads/2020/03/IMG_MISE_EN_AVT_pain-1440x1128.jpg',5)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES('Pain de campagne', 'pain blanc et rond bien cuit avec une croute croustiante',39, 1.50, 'https://www.maison-kayser.com/info/wp-content/uploads/2018/04/Recette-de-Pain-de-Campagne-Eric-Kayser.jpg',5)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES('Croissant', 'Viennoiserie de pate feuilletée avec du beurre',69, 0.80, 'https://www.delifrance.com/media/catalog/product/cache/a2b5c37732c9b5ce289563642b3b2aeb/1/9/19477_1.jpg', 5)

INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Chips Lays Paprika','Chips croquante goût', 10, 1.99,'https://cdn.carrefour.eu/1200_06540407_8710398603234_01.jpeg',6)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Special K', 'Céréales Kellogs au chocolat', 5, 4.99, 'https://m.ftscrt.com/food/46fead57-c2fa-46a1-8184-4c6b7538576a_lg_sq.jpg' ,6)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Tuc', 'Biscuit crakers goût bacon', 7, 1.85, 'https://www.worldofsweets.de/out/pictures/master/product/1/tuc-bacon-100g-no1-4114.jpg',6)

INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Duracel', 'Lots de piles', 4, 9.99,'https://www.vindiqoffice.com/resize/4141025_P-HR-20220725.jpg/0/1100/True/duracell-piles-plus-100-aa-blister-16-pieces.jpg',7)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Samsung','Telephone',2, 199.99,'https://media.ldlc.com/ld/products/00/04/49/26/LD0004492623_2.jpg',7)
INSERT INTO Product (Nom, Description, Stock, PrixHTVA, Image, CategorieID) VALUES ('Gps', 'Gps',1, 159.99,'https://img.fruugo.com/product/8/50/366967508_max.jpg',7)

INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES ( 'Oliplum', 'Peluche bébé éléphant rose',25, 12.50, 'https://la-peluche.com/wp-content/uploads/2020/04/product-image-552522656.jpg', 8)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES ('Lego Ninjago', 'Jouet de construction Lego Ninjago',10, 25, 'https://m.media-amazon.com/images/I/815BBPcE92L.jpg', 8)
INSERT INTO Product (Nom, Description,Stock, PrixHTVA, Image, CategorieID) VALUES ( 'Pulzze Disney', 'Disney princesse Disney la reine des neige',4, 10,'https://data.puzzle.be/ravensburger.5/2-puzzles-disney-la-reine-des-neiges-puzzle-24-pieces.46917-1.fs.jpg', 8)