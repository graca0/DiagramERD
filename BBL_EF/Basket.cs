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

namespace BBL_EF
{
    public class Basket : IBasket
    {

        public void AddProductToBasket(BasketRequest basket)
        {
            db.BasketPosition.Add(new Model.BasketPosition()
            {
                ProductId = basket.ProductId,
                Users = db.User.Where(x => x.Id == basket.UserId).ToList(),
                Amount = basket.Quantity
            }) ;
        }

        public void ChangeBasketQuantity(BasketRequest basket)
        {
            var user = db.User.FirstOrDefault(y => y.Id == basket.UserId);
            if (user != null)
            {
                db.BasketPosition.Where(x=>x.ProductId==basket.ProductId).
            }
        }

        public void GenerateOrder(int userId)
        {
           
        }

        public void Payment(int userId, int payment)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductBasket(int productId, int userId)
        {
            db.
        }
    }
}
