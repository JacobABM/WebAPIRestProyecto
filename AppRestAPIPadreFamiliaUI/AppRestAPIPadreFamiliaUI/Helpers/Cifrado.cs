using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebUIMaestra.Helpers
{
    public static class Cifrado
    {
        public static string GetHash(string clave)
        {
            var algoritmo = SHA512.Create();
            var array = Encoding.UTF8.GetBytes(clave + "str0ngP4ss");
            var result = algoritmo.ComputeHash(array).Select(x => x.ToString("x2"));
            return string.Join("", result);
        }
    }
}
