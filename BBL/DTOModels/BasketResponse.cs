using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL.DTOModels
{
    public class BasketResponse
    {
        public int UserId {  get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
