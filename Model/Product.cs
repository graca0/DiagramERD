using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }

        #region Group
        public int? GroupID { get; set; }

        [ForeignKey(nameof(GroupID))]
        public ProductGroup? ProductGroup { get; set; }
        #endregion
        public ICollection<BasketPosition>? BasketPositions { get; set; }
    }
}
