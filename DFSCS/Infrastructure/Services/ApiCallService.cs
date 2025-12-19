using Application.Common.Interfaces;
using Domain.Entities.Common;
using MyNewEncDec;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ApiCallService : IApiCall
    {
        private int _useProxy;
        public ApiCallService(int UseProxy)
        {
            _useProxy = UseProxy;
        }

        public async Task<RepT> CallApi<RepT, ReqT>(ReqT requestBody, ApiData apiData)
        {
            var apiResponseString = "";
            var errMsg = "No Data Found";
            if (apiData != null)
            {
                var RequestObject = JsonSerializer.Serialize(requestBody);// use jsonbody instead
                HttpResponseMessage response = null;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    if (!ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) && !ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)12288))
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | (SecurityProtocolType)12288;
                    }
                    // Set up the proxy
                    var handler = new HttpClientHandler();
                    if (_useProxy == 1)
                    {
                        var proxy = new WebProxy($"{apiData.Api_Proxy!}:{apiData.Api_Port!}", true);
                        if (!string.IsNullOrEmpty(apiData.Api_Username!) && !string.IsNullOrEmpty(apiData.Api_Pwd!))
                        {
                            proxy.Credentials = new NetworkCredential(apiData.Api_Username!, apiData.Api_Pwd!);
                        }
                        handler = new HttpClientHandler
                        {
                            Proxy = proxy,
                            UseProxy = true
                        };
                    }

                    HttpMethod httpMethod = apiData.Api_Http_Method switch
                    {
                        "GET" => HttpMethod.Get,
                        "POST" => HttpMethod.Post,
                        "PATCH" => HttpMethod.Patch,
                        "PUT" => HttpMethod.Put,
                        _ => throw new KeyNotFoundException(apiData.Api_Http_Method!)
                    };

                    using (var httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(apiData.Api_Timeout) })
                    {
                        var httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(apiData.Api_Url!))
                        {
                            Content = new StringContent(RequestObject, Encoding.UTF8, apiData.Api_Content_Type!) // Set content if applicable
                        };

                        if (!string.IsNullOrEmpty(apiData.Api_Authorization!))
                        {
                            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(apiData.Api_Authorization_Type == null ? "Bearer" : apiData.Api_Authorization_Type.Trim(), apiData.Api_Authorization.Contains("Bearer") ? apiData.Api_Authorization.Trim().Remove(0, 7) : apiData.Api_Authorization.Trim());
                        }

                        if (!string.IsNullOrEmpty(apiData.Api_Username!) && !string.IsNullOrEmpty(apiData.Api_Pwd!))
                        {
                            New_Enc_Dec _Enc_Dec = new New_Enc_Dec();
                            string username = apiData.Api_Username!;
                            string pwd = _Enc_Dec.My_Decode(apiData.Api_Pwd!);
                            string credentials = $"{username}:{pwd}";

                            // Encode credentials to Base64
                            string base64Credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials));

                            // Add Authorization header
                            httpRequestMessage.Headers.Add("Authorization", $"Basic {base64Credentials}");

                        }
                        if (apiData.Api_Http_Method!.ToUpper() == "POST" || apiData.Api_Http_Method!.ToUpper() == "PATCH" || apiData.Api_Http_Method!.ToUpper() == "PUT")
                        {
                            httpRequestMessage.Content.Headers.ContentLength = Encoding.UTF8.GetByteCount(RequestObject);
                        }
                        response = await httpClient.SendAsync(httpRequestMessage);
                        response.EnsureSuccessStatusCode();
                        var contentType = response.Content.Headers.ContentType?.MediaType;
                        if (contentType == "application/pdf" || contentType == "application/octet-stream" || contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            var bytes = await response.Content.ReadAsByteArrayAsync();
                            var base64 = Convert.ToBase64String(bytes);
                            object? result = base64;
                            return (RepT)result!;
                        }
                        else
                        {
                            apiResponseString = await response.Content.ReadAsStringAsync();
                            return (await response!.Content!.ReadFromJsonAsync<RepT>())!;
                        }
                    }
                }
                catch (WebException ex1)
                {
                    errMsg = ex1.Message;
                    if (response != null)
                    {
                        if (apiResponseString.ToLower().Contains("<html") || apiResponseString.ToLower().Contains("<?xml"))
                        {
                            apiResponseString = "{\"responseCode\":1001,\"responseMessage\":\"" + errMsg + "\"}";
                        }
                        else if (apiResponseString.Trim() == "")
                        {
                            apiResponseString = "{\"responseCode\":1002,\"responseMessage\":\"" + errMsg + "\"}";
                        }
                    }
                    else
                    {
                        apiResponseString = "{\"responseCode\": 1003,\"responseMessage\":\"" + errMsg + "\"}";
                    }
                }
                catch (Exception ex2)
                {
                    errMsg = ex2.Message;
                    apiResponseString = "{\"responseCode\":1004,\"responseMessage\":\"" + errMsg + "\"}";
                }
            }
            //--------------Response return
            return JsonSerializer.Deserialize<RepT>(apiResponseString)!;
        }
    }
}
