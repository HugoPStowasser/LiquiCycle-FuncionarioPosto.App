﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquiCycle_FuncionarioPosto.Services
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "http://10.0.2.2:5031/api/";

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await client.GetAsync(BaseUrl + url);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", error, "OK");
                    return default(T);
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }

        public async Task<TResult> PostAsync<TData, TResult>(string url, TData data)
        {
            try
            {
                if (data != null)
                {
                    var jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(BaseUrl + url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        await Application.Current.MainPage.DisplayAlert("Error", error, "OK");
                        return default(TResult);
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(responseContent);
                }
                else
                {
                    var response = await client.PostAsync(BaseUrl + url, null);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        await Application.Current.MainPage.DisplayAlert("Error", error, "OK");
                        return default(TResult);
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(responseContent);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }

        public async Task<TResult> PutAsync<TData, TResult>(string url, TData data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(BaseUrl + url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", error, "OK");
                    return default(TResult);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(responseContent);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }


        public async Task<bool> DeleteAsync(string url)
        {
            try
            {
                var response = await client.DeleteAsync(BaseUrl + url);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", error, "OK");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }
    }
}
