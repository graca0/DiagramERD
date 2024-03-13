using BBL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL.ServiceInterfaces
{
    public interface IBasket
    {
        void AddProductToBasket(BasketResponse basket);
        void ChangeBasketQuantity(BasketResponse basket);
        void RemoveProductBasket(int productId, int userId);
        void GenerateOrder(int userId);
        void Payment(int userId, int payment);

    }
}
