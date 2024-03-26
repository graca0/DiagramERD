using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BasketPosition
    {
        [Key]
        public int Id { get; set; }

        #region ProductId
        public int? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
        #endregion

        public ICollection<User>? Users { get; set; }
        public int Amount { get; set; }
    }
}
