using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp_api
{
    public class IdentityOptions
    {
        public const string ISSUER = "http://localhost:5000"; // издатель токена
        public const string AUDIENCE = "http://localhost:4200"; // потребитель токена
        const string KEY = "secret secret secret secret secret secret";   // ключ для шифрации
        public const int LIFETIME = 30; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
