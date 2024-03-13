using BBL.DTOModels;
using BBL.ServiceInterfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBL_EF
{
    internal class ProductEf : IProduct
    {
        public void AddNewProduct(string name, double price, int groupId)
        {
            using (var db = new WebshopContext())
            {
                var biggestId = db.Product.Max(x => x.Id);
                db.Product.Add(new Model.Product
                {
                    Id = biggestId,
                    Name = name,
                    Price = price,
                    GroupID = groupId,
                });
                db.SaveChanges();
            }
        }

        public void ChangeActivityOfProduct(int productId, bool state)
        {
            using (var db = new WebshopContext())
            {
                db.Product.Single(x => x.Id == productId).IsActive = state;
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductResponse> GetProducts(bool desc, string filtrByName,string filtrByGroup, int filtrByGroupId, IProduct.FiltrBy filtrBy, bool showNonActive)
        {
            using (var db = new WebshopContext())
            {
                var product = db.Product.ToList();
                if (!showNonActive)
                    product = product.Where(x => x.IsActive).ToList();

                if (filtrByName != null || filtrByName != "")
                    product.Where(x => x.Name.Contains(filtrByName)).ToList();

                if (filtrByGroup != null || filtrByGroup != "")
                {
                    var grId = db.ProductGroup.Where(x => x.Name == filtrByGroup).FirstOrDefault();
                    if (grId != null)
                    {
                        product = product.Where(x => x.GroupID == grId.Id).ToList();
                    }
                }

                if (filtrByGroupId != null)
                    product = product.Where(x => x.GroupID == filtrByGroupId).ToList();

                switch (filtrBy)
                {
                    case IProduct.FiltrBy.Name:
                        if (desc)
                            product = product.OrderByDescending(x => x.Name).ToList();
                        else
                            product = product.OrderBy(x => x.Name).ToList();
                        break;
                    //TODO TUTAJ SKONCZYLEM //GRACA
                    //case IProduct.FiltrBy.GroupName:
                    //    if (desc)
                    //        product = product.OrderByDescending(x => x.Name).ToList();
                    //    else
                    //        product = product.OrderBy(x => x.Name).ToList();
                    //    break;
                    case IProduct.FiltrBy.GroupId:
                        if (desc)
                            product = product.OrderByDescending(x => x.GroupID).ToList();
                        else
                            product = product.OrderBy(x => x.GroupID).ToList();
                        break;
                }
            }
            return null;
        }

        public IEnumerable<ProductResponse> GetProducts(bool desc, string filtrByName, int filtrByGroup, string filtrByGroupId, IProduct.FiltrBy filtrBy, bool showNonActive)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
