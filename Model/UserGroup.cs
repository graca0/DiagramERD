using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserGroup
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }

        public ICollection<User>? Users;
    }
}
