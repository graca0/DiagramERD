using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderPosition
    {
        [Key]
        public int Id { get; set; }
        #region Order
        public int? OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
        #endregion
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
