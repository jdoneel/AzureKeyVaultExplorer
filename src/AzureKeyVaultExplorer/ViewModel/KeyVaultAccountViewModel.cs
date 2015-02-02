﻿namespace AzureKeyVaultExplorer.ViewModel
{
    using System.Collections.Generic;

    using AzureKeyVaultExplorer.Model;

    using GalaSoft.MvvmLight;

    public class KeyVaultAccountViewModel : ViewModelBase
    {
        private readonly KeyVaultConfiguration keyVaultConfiguration;

        public KeyVaultAccountViewModel(KeyVaultConfiguration keyVaultConfiguration)
        {
            this.keyVaultConfiguration = keyVaultConfiguration;
        }

        public IEnumerable<Key> AllKeys { get; set; }

        public Key SelectedKey { get; set; }
    }
}