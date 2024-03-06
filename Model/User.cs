using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public AcType Type { get; set; }
        public bool IsActive { get; set; }

        #region UserGroups
        public int? UserGroupId { get; set; }
        [ForeignKey(nameof(UserGroupId))]
        public UserGroup? UserGroups { get; set; }
        #endregion

        #region BasketPosition
        public int? BasketPositionId { get; set; }
        [ForeignKey(nameof(BasketPositionId))]
        public BasketPosition? BasketPosition { get; set; }
        #endregion
        
        public ICollection<Order>? Orders { get; set; }
    }
}
