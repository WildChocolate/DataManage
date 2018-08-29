using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common
{
    public class DES
    {
        public DES()//密钥向量,8位就好了例如new byte[]{0x01,0x02,0x03,0x04,0x05,0x05,0x07}
        {
            this.keyvi = new byte[] { 0x01, 0x03, 0x02, 0x04, 0x09, 0x05, 0x06, 0x00};
        }
        private byte[] keyvi;
        private string EncryptKey = "1324956";
        public string DesEncrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var key = Encoding.UTF8.GetBytes(EncryptKey.PadLeft(8, '0').Substring(0, 8));
            using (MemoryStream ms = new MemoryStream())
            {
                var encry = new DESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, encry.CreateEncryptor(key, keyvi), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        
        
        public string DesDecrypt(string securityTxt)//解密
        {
            try
            {
                var bytes = Convert.FromBase64String(securityTxt);
                var key = Encoding.UTF8.GetBytes(EncryptKey.PadLeft(8, '0').Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    var descrypt = new DESCryptoServiceProvider();
                    CryptoStream cs = new CryptoStream(ms, descrypt.CreateDecryptor(key, keyvi), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
