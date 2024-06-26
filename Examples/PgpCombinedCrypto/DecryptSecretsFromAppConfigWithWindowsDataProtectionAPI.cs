﻿using Amazon.Util.Internal.PlatformServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ProtectSecretsWithWindowsDataProtectionAPI;

namespace PgpCombinedCrypto
{

    /// <summary>
    /// This Helper Class uses Windows Data Protection API (DPAPI) to decrypt the OpenPGP private key's Secret Passphrase. https://learn.microsoft.com/en-us/previous-versions/ms995355(v=msdn.10)?redirectedfrom=MSDN 
    /// LIMITATION! This helper class only supports Windows Systems!
    /// Credits: https://weblogs.asp.net/jongalloway/encrypting-passwords-in-a-net-app-config-file 
    /// </summary>
    internal class DecryptSecretsFromAppConfigWithWindowsDataProtectionAPI : IGetSecrets
    {
        /// <summary>
        /// Fetch and Decrypt OpenPGP Private Key's Secret Passphrase
        /// </summary>
        /// <param name="SecretID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetSecretString(string SecretID)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[SecretID];

            if (result != null)
            {
                return Util.ToInsecureString(SecretsDecryptor.DecryptString(result));
            }
            else
            {
                throw new Exception(String.Format("{0} Keys not found in AppConfig!", SecretID));
            }
        }

    }

}
