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
        bool AddProductToBasket(BasketRequest basket);
        bool ChangeBasketAmount(BasketRequest basket);
        bool RemoveProductBasket(int productId, int userId);
        bool GenerateOrder(int userId);
        bool Payment(int userId, int payment);

    }
}
