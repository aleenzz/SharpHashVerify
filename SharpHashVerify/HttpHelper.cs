using SharpHashVerify.Ntlm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpHashVerify
{
    public class HttpHelper
    {
        private static readonly HttpClient httpClient;

        static HttpHelper()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            httpClient = new HttpClient();
        }
        public static HttpResponseMessage doGet(string url, string username, string password)
        {
            var type1 = new NtlmNegotiateMessageGenerator();
            var type1HeaderValue = type1.GenerateAuthorizationHeader();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", type1HeaderValue);
            var getChallengeResponse = httpClient.GetAsync(url).Result;
            var authHeaders = getChallengeResponse.Headers.Where(h => h.Key.IndexOf("WWW-Authenticate", StringComparison.InvariantCultureIgnoreCase) >= 0).FirstOrDefault().Value;
            var type2 = new NtlmChallengeMessageGenerator(authHeaders.FirstOrDefault());
            var type3 = new NtlmAuthenticateMessageGenerator(null, null, username, password, type2);
            var type3HeaderValue = type3.GenerateAuthorizationHeader();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", type3HeaderValue);
            HttpResponseMessage Getresponses = httpClient.GetAsync(url).Result;
            return Getresponses;
        }


    }
}
