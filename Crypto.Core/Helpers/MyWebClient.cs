using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Common {
    public class MyWebClient : HttpClient {
        public string DownloadString(string address) {
            Task<string> t = this.GetStringAsync(address);
            t.Wait(10000);
            return t.Result;
        }
        public byte[] DownloadData(string adress) {
            Task<byte[]> t = this.GetByteArrayAsync(adress);
            t.Wait(10000);
            return t.Result;
        }
        public Task<string> DownloadStringTaskAsync(string address) {
            return this.GetStringAsync(address);
        }
        public byte[] UploadValues(string address, HttpRequestParamsCollection coll) {
            var content = new FormUrlEncodedContent(coll);
            Task<HttpResponseMessage> t = this.PostAsync(address, content);
            t.Wait(10000);
            Task<byte[]> tt = t.Result.Content.ReadAsByteArrayAsync();
            tt.Wait(10000);
            return tt.Result;
        }
        public async Task<byte[]> UploadValuesTaskAsync(string address, HttpRequestParamsCollection coll) {
            var content = new FormUrlEncodedContent(coll);
            HttpResponseMessage message = await this.PostAsync(address, content);
            return await message.Content.ReadAsByteArrayAsync();
        }
        public HttpRequestHeaders Headers { get { return DefaultRequestHeaders; } }
    }

    public class HttpRequestParamsCollection : List<KeyValuePair<string, string>> {
        public void Add(string key, string value) {
            this.Add(new KeyValuePair<string, string>(key, value));
        }
    }

    //public class MyWebClient : WebClient {
    //    static MyWebClient() {
    //        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
    //         X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //        { return true; };
    //    }
    //    public MyWebClient() : base() {
            
    //    }
    //    protected override WebRequest GetWebRequest(Uri address) {
    //        WebRequest res = base.GetWebRequest(address);
    //        HttpWebRequest hres = res as HttpWebRequest;
    //        if(hres != null) {
    //            hres.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
    //            hres.ContentType = "application/x-www-form-urlencoded";
    //            hres.Accept = "application/xml, text/json, text/x-json, text/javascript, text/xml, application/json";
    //            hres.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
    //            hres.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
    //        }
    //        res.Timeout = 7000;
    //        return res;
    //    }
    //}
}
