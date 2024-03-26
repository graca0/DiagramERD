using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        #region parent
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual ProductGroup? Parent { get; set; }
        public virtual ICollection<ProductGroup>? Children { get; set; } 
        #endregion

        public ICollection<Product>? Products { get; set; }
    }
}
