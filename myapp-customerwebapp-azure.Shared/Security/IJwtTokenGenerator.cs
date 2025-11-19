using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Shared.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(string username);
        string GenerateRefreshToken(string username);

    }
}
