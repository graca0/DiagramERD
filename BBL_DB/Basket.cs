using BBL.DTOModels;
using BBL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL_DB
{
    public class Basket : IBasket
    {
        public void AddProductToBasket(BasketRequest basket)
        {
            throw new NotImplementedException();
        }

        public void ChangeBasketQuantity(BasketRequest basket)
        {
            throw new NotImplementedException();
        }

        public void GenerateOrder(int userId)
        {
            throw new NotImplementedException();
        }

        public void Payment(int userId, int payment)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductBasket(int productId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
