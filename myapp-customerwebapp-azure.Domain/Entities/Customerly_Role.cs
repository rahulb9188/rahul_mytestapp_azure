using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure;

public partial class Customerly_Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Customerly_UserRole> Customerly_UserRoles { get; set; } = new List<Customerly_UserRole>();
}
