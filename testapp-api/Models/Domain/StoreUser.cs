using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testapp_api.Models
{
    public class StoreUser: IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        public string FullName { get; set; }
    }
}
