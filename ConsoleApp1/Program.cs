using System;
using System.Security.Cryptography.X509Certificates;
using RSA;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pubkeyPKCS1 = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCW0TOCb7e0lgFoEuMa7klv3QBv
mIypqOeoQGf9tp9UPGbMnqCHWipV2JMmWFBZ6pxS2ssbP7MPFFVjvz5tmbUHtpi4
87C9H6mkGnLBEPNENbRNRQUF4ks+WZUKihjyFnXTvGpNkIDdFlsf4Pma28InHO3M
PhQmId6w+lu0KV6kSwIDAQAB
-----END PUBLIC KEY-----";
            var privkeyPKCS1 = @"-----BEGIN RSA PRIVATE KEY-----
MIICXAIBAAKBgQCW0TOCb7e0lgFoEuMa7klv3QBvmIypqOeoQGf9tp9UPGbMnqCH
WipV2JMmWFBZ6pxS2ssbP7MPFFVjvz5tmbUHtpi487C9H6mkGnLBEPNENbRNRQUF
4ks+WZUKihjyFnXTvGpNkIDdFlsf4Pma28InHO3MPhQmId6w+lu0KV6kSwIDAQAB
AoGAIa0xHU0o7jAYvE6LW7Ydu9bThBfCQRgwMke/cM3Yogyiv8oj+lVN4sFrikJq
HsnegjnM5JbCuvlFX/KSVBLgczKeoJITltn721L38AJA9o1C6KKZDNbK8QwN+n1B
HXIgJm70rDN2x7hY47MloCmT4sRLh9PZJ400xk1X0YiNSSECQQDOFtz+PqjsYRCS
TBFJ0Wt9AH8BtS88KXvLm/zRSOme54PNNwLVFjhbrlHX2CPPVPYbP0/ylxedu9zO
uDE89W4TAkEAu1eS9qrYutiGZbOqzm1wfcI44IOYGExggOee3IqSVELV4Vxzjz7s
ia66cTqZCO1Oz/W3iexTbyxRocC10rdX6QJALH8XO/W/dzoF3/B4kx2aPaULxwyd
pDm4tt95GJ7LgjmuZXKJNATWKkVa+mV6ZduMP5nN4yzoNnBHXoK6btT27QJAZwdQ
cYufyZIOx5u5kfEp7l+D/CUi3ioS4JGBEnS66NAg4+F+RAcs+sM3EdJSG1C3CTNv
DtEDSZ6r/jMHS609EQJBAKWD2JIpJX3uiT4PVPZyAAXl21p2HlRuRw6Zyd8dMzp2
UGkVWJ6fBPq9vYxO1dUr0QYFO+JD6DBLLZlqtOM5Yqs=
-----END RSA PRIVATE KEY-----";

            var pubkeyPKCS8 = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvravqMhSsw4z1Liu+jLi
EscIVv+rLc1u+fuUhXZ39zAgRtD5HmQWDYkFzufw4iFtm2ieQt1y6EpE1pRvUr64
6Fg27KO64mcDeQ1ICIy7ZdkZ37a2N4RvX94Ck2eFL7MSnyevjkE3BTR5SrQSlZiX
4UCrL5vBx93H+MW02PhXyCb3h9Jhv7F6mVsv1at+U+9Bi1fie200G7GC+KPQz98Z
Yxo0+WA8Mu2tmbAB27L8OAOX99dT1+NZsnKLj0WWNqQdubiAvCa7aeFxvlTAY7kG
13cOJFHotPzwVeLrhKASy72Lp82ant0N+3C9Iq7+zdBO/T8q01+99loE1x3VKXD9
8QIDAQAB
-----END PUBLIC KEY-----";
            var privkeyPKCS8 = @"-----BEGIN PRIVATE KEY-----
MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC+tq+oyFKzDjPU
uK76MuISxwhW/6stzW75+5SFdnf3MCBG0PkeZBYNiQXO5/DiIW2baJ5C3XLoSkTW
lG9SvrjoWDbso7riZwN5DUgIjLtl2RnftrY3hG9f3gKTZ4UvsxKfJ6+OQTcFNHlK
tBKVmJfhQKsvm8HH3cf4xbTY+FfIJveH0mG/sXqZWy/Vq35T70GLV+J7bTQbsYL4
o9DP3xljGjT5YDwy7a2ZsAHbsvw4A5f311PX41mycouPRZY2pB25uIC8Jrtp4XG+
VMBjuQbXdw4kUei0/PBV4uuEoBLLvYunzZqe3Q37cL0irv7N0E79PyrTX732WgTX
HdUpcP3xAgMBAAECggEBAJGd+9Hr4vlExt6NhU0UvPmJbxy+an22dh35shWVt1PO
M1bfCNfjo9MB5KVvA8YlsAMRhoWXgL1Mt/L/s2CCr7z4XjehbHiiH0y2j2G30v6Z
HeBrKgaJ4GBaq6zRRih9mqJbMvhzYwdeYZ5gkDAZKx6auhrH2tY4yQ9yJfvk42G2
zNiMoosO+K5JE/HcMHgOtbi69Ydfo5WG4G3h9u7ahjeSDP/h4VT9udSqoFQdwqfl
Cru3A5oE5JUDZzX38255HtRyQMq2Ibjva1muKUC7TE78n+te+xZt47FIIAekd6lq
agyOkVsvjNQ0iHqDlUAoymdk41s/xufVvoTA7LIXpmECgYEA7P8pSnCJrSdmSm6n
57vuHElz1DJVCeHwhaxdz3KwUaIr0FCrrCKRjDfvJV7RRNlEQFg5x4iZMUsozJFE
2YyGUaJRaVXGmXMF3hk5Xq2FKyLFenK59vVEdocbvNAXPiHxoxArnrRtQEjNu+zK
3Qy6Gew/XtG80xTAY88FnB0Kx0MCgYEAzgF4Xy3VO60g4am5ifdo/UY5xDH8J9NM
46czmrbYzp5jIqIppkCTtzRNImXj7sG2CdEg4xx0m0bKuifgMVC1Pewtj9LMHfVL
8OmciHm6z39xSSpLLDTO+ZY87Gd8g+I5fnkjh4z0SPOU0edKPx7xZovoSmOZqCv7
aaTuQ9Z50LsCgYEAvIDLt+DBMQ+YhfVz6ZcJ8kfeMFaEWyLx63DJAPrJEXU7AitY
EMdCG1RWC9RaATgK2F8UPggBSTrtzaByMdNa9s1zkaPfHihl710Cy7KarE+w4Q8l
qS82cExQnKKrCgl3p+pxt37tMud1dFcImD/KOlo1oVaTqRTlXb3JSX20F/8CgYBp
FqbpJuBYCQF1HLfhgay1R1XAmB8h0dCvcWQJ0KzY+kNoL4E/pkG0e6G9omycJ4VN
jLlSIfn6HCreu+jKP3m1lP3JGKe3wKJ0yzdnB3Ic185anJCshGPxPorlrgT+4jCi
K/nL9EPJ0zhfu7VMbeSHg2eESzrfZ1BhnBlP0eFicQKBgHoLxUihYmkphBvcR49y
11rGo2TS0+alTFcqL+XrxbxAWEWpf5pPL/WBR+9CrhuHHnsDeuvbHJX7bZ+GHtfy
epWIwLgZpZP08lC6oefVXmr3ODevXjkIsJ3Z7CSXuujPhSvH2bqRD7Xvw5JU9rTj
FJx3whGJT4/2A5z8jtUz+HQg
-----END PRIVATE KEY-----";
            var stringData = "Hello World!";
            var encryptString = RsaCrypto.Encrypt(pubkeyPKCS1, stringData);
            var decryptString = RsaCrypto.Decrypt(privkeyPKCS1, encryptString);
            var signResult = RsaCrypto.Sign(privkeyPKCS1, encryptString);
            var signVarifiedResult = RsaCrypto.VerifySignature(encryptString, pubkeyPKCS1, signResult);
            Console.WriteLine("EncryptString :" + encryptString);
            Console.WriteLine("DecryptString :" + decryptString);
            Console.WriteLine("VerifySignature :" + signVarifiedResult);
            Console.ReadKey();
        }
    }
}