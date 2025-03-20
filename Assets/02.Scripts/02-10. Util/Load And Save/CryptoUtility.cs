using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public static class CryptoUtility
{
    private static readonly string _encryptionKey = "2001062620010626"; 
    // 암호화 때 사용한 IV(초기화 벡터)와 동일한 IV를 복호화 때 사용해야 데이터에 접근 가능하다.

    public static string Encrypt(string text)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = new byte[16]; // IV는 0으로 초기화하여 사용
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public static string Decrypt(string encryptedText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = new byte[16]; // IV는 암호화할 때와 동일하게 설정
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
