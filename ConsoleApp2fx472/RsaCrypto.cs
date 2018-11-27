using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RSA;

namespace ConsoleApp2fx472
{
    public class RsaCrypto
    {
        /// <summary>
        /// RSA 私钥解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Decrypt(string privateKey, string content)
        {
            var xml = RSA_PEM.FromPEM(privateKey).ToXmlString(true);
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xml);

            var cipherbytes = provider.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        /// <summary>
        /// RSA 公钥加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Encrypt(string publicKey, string content)
        {
            var xml = RSA_PEM.FromPEM(publicKey).ToXmlString(false);
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xml);

            var cipherbytes = provider.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// 私钥签名
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Sign(string privateKey, string ciphertext)
        {
            var buffer = Encoding.UTF8.GetBytes(ciphertext);
            var provider = new RSACryptoServiceProvider();
            var xml = RSA_PEM.FromPEM(privateKey).ToXmlString(true);
            provider.FromXmlString(xml);

            var signBytes = provider.SignData(buffer, "md5");
            return Convert.ToBase64String(signBytes);
        }

        /// <summary>
        /// 使用公钥验签
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="publicKey"></param>
        /// <param name="signedData"></param>
        /// <returns></returns>
        public static bool VerifySignature(string plainText, string publicKey, string signedData)
        {
            var buffer = Encoding.UTF8.GetBytes(plainText);
            var signature = Convert.FromBase64String(signedData);
            var provider = new RSACryptoServiceProvider();
            var xml = RSA_PEM.FromPEM(publicKey).ToXmlString(false);
            provider.FromXmlString(xml);
            return provider.VerifyData(buffer, "md5", signature);
        }
    }
}