﻿namespace AzureKeyVaultExplorer.Model
{
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    using AzureKeyVaultExplorer.Interface;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Microsoft.KeyVault.Client;

    public class KeyOperations : IKeyOperations
    {
        private readonly KeyVaultConfiguration keyVaultConfiguration;

        private readonly KeyVaultClient keyVaultClient;

        public KeyOperations(KeyVaultConfiguration keyVaultConfiguration)
        {
            this.keyVaultConfiguration = keyVaultConfiguration;
            this.keyVaultClient = new KeyVaultClient(this.HandleKeyVaultAuthenticationCallback);
        }

        public async Task<string> Encrypt(Key key, string dataToEncrypt)
        {
            var byteData = Encoding.Unicode.GetBytes(dataToEncrypt);
            var result = await this.keyVaultClient.EncryptDataAsync(key.KeyIdentifier, "RSA_OAEP", byteData);
            return Encoding.Unicode.GetString(result.Result);
        }

        public Task<string> Decrypt(Key key, string dataToDecrypt)
        {
            throw new System.NotImplementedException();
        }

        private string HandleKeyVaultAuthenticationCallback(string authority, string resource, string scope)
        {
            var adCredential = new ClientCredential(
                this.keyVaultConfiguration.ADApplicationClientId,
                this.keyVaultConfiguration.ADApplicationSecret);
            var authenticationContext = new AuthenticationContext(authority, null);
            return authenticationContext.AcquireToken(resource, adCredential).AccessToken;
        }
    }
}