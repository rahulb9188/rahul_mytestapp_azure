using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Domain.Entities;

public partial class Customerly_Role
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Customerly_User> Customerly_Users { get; set; } = new List<Customerly_User>();
}
