using Application.Interfaces.V1;
using Domain.Entities.Common;
using Domain.ErrorLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services.V1
{
    public class APICallService : IAPICall
    {
        private int _useProxy;
        public APICallService(int UseProxy)
        {
            _useProxy = UseProxy;
        }

        public async Task<RepT> CallingAPI<RepT, ReqT>(string ApiName, ReqT requestBody)
        {
            var apiResponseString = "";
            var errMsg = "No Data Found";
            var apiData = ApiName == "All" ? ApiMasterData.apiData : AllApi.ApiDetails!.FirstOrDefault(x => x.Task_Name == ApiName);  //await _GetApiOnApiName(ApiName);
            if (apiData != null)
            {
                var RequestObject = JsonSerializer.Serialize(requestBody);// use jsonbody instead
                HttpResponseMessage response = null;
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    }
                    // Set up the proxy
                    var handler = new HttpClientHandler();
                    if (_useProxy == 1)
                    {
                        var proxy = new WebProxy($"{apiData.Api_Proxy!}:{apiData.Api_Port!}", true);

                        // Check UserName and Password. Optional, if authentication is needed
                        if (!string.IsNullOrEmpty(apiData.Api_Username!) && !string.IsNullOrEmpty(apiData.Api_Pwd!))
                        {
                            proxy.Credentials = new NetworkCredential(apiData.Api_Username!, apiData.Api_Pwd!);
                        }

                        // Create an HttpClientHandler with the proxy
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
                        _ => throw new ValueNotHandledException(apiData.Api_Http_Method!)
                    };
                    // Convert to HttpClient
                    using (var httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(apiData.Api_Timeout) })
                    {
                        // Create the HttpRequestMessage
                        var httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(apiData.Api_Url!))
                        {
                            Content = new StringContent(RequestObject, Encoding.UTF8, apiData.Api_Content_Type!) // Set content if applicable

                        };

                        // Set headers

                        if (!string.IsNullOrEmpty(apiData.Api_Authorization!))
                        {
                            httpRequestMessage.Headers.Add("Authorization", $"{apiData.Api_Authorization_Type} {apiData.Api_Authorization}");
                            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiAuthorization);
                        }

                        // 'For POST Method
                        if (apiData.Api_Http_Method!.ToUpper() == "POST" || apiData.Api_Http_Method!.ToUpper() == "PATCH" || apiData.Api_Http_Method!.ToUpper() == "PUT")
                        {
                            // Set Content-Length manually (not usually recommended)
                            httpRequestMessage.Content.Headers.ContentLength = Encoding.UTF8.GetByteCount(RequestObject);
                        }
                        // Send the request
                        response = await httpClient.SendAsync(httpRequestMessage);

                        // Ensure we get a successful response
                        response.EnsureSuccessStatusCode();

                        //if (response.IsSuccessStatusCode)
                        //{
                        //    // Read response body
                        //    return await response.Content.ReadFromJsonAsync<RepT>(); //apiResponseString = await response.Content.ReadAsStringAsync();
                        //    // Deserialize JSON into ApiResponse object
                        //    // var apiResponse = JsonConvert.DeserializeObject<StoreName_CounterNo>(apiResponseString);

                        //}
                        //apiResponseString = await response.Content.ReadAsStringAsync();
                        //return (await response!.Content!.ReadFromJsonAsync<RepT>())!;
                        var contentType = response.Content.Headers.ContentType?.MediaType;
                        if (contentType == "application/pdf" || contentType == "application/octet-stream" || contentType== "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            var bytes = await response.Content.ReadAsByteArrayAsync();
                            // wrap into a custom FileResult object
                            var base64 = Convert.ToBase64String(bytes);
                            //var fileResultJson = $"{{\"FileBase64\":\"{base64}\"}}";
                            //return JsonSerializer.Deserialize<RepT>(fileResultJson)!;
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
            else
            {
                errMsg = $"API data for task '{ApiName}' not found.";
                apiResponseString = "{\"responseCode\":1000,\"responseMessage\":\"" + errMsg + "\"}";
            }

            //--------------Response return
            return JsonSerializer.Deserialize<RepT>(apiResponseString)!;
        }
    }
}
