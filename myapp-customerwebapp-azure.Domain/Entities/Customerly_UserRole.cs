using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure;

public partial class Customerly_UserRole
{
    public int UserRoleID { get; set; }

    public int RoleID { get; set; }

    public string UserID { get; set; } = null!;

    public virtual Customerly_Role Role { get; set; } = null!;

    public virtual Customerly_User User { get; set; } = null!;
}
