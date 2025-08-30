using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Configuration;
using vesa.core.DTOs;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
