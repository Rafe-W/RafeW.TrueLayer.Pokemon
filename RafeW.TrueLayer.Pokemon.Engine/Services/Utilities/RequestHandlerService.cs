using Microsoft.Extensions.Configuration;
using RafeW.TrueLayer.Pokemon.Engine.Entities;
using RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities;
using RafeW.TrueLayer.Pokemon.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Services.Utilities
{
    public interface IRequestHandlerService<TApiSettings> where TApiSettings : IApiSettings, new()
    {
        public Task<TResponse> SendRequest<TResponse>(string path, HttpMethod method, object bodyArgs = null);
        Task<RequestResult<TResponse>> TrySendRequest<TResponse>(string path, HttpMethod method, object bodyArgs = null);
    }

    [Injectable]
    public class RequestHandlerService<TApiSettings> : IRequestHandlerService<TApiSettings> where TApiSettings : IApiSettings, new()
    {
        public TApiSettings Settings { get; }
        public RequestHandlerService(IConfiguration configuration)
        {
            var settings = new TApiSettings();
            var configSection = configuration.GetSection("ExternalApiConfig");
            var apiSection = configSection.GetSection(settings.ApiIdentifier);

            settings.BaseUrl = new Uri(apiSection["BaseUrl"]);
            settings.DefaultHeaders = apiSection.GetSection("DefaultHeaders")?.GetChildren().ToDictionary(kv => kv.Key, kv => kv.Value) ?? new Dictionary<string, string>();

            Settings = settings;
        }

        public RequestHandlerService(TApiSettings settings)
        {
            Settings = settings;
        }

        public async Task<TResponse> SendRequest<TResponse>(string path, HttpMethod method, object bodyArgs = null)
        {
            using (var client = CreateClient())
            {
                using (var request = CreateRequest(path, method, bodyArgs))
                {
                    var result = await client.SendAsync(request);

                    result.EnsureSuccessStatusCode();

                    return await result.Content.ReadFromJsonAsync<TResponse>();
                }
            }
        }

        public async Task<RequestResult<TResponse>> TrySendRequest<TResponse>(string path, HttpMethod method, object bodyArgs = null)
        {
            var result = new RequestResult<TResponse>();
            try
            {
                var response = await SendRequest<TResponse>(path, method, bodyArgs);
                result.SetSuccess(response);
                return result;
            }
            catch(Exception ex)
            {
                result.SetFailure(ex);
                return result;
            }
        }

        public HttpRequestMessage CreateRequest(string path, HttpMethod method, object bodyArgs)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            if (bodyArgs != null)
            {
                request.Content = JsonContent.Create(bodyArgs);
            }

            return request;
        }

        public HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = Settings.BaseUrl;
            foreach (var header in Settings.DefaultHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            return httpClient;
        }
    }
}
