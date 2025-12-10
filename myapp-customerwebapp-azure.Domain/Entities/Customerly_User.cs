using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure;

public partial class Customerly_User
{
    public string UserId { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public int? Role { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpires { get; set; }

    public virtual ICollection<Customerly_UserRole> Customerly_UserRoles { get; set; } = new List<Customerly_UserRole>();
}
