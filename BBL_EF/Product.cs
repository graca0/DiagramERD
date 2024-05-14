using BBL.DTOModels;
using BBL.ServiceInterfaces;
using DAL;
using Model;
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
    public class Product : IProduct
    {
        public bool AddNewProduct(string name, double price, int groupId)
        {
            try
            {
                using (var db = new WebshopContext())
                {

                     
                    db.Product.Add(new Model.Product
                    {
                        Name = name,
                        Price = price,
                        GroupID = groupId,
                    });
                    db.SaveChanges();
                    return true;
                }
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeActivityOfProduct(int productId, bool state)
        {
            try
            {
                using (var db = new WebshopContext())
                {
                    db.Product.Single(x => x.Id == productId).IsActive = state;
                    db.SaveChanges();
                    return true;
                }
            } catch (Exception ex) { return false; }
        }

        public IEnumerable<ProductResponse> GetProducts(bool? desc, string? filtrByName, string? filtrByGroup, int? filtrByGroupId, IProduct.FiltrBy? filtrBy, bool? showNonActive)
        {
            using (var db = new WebshopContext())
            {
                var product = db.Product.ToList();
                if (showNonActive!=null && !showNonActive.Value)
                    product = product.Where(x => x.IsActive).ToList();

                if (filtrByName != null && filtrByName != "")
                    product = product.Where(x => x.Name.Contains(filtrByName)).ToList();

                if (filtrByGroup != null && filtrByGroup != "")
                {
                    var grId = db.ProductGroup.Where(x => x.Name == filtrByGroup).FirstOrDefault();
                    if (grId != null)
                    {
                        product = product.Where(x => x.GroupID == grId.Id).ToList();
                    }
                }

                if (filtrByGroupId != null &&  filtrByGroupId != 0)
                    product = product.Where(x => x.GroupID == filtrByGroupId).ToList();
                
                bool sortType;
                if(!desc.HasValue)
                    sortType = false;
                else
                    sortType = desc.Value;
                // Sortowanie
                switch (filtrBy)
                {
                    case IProduct.FiltrBy.Name:
                        product = sortType ? product.OrderByDescending(x => x.Name).ToList() : product.OrderBy(x => x.Name).ToList();
                        break;
                    case IProduct.FiltrBy.GroupName:
                        product = sortType ? product.OrderByDescending(x => db.ProductGroup.FirstOrDefault(g => g.Id == x.GroupID).Name).ToList()
                                       : product.OrderBy(x => db.ProductGroup.FirstOrDefault(g => g.Id == x.GroupID).Name).ToList();
                        break;
                    case IProduct.FiltrBy.GroupId:
                        product = sortType ? product.OrderByDescending(x => x.GroupID).ToList() : product.OrderBy(x => x.GroupID).ToList();
                        break;
                }
                var productResponses = new List<ProductResponse>();

                foreach (var p in product)
                {
                    var productResponse = new ProductResponse
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        GroupID = p.GroupID,
                        GroupName = db.ProductGroup.FirstOrDefault(x => x.Id == p.GroupID) != null ? db.ProductGroup.FirstOrDefault(x => x.Id == p.GroupID).Name : ""
                    };

                    productResponses.Add(productResponse);
                }

                return productResponses;
            }
            
        }

        public bool RemoveProduct(int productId)
        {

            using (var db = new WebshopContext())
            {
                var productToRemove = db.Product.FirstOrDefault(p => p.Id == productId);
                if (productToRemove != null)
                {
                    db.Product.Remove(productToRemove);
                    db.SaveChanges();
                    return true;
                }
                return false;

            }
        }
    }
}
