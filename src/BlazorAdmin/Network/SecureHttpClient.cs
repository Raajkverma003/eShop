﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAdmin.Constants;
using BlazorAdmin.Services;
using static BlazorAdmin.Pages.Index;

namespace BlazorAdmin.Network
{
    public class SecureHttpClient
    {
        private readonly HttpClient client;

        public SecureHttpClient(HttpClient client)
        {
            this.client = client;

            this.client.DefaultRequestHeaders.Add("Authorization", $"Bearer ");
        }

        public async Task<List<CatalogBrand>> GetCatalogBrandsAsync()
        {
            var brands = new List<CatalogBrand>();

            try
            {
                brands = (await client.GetFromJsonAsync<CatalogBrandResult>($"{GeneralConstants.API_URL}catalog-brands")).CatalogBrands;
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }

            return brands;
        }
    }
}
