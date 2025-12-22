using Application.Interfaces.V1;
using Microsoft.JSInterop;
using MyNewEncDec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _js;
        private readonly New_Enc_Dec _Enc_Dec;

        public LocalStorageService(IJSRuntime js, New_Enc_Dec Enc_Dec)
        {
            _js = js;
            _Enc_Dec = Enc_Dec;
        }
        public async Task SetItemAsync<T>(string key, T value)
        {
            var json =  JsonSerializer.Serialize(value);
            var encrypted = _Enc_Dec.My_Encode(json);
            await _js.InvokeVoidAsync("localStorage.setItem", key, encrypted);
        }
        public async Task<T?> GetItemAsync<T>(string key)
        {
            var encrypted = await _js.InvokeAsync<string>("localStorage.getItem", key);
            if (string.IsNullOrWhiteSpace(encrypted)) return default;

            var decrypted = _Enc_Dec.My_Decode(encrypted);
            return JsonSerializer.Deserialize<T>(decrypted);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
