using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PettiInn.Utilities
{
    public static class Security
    {
        /// <summary>
        /// 生成随机明文密码
        /// </summary>
        /// <param name="length">要生成的密码长度</param>
        /// <returns>明文密码</returns>
        public static string GeneratePassword(int length)
        {
            var crypt = new RNGCryptoServiceProvider();
            var random = new byte[length];

            crypt.GetBytes(random);

            //可能出现在密码中的字符
            var chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                // for each position in the password, get the modulus position from the characters:
                password[i] = chars[(int)random[i] % chars.Length];
            }

            return new string(password);
        }

        /// <summary>
        /// 生成密码16位种子
        /// </summary>
        /// <returns>随机16位密码种子</returns>
        public static string GenerateSalt()
        {
            var crypto = new RNGCryptoServiceProvider();
            var buffer = new byte[16];

            crypto.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// 用SHA256算法对明文密码加密
        /// 注：此方式加密后的密码不可还原
        /// </summary>
        /// <param name="Password">用户输入的密码</param>
        /// <param name="Salt">数据库里的密码种子</param>
        /// <returns>HASH后的密码</returns>
        public static string HashPassword(string Password, string Salt)
        {
            var algorithm = new SHA256Managed();
            var passwordBytes = Encoding.UTF8.GetBytes(Password);
            var saltBytes = Encoding.UTF8.GetBytes(Salt);

            byte[] plainTextWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                plainTextWithSaltBytes[i] = passwordBytes[i];
            }
            for (int i = 0; i < saltBytes.Length; i++)
            {
                plainTextWithSaltBytes[passwordBytes.Length + i] = saltBytes[i];
            }

            var hashed = algorithm.ComputeHash(plainTextWithSaltBytes);
            var result = Convert.ToBase64String(hashed);
            result = string.Concat(result, Salt);

            return result;
        }

        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="DBencryptedPassword">数据库储存的加密密码</param>
        /// <param name="PasswordToValidate">用户输入的密码</param>
        /// <param name="salt">数据库里的密码种子</param>
        /// <returns>是否成功</returns>
        public static bool ValidatePassword(string DBencryptedPassword, string PasswordToValidate)
        {
            bool result = false;
            var salt = DBencryptedPassword.Substring(DBencryptedPassword.Length - 24);

            string encryptedPassword = HashPassword(PasswordToValidate, salt);

            //加密后的密码全是大写
            if (encryptedPassword.ToUpper() == DBencryptedPassword.ToUpper())
            {
                result = true;
            }

            return result;
        }

        /// <!--DEC 加密法 --> 
        /// <summary> 
        /// DEC 加密法
        /// </summary> 
        /// <param name="pToEncrypt">加密的字串</param> 
        /// <param name="sKey">加密金鎖</param> 
        /// <param name="sIV">初始化向量</param> 
        /// <returns></returns> 
        public static string Encrypt(string pToEncrypt, string sKey, string sIV)
        {
            StringBuilder ret = new StringBuilder();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //將字元轉換為Byte 
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                //設定加密金鑰(轉為Byte) 
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                //設定初始化向量(轉為Byte) 
                des.IV = ASCIIEncoding.ASCII.GetBytes(sIV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    //輸出資料 
                    foreach (byte b in ms.ToArray())
                        ret.AppendFormat("{0:X2}", b);
                }
            }
            //回傳 
            return ret.ToString();
        }

        /// <!--DEC 解密法--> 
        /// <summary> 
        /// DEC 解密法 - design By Phoenix 2008 - 
        /// </summary> 
        /// <param name="pToDecrypt">解密的字串</param> 
        /// <param name="sKey">加密金鑰</param> 
        /// <param name="sIV">初始化向量</param> 
        /// <returns></returns> 
        public static string Decrypt(string pToDecrypt, string sKey, string sIV)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {

                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                //反轉 
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                //設定加密金鑰(轉為Byte) 
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                //設定初始化向量(轉為Byte) 
                des.IV = ASCIIEncoding.ASCII.GetBytes(sIV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        //例外處理 
                        try
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            //輸出資料 
                            return Encoding.Default.GetString(ms.ToArray());
                        }
                        catch (CryptographicException)
                        {
                            //若金鑰或向量錯誤，傳回N/A 
                            return null;
                        }
                    }
                }
            }
        }
        
    }
}
