using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePasswordStorage
{

    public class HashedPassword
    {
        public byte[] Salt { get; set; }
        public string Hash { get; set; }

        public HashedPassword(byte[] salt, string hash)
        {
            this.Salt = salt;
            this.Hash = hash;
        }
    }
}
