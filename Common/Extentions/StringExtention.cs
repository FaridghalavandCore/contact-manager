﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Extentions
{
    public static class StringExtention
    {
        public static string EncryptSha256( this string value)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}