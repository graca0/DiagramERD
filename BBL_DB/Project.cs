using BBL.DTOModels;
using BBL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL_DB
{
    public class Project : IProduct
    {
        public void AddNewProduct(string name, double price, int groupId)
        {
            throw new NotImplementedException();
        }

        public void ChangeActivityOfProduct(int productId, bool state)
        {
            throw new NotImplementedException();
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
