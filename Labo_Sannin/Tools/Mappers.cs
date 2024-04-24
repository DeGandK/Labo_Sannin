using Labo_BLL.Models;
using Labo_Domain.Models;
using Labo_Sannin_API.Models;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Labo_Sannin_API.Tools
{
    public static class Mappers
    {
        public static Product ToDOMAIN(this ProductCreateForm form)
        {
            return new Product
            {
                Nom = form.Nom,
                Description = form.Description,
                Stock = form.Stock,
                PrixHTVA = form.PrixHTVA,
                Image = form.Image,
                CategorieID = form.CategorieID,
            };
        }
        public static User ToDOMAIN(this UserUpdateForm form)
        {
            return new User
            {
                Adresse = form.Adresse,
                Telephone = form.Telephone
            };
        }
        public static Command ToDOMAIN(this CommandCreateForm form)
        {
            return new Command
            {
                UserID = form.UserID,
                IsPaid = form.IsPaid,
                DateCommande = form.DateCommande 
            };
        }
        public static CommandRow ToDOMAIN(this Models.CommandRowCreateForm form) 
        {
            return new CommandRow
            {
                ProductID = form.ProductID,
                Quantite = form.Quantite,
            };
        }
        public static CommandRow ToDOMAIN1(this CommandRowCreateForm form)
        {
            return new CommandRow
            {
                ProductID = form.ProductID,
                Quantite = form.Quantite,
            };
        }
        public static CompleteCommand ToBLL(this CommandCreateForm form) 
        {
            return new CompleteCommand
            {
                UserID = form.UserID,
                IsPaid = form.IsPaid,
                DateCommande = DateTime.Now,
                CommandRows = form.produitChoisis.Select(s => s.ToDOMAIN1()),
            };
        }
    }
}
