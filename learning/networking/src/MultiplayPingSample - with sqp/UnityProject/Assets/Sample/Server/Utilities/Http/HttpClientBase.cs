using System;
using System.Text;
using MultiplayPingSample.Server.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MultiplayPingSample.Server
{
    public abstract class HttpClientBase
    {
        protected const string k_ContentTypeJson = "application/json";
        protected const int k_DefaultTimeoutSeconds = 30;
        readonly Uri m_BaseUri;
        string m_BearerToken;

        public HttpClientBase(string baseUri, string bearerToken)
        {
            m_BearerToken = bearerToken;
            m_BaseUri = new Uri(baseUri);
        }

        protected AsyncResult<TResult> PostJsonAsync<TResult>(object postBody, string path = null, int timeout = k_DefaultTimeoutSeconds) where TResult : class
        {
            return CreateJsonWebRequestAsync<TResult>("POST", postBody, path, timeout);
        }

        protected AsyncResult<TResult> GetJsonAsync<TResult>(string path = null, int timeout = k_DefaultTimeoutSeconds) where TResult : class
        {
            return CreateJsonWebRequestAsync<TResult>("GET", null, path, timeout);
        }

        protected AsyncResult<TResult> CreateJsonWebRequestAsync<TResult>(string httpVerb, object postBody, string path = null, int timeout = k_DefaultTimeoutSeconds) where TResult : class
        {
            path = string.IsNullOrEmpty(path) ? string.Empty : $"/{path}";
            var webRequest = new UnityWebRequest(m_BaseUri + path, httpVerb);
            webRequest.SetRequestHeader("Accept", k_ContentTypeJson);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            if (postBody != null)
            {
                webRequest.SetRequestHeader("Content-Type", k_ContentTypeJson);
                webRequest.timeout = timeout;
                var requestBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postBody));
                webRequest.uploadHandler = new UploadHandlerRaw(requestBody);
            }

            if (m_BearerToken != null)
                webRequest.SetRequestHeader("Authorization", m_BearerToken);

            var asyncResult = new AsyncResult<TResult>(ProcessJsonResponse<TResult>);
            UnityWebRequestAsyncOperation webRequestAsyncOperation = webRequest.SendWebRequest();
            webRequestAsyncOperation.completed += asyncResult.Completed;
            return asyncResult;
        }

        T ProcessJsonResponse<T>(AsyncOperation obj) where T : class
        {
            if (!(obj is UnityWebRequestAsyncOperation httpRequest))
                throw new ArgumentException("Wrong AsyncOperation type in callback.");

            try
            {
                if (httpRequest.webRequest.isNetworkError)
                    throw new Exception(httpRequest.webRequest.error ?? "Undefined Network Error");

                if (httpRequest.webRequest.isHttpError)
                {
                    var body = httpRequest.webRequest.downloadHandler?.text;

                    if (body != null)
                        body = "\n\nResponse Body:\n" + body;

                    throw new Exception(httpRequest.webRequest.error + body);
                }

                // Successful responses w/o bodies are not considered failures
                if (httpRequest.webRequest.downloadHandler?.data?.Length == 0)
                    return default;

                return JsonConvert.DeserializeObject<T>(httpRequest.webRequest.downloadHandler.text);
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception occured while parsing json response: {e}");
                throw;
            }
            finally
            {
                httpRequest.webRequest?.Dispose();
            }
        }
    }
}
