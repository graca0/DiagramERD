using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using BBL.DTOModels;
using BBL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;

namespace BBL_EF
{
    public class Basket : IBasket
    {
        private readonly WebshopContext db;
        public Basket(WebshopContext dbContext)
        {
            this.db = dbContext;
        }
        public bool AddProductToBasket(BasketRequest basket)
        {
            var product = db.Product.FirstOrDefault(x => x.Id == basket.ProductId);
            if (product != null && basket.Amount >= 0)
            {
                db.BasketPosition.Add(new Model.BasketPosition()
                {
                    ProductId = basket.ProductId,
                    Users = db.User.Where(x => x.Id == basket.UserId).ToList(),
                    Amount = basket.Amount
                });
                db.Update(db.BasketPosition);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeBasketAmount(BasketRequest basket)
        {
            var user = db.User.FirstOrDefault(y => y.Id == basket.UserId);
            var bp = db.BasketPosition?
                .Where(x => x.ProductId == basket.ProductId)
                .Where(x => x.Users.FirstOrDefault(y => y.Id == basket.UserId) != null)
                .FirstOrDefault();
            if (bp != null && basket.Amount >= 0)
            {
                bp.Amount = basket.Amount;
                db.Update(bp);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool GenerateOrder(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Payment(int userId, int payment)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProductBasket(int productId, int userId)
        {
            var bp = db.BasketPosition?
                .Where(x => x.ProductId == productId)
                .Where(x => x.Users.FirstOrDefault(y => y.Id == userId) != null)
                .FirstOrDefault();

            if (bp != null)
            {
                db.Remove(bp);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
