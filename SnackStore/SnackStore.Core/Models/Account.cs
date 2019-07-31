using SnackStore.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SnackStore.Core.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<ProductLike> ProductLikes { get; set; }

       
    }

    public enum Role
    {
        Admin = 0,
        Guest = 1
    }
}

