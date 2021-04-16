using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Tools.Safety
{
    /// <summary>
    /// 加密类
    /// </summary>
    public static class EncryptionHelp
    {
        #region md5加密
        /// <summary>
        /// md5 32位加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string To32MD5(this string source, bool isUpper = true)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                string hash = sBuilder.ToString();
                if (isUpper)
                {
                    return hash.ToUpper();
                }
                else
                {
                    return hash.ToLower();
                }
            }
        }
        /// <summary>
        /// 获取16位md5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string To16MD5(this string source, bool isUpper = true)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
                //转换成字符串，并取9到25位
                string sBuilder = BitConverter.ToString(data, 4, 8);
                //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
                sBuilder = sBuilder.Replace("-", "");
                string hash = sBuilder.ToString();
                if (isUpper)
                {
                    return hash.ToUpper();
                }
                else
                {
                    return hash.ToLower();
                }
            }
        }
        #endregion

        #region AES 加密
        /// <summary>
        /// AES加密
        /// </summary>  
        /// <param name="key">密钥,长度为16的字符串</param>  
        /// <param name="iv">偏移量,长度为16的字符串</param>  
        /// <returns>密文</returns>  
        public static string ToAESEncode(this string text, string key = "bX801f1WbX801f1W", string iv = "bX801f1WbX801f1W")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.Zeros;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = Encoding.UTF8.GetBytes(iv);
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// AES解密
        /// </summary>  
        /// <param name="text">密文</param>  
        /// <param name="key">密钥,长度为16的字符串</param>  
        /// <param name="iv">偏移量,长度为16的字符串</param>  
        /// <returns>明文</returns>  
        public static string ToAESDecode(this string text, string key = "bX801f1WbX801f1W", string iv = "bX801f1WbX801f1W")
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.Zeros;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = Encoding.UTF8.GetBytes(iv);
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }
        #endregion

        #region DES加密
        /// <summary> 
        /// DES加密数据 
        /// </summary> 
        /// <param name="Text">加密字符</param> 
        /// <param name="sKey">密钥</param> 
        /// <returns></returns> 
        public static string ToDESEncode(this string encryptString, string sKey = "bX801f1W")
        {

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(encryptString);
            string md5SKey = sKey.To32MD5().Substring(0, 8);
            des.Key = ASCIIEncoding.ASCII.GetBytes(md5SKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(md5SKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary> 
        /// DES解密数据 
        /// </summary> 
        /// <param name="Text">解密字符</param> 
        /// <param name="sKey">密钥</param> 
        /// <returns></returns> 
        public static string ToDESDecode(string decryptString, string sKey = "bX801f1W")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            string md5SKey = sKey.To32MD5().Substring(0, 8);
            des.Key = ASCIIEncoding.ASCII.GetBytes(md5SKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(md5SKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
