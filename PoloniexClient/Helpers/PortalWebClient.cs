using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Helpers {
    public class PortalClient : WebClient {
        public PortalClient(PortalHelperBase helper) {
            Chromium = helper.Chromium;
        }
        public PortalClient(ChromiumWebBrowser browser) {
            Chromium = browser;
        }
        public PortalClient() { }
        public string FileName { get; set; }
        public WebBrowser Browser { get; private set; }
        public ChromiumWebBrowser Chromium { get; private set; }
        
        protected override WebRequest GetWebRequest(Uri address) {
            WebRequest r = base.GetWebRequest(address);
            HttpWebRequest hr = r as HttpWebRequest;
            if(hr != null) {
                hr.CookieContainer = new CookieContainer();
                if(Expect100Continue.HasValue) {
                    hr.ServicePoint.Expect100Continue = Expect100Continue.Value;
                }
                if(Browser != null)
                    hr.CookieContainer.SetCookies(address, Browser.Document.Cookie.Replace(';', ',') + ",claims_list=%7B%22limit%22%3A25%2C%22sort%22%3Anull%2C%22direction%22%3Anull%7D,device_view=full");
                else if(Chromium != null)
                    SetCookies(hr);
            }
            return r;
        }
        void SetCookies(HttpWebRequest httpWebRequest) {
            CookieVisitor visitor = new CookieVisitor(httpWebRequest);
            CefSharp.Cef.GetGlobalCookieManager().VisitAllCookies(visitor);
            while(!visitor.IsFinished)
                Application.DoEvents();
        }
        public bool Cancel { get; set; }
        public bool? Expect100Continue { get; set; }
        public bool AllowZip { get; internal set; } = true;
    }

    public class CookieVisitor : ICookieVisitor {
        public CookieVisitor(HttpWebRequest hr) {
            Request = hr;
        }

        protected HttpWebRequest Request { get; set; }
        public bool IsFinished;

        void IDisposable.Dispose() {
            
        }

        bool ICookieVisitor.Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie) {
            try {
                deleteCookie = false;
                if(cookie.Domain == null || cookie.Name == null)
                    return true;
                Debug.WriteLine(cookie.Name + ", " + cookie.Value + ", " + cookie.Domain + ", " + cookie.Path);
                Request.CookieContainer.Add(new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
                IsFinished = count == total - 1;
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
            }
            return true;
        }
    }
}
