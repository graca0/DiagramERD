using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        #region User
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        #endregion
        public DateTime Date { get; set; }

        public ICollection<OrderPosition>? OrderPositions { get; set; } 
    }
}
