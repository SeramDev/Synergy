using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Synergy
{
    class encryption
    {
        public string passHash(string data)
        {
            SHA1 sha = SHA1.Create();
            byte[] hashdata = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnvalue = new StringBuilder();

            for (int i = 0; i < hashdata.Length; i++)
            {
                returnvalue.Append(hashdata[i].ToString());
            }
            return returnvalue.ToString();
        }
    }
}
