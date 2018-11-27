using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RSA;

namespace ConsoleApp1
{
    public static class RSAExtensions
    {
        public static void FromXmlFormatString(this RSACryptoServiceProvider rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Exponent": parameters.Exponent = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "P": parameters.P = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Q": parameters.Q = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DP": parameters.DP = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DQ": parameters.DQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "InverseQ": parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "D": parameters.D = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        public static string ToXmlFormatString(this RSACryptoServiceProvider rsa, bool includePrivateParameters = false)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                  parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                  parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                  parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                  parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                  parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                  parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                  parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                  parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
        }
    }

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
            var xml = RSA_PEM.FromPEM(privateKey).ToXmlFormatString(true);
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlFormatString(xml);

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
            var xml = RSA_PEM.FromPEM(publicKey).ToXmlFormatString(false);
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlFormatString(xml);

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
            var xml = RSA_PEM.FromPEM(privateKey).ToXmlFormatString(true);
            provider.FromXmlFormatString(xml);

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
            var xml = RSA_PEM.FromPEM(publicKey).ToXmlFormatString(false);
            provider.FromXmlFormatString(xml);
            return provider.VerifyData(buffer, "md5", signature);
        }
    }
}