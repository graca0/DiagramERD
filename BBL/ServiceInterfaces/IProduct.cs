using BBL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBL.ServiceInterfaces
{

    public interface IProduct
    {
        public enum FiltrBy
        {
            Name,
            GroupName,
            GroupId

        }
        IEnumerable<ProductResponse> GetProducts(bool? desc, string? filtrByName, string? filtrByGroup, 
            int? filtrByGroupId, FiltrBy? filtrBy, bool? showNonActive);
        bool RemoveProduct(int productId);
        bool ChangeActivityOfProduct(int productId, bool state);
        bool AddNewProduct(string name, double price, int groupId);

    }
}
