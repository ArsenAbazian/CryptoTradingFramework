using CefSharp;
using CefSharp.WinForms;
using Crypto.Core;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient.Helpers {
    public abstract class PortalHelperBase : IDisposable {
        Stopwatch timer;

        public void ClearCache() {
            //SudRfAppealCookieVisitor visitor = new SudRfAppealCookieVisitor();
            //visitor.ClearCookies = true;
            //ChromiumWebBrowser wb = new ChromiumWebBrowser("www.google.ru");
            //Cef.GetGlobalCookieManager().VisitAllCookies(visitor);
        }

        public static bool DisablePortalOperations { get; set; }
        public static bool CheckDisablePortalOperations() {
            if(DisablePortalOperations) {
                XtraMessageBox.Show("Some components needed to work with portal are missed.");
            }
            return DisablePortalOperations;
        }
        public PortalHelperBase() {
            //Cef.Initialize(new CefSettings() { Locale = "ru" });
            this.timer = new Stopwatch();
            State = PortalState.None;
        }

        public bool ClickOnObjectByClass(string className) {
            return ClickOnObjectByClass(className, 0);
        }

        protected bool SetElementText(string className, string text) {
            bool result = true;
            try {
                Task task = Chromium.EvaluateScriptAsync(
                    "(function() { " +
                    "    var links = document.getElementsByClassName('" + className + "'); " +
                    "    if(links.length == 0) return false;" +
                    "    links[0].value = '" + text + "';" +
                    "    return true;" +
                    "})();").ContinueWith(x => {
                        if(x.Result != null && x.Result.Result != null)
                            Console.WriteLine("enter value " + className + " = " + text + " -> " + x.Result.Result.ToString());
                        if(x.Result == null || x.Result.Result == null || (!(bool)x.Result.Result))
                            result = false;
                    });
                task.Wait(10000);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                result = false;
            }
            if(!result) {
                return false;
            }
            return true;
        }

        protected bool ClickOnObjectByClass(string className, int index) {
            bool result = true;
            try {
                Task task = Chromium.EvaluateScriptAsync(
                    "(function() { " +
                    "    var links = document.getElementsByClassName('" + className + "'); " +
                    "    if(links.length == 0) return false;" +
                    "    links[" + index + "].click();" +
                    "    return true;" +
                    "})();").ContinueWith(x => {
                        if(x.Result != null && x.Result.Result != null)
                            Console.WriteLine("clickOnObject " + className + " -> " + x.Result.Result.ToString());
                        if(x.Result == null || x.Result.Result == null || (!(bool)x.Result.Result))
                            result = false;
                    });
                task.Wait(10000);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                result = false;
            }
            if(!result) {
                return false;
            }
            return true;
        }

        public bool SetElementValueById(string id, string value) {
            bool result = true;
            Task task = Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var input = document.getElementById('" + id + "');" +
                "    if(input != null) {" +
                "        input.value = " + value + ";" +
                "        input.defaultValue = " + value + ";" +
                "        var ev = new Event('input', { bubbles: true});" +
                "        ev.simulated = true;" +
                "        input.dispatchEvent(ev);" +
                "        return true;" +
                "    }" +
                "    return false;" +
                "})();").ContinueWith(x => {
                    if(x.Result != null && !((bool)x.Result.Result)) {
                        result = false;
                    }
                });
            task.Wait(5000);
            if(!result) {
                return false;
            }
            return true;
        }

        public string GetElementByClassNameContent(string className) {
            return (string)Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var input = document.getElementsByClassName('" + className + "');" +
                "    if(input != null && input.length > 0) {" +
                "        return input[0].innerText;" +
                "    }" +
                "    return null;" +
                "})();").Result.Result;
        }

        public bool CheckElementByClassNameContent(string className, string content) {
            return (bool)Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var input = document.getElementsByClassName('" + className + "');" +
                "    if(input != null && input.length > 0) {" +
                "        return input[0].innerText == '" + content + "';" +
                "    }" +
                "    return false;" +
                "})();").Result.Result;
        }

        public string GetElementByIdContent(string id) {
            return (string)Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var input = document.getElementById('" + id + "');" +
                "    if(input != null) {" +
                "        return input.innerText;" +
                "    }" +
                "    return null;" +
                "})();").Result.Result;
        }

        public bool FindElementById(string id) {
            return (bool)Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var input = document.getElementById('" + id + "');" +
                "    return input != null;" +
                "})();").Result.Result;
        }

        public bool ClickOnObjectById(string idName) {
            bool result = true;
            Task task = Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var item = document.getElementById('" + idName + "'); " +
                "    if(item == null) return false;" +
                "    item.click();" +
                "    return true;" +
                "})();").ContinueWith(x => {
                    if(x.Result != null && !((bool)x.Result.Result)) {
                        result = false;
                    }
                });
            task.Wait(10000);
            if(!result) {
                return false;
            }
            return true;
        }

        protected bool ClickOnObjectByHref(string className, string href) {
            bool result = true;
            try {
                Task task = Chromium.EvaluateScriptAsync(
                    "(function() { " +
                    "    var links = document.getElementsByClassName('" + className + "'); " +
                    "    for(i=0; i < links.length; i++) {" +
                    "        if(links[i].href == '" + href + "') {" +
                    "            links[i].click();" +
                    "            return true;" +
                    "        }" +
                    "    }" +
                    "    return false;" +
                    "})();").ContinueWith(x => {
                        if(x.Result != null && !((bool)x.Result.Result)) {
                            result = false;
                        }
                    });
                task.Wait(10000);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                result = false;
            }
            if(!result) {
                return false;
            }
            return true;
        }

        protected bool ClickOnHref(string href) {
            bool result = true;
            try {
                Task task = Chromium.EvaluateScriptAsync(
                    "(function() { " +
                    "    var links = document.getElementsByTagName('a'); " +
                    "    for(i=0; i < links.length; i++) {" +
                    "        if(links[i].href == '" + href + "') {" +
                    "            links[i].click();" +
                    "            return true;" +
                    "        }" +
                    "    }" +
                    "    return false;" +
                    "})();").ContinueWith(x => {
                        if(x.Result != null && !((bool)x.Result.Result)) {
                            result = false;
                        }
                    });
                task.Wait(10000);
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);
                result = false;
            }
            if(!result) {
                return false;
            }
            return true;
        }

        protected bool ClickOnObjectByProperty(string className, string property, string value) {
            bool result = true;
            Task task = Chromium.EvaluateScriptAsync(
                "(function() { " +
                "    var links = document.getElementsByClassName('" + className + "'); " +
                "    for(i=0; i < links.length; i++) {" +
                "        if(links[i]." + property + " == '" + value + "') {" +
                "            links[i].click();" +
                "            return true;" +
                "        }" +
                "    }" +
                "    return false;" +
                "})();").ContinueWith(x => {
                    if(x.Result != null && !((bool)x.Result.Result)) {
                        result = false;
                    }
                });
            task.Wait(10000);
            if(!result) {
                return false;
            }
            return true;
        }

        protected bool SelectOpiton(string className, int optionIndex) {
            bool result = true;
            string message = "success";
            for(int i = 0; i < 3; i++) {
                Task task = Chromium.EvaluateScriptAsync(
                    "(function() {" +
                    "    var select = document.getElementsByClassName('" + className + "');" +
                    "    if(select == null || select.length == 0) return false;" +
                    "    select[0].value = select[0].options[" + optionIndex + "].value;" +
                    "    $(select[0]).trigger('change');" +
                    "    return true;" +
                    "})();"
                ).ContinueWith(x => {
                    if(x.Result != null && x.Result.Result != null && !((bool)x.Result.Result)) {
                        message = x.Result.Message;
                        result = false;
                    }
                });
                task.Wait(10000);
                if(task.Status != TaskStatus.RanToCompletion)
                    Application.DoEvents();
                if(result)
                    break;
            }
            if(!result) {
                return false;
            }
            return true;
        }

        protected async Task<T> ExecuteScript<T>(int timeMs, string code, T failValue) {
            RestartTimer();

            string totalCode = "(function() { " +
                    code +
                "})();";

            try {
                Task<JavascriptResponse> task = Chromium.EvaluateScriptAsync(totalCode, TimeSpan.FromMilliseconds(timeMs));
                await task;
                if(task.Result != null && task.Result.Success && task.Result != null) {
                    Console.WriteLine("'" + code + "' execution time = " + timer.ElapsedMilliseconds);
                    return (T)task.Result.Result;
                }
            }
            catch(Exception e) {
                Telemetry.Default.TrackException(e);

                return failValue;
            }
            return failValue;
        }

        protected virtual Image MakeSnapshot() {
            return null;
        }

        protected bool WaitUntilFoundById(int timeMs, string code) {
            RestartTimer();
            bool found = false;
            while(ElapsedMs < timeMs) {
                string totalCode = "(function() { " +
                    "    var item = " + code + ";" +
                    "    return item != null;" +
                    "})();";

                Task task = Chromium.EvaluateScriptAsync(totalCode).ContinueWith(x => {
                    if(x.Result != null && x.Result.Success && x.Result.Result != null)
                        found = (bool)x.Result.Result;
                    else
                        found = false;
                });
                task.Wait(timeMs);
                if(found) break;
                Application.DoEvents();
            }

            return found;
        }

        protected bool WaitUntil(int timeMs, string code) {
            RestartTimer();
            bool found = false;
            while(ElapsedMs < timeMs) {
                string totalCode = "(function() { " +
                    "    var result = " + code + ";" +
                    "    return result;" +
                    "})();";

                Task task = Chromium.EvaluateScriptAsync(totalCode).ContinueWith(x => {
                    if(x.Result != null && x.Result.Success && x.Result.Result != null)
                        found = (bool)x.Result.Result;
                    else
                        found = false;
                });
                task.Wait(timeMs);
                if(found) break;
                Application.DoEvents();
            }

            return found;
        }

        protected bool WaitUntilFoundItems(int timeMs, string code) {
            RestartTimer();
            bool found = false;
            while(ElapsedMs < timeMs) {
                string totalCode = "(function() { " +
                    "    var items = " + code + ";" +
                    "    return items != null && items.length > 0;" +
                    "})();";

                Task task = Chromium.EvaluateScriptAsync(totalCode).ContinueWith(x => {
                    if(x.Result != null && x.Result.Success && x.Result.Result != null)
                        found = (bool)x.Result.Result;
                    else
                        found = false;
                });
                task.Wait(timeMs);
                if(found) break;
                Application.DoEvents();
            }

            return found;
        }
        protected bool WaitUntilFoundByProperty(int timeMs, string code, string property, string value) {
            RestartTimer();
            bool found = false;
            while(ElapsedMs < timeMs) {
                string totalCode = "(function() { " +
                    "    var item = " + code + ";" +
                    "    return item != null && item." + property + " == " + value + ";" +
                    "})();";

                Task task = Chromium.EvaluateScriptAsync(totalCode).ContinueWith(x => {
                    if(x.Result != null && x.Result.Success && x.Result.Result != null)
                        found = (bool)x.Result.Result;
                    else
                        found = false;
                });
                task.Wait(timeMs);
                if(found) break;
                Application.DoEvents();
            }

            return found;
        }
        protected bool WaitUntilTrue(int timeMs, string code) {
            RestartTimer();
            bool found = false;
            while(ElapsedMs < timeMs) {
                string totalCode = "(function() { " +
                    "    return (" + code + ");" +
                    "})();";

                Task task = Chromium.EvaluateScriptAsync(totalCode).ContinueWith(x => {
                    if(x.Result != null && x.Result.Success && x.Result.Result != null)
                        found = (bool)x.Result.Result;
                    else
                        found = false;
                });
                task.Wait(timeMs);
                if(found) break;
                Application.DoEvents();
            }

            return found;
        }

        public string StartAddress { get; set; }

        ChromiumWebBrowser chromium;
        public ChromiumWebBrowser Chromium {
            get {
                if(chromium == null) {
                    try {
                        chromium = new ChromiumWebBrowser(StartAddress);
                        //chromium.RequestHandler = new RuRequestHandler();
                        chromium.Dock = DockStyle.Fill;
                        chromium.LoadingStateChanged += OnLoadingStateChanged;
                        chromium.AddressChanged += OnAddressChanged;
                        chromium.IsBrowserInitializedChanged += OnBrowserInitializedChanged;
                    }
                    catch(Exception e) {
                        Telemetry.Default.TrackException(e);
                        throw e;
                    }
                }
                return chromium;
            }
        }

        protected string CurrentAddress { get; set; }

        private void OnAddressChanged(object sender, AddressChangedEventArgs e) {
            CurrentAddress = e.Address;
        }

        protected abstract string LoginPageAdress { get; }
        protected virtual void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e) {
            Debug.WriteLine(e.IsLoading);
            if(e.IsLoading)
                return;

            if(State == PortalState.LoginPage) {
                OnLoginPageLoaded(new WebBrowserDocumentCompletedEventArgs(new Uri(LoginPageAdress)));
            }
            if(State == PortalState.Autorization) {
                OnAutorizationLoaded(new WebBrowserDocumentCompletedEventArgs(new Uri(LoginPageAdress)));
            }
            if(State == PortalState.LoadPage) {
                State = PortalState.PageLoaded;
            }
        }

        protected virtual void OnAutorizationLoaded(WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs) {

        }

        protected virtual void OnLoginPageLoaded(WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs) {

        }

        public bool IsBrowserInitialized { get; set; } = true;
        private void OnBrowserInitializedChanged(object sender, EventArgs e) {
            IsBrowserInitialized = true;// e.IsBrowserInitialized;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public long ElapsedMs { get { return this.timer.ElapsedMilliseconds; } }
        public void StopTimer() { this.timer.Stop(); }
        public void RestartTimer() {
            this.timer.Stop();
            this.timer.Reset();
            this.timer.Start();
        }

        //WebBrowser browser;
        //public WebBrowser Browser {
        //    get {
        //        if(browser == null) {
        //            browser = new WebBrowser();
        //            browser.Dock = DockStyle.Fill;
        //            browser.AllowNavigation = true;
        //            browser.DocumentCompleted += OnDocumentCompleted;
        //            browser.Navigating += OnBrowserNavigating;
        //        }
        //        return browser;
        //    }
        //}

        //protected HtmlDocument Document { get { return Browser.Document; } }

        protected XtraForm Form { get; set; }
        protected virtual void ShowForm() {
            ShowForm(Chromium);
        }
        protected virtual void ShowForm(Control browser) {
            if(browser == null)
                throw new Exception("Browser == null");
            browser.Dock = DockStyle.Fill;
            Form = new XtraForm();
            Form.StartPosition = FormStartPosition.CenterScreen;
            Form.Size = new Size((int)(DpiProvider.Default.DpiScaleFactor * 1000), (int)(DpiProvider.Default.DpiScaleFactor * 800));
            Form.Controls.Add(browser);
            Form.Shown += Form_Shown;
            Form.TopMost = true;

            BarManager manager = new BarManager();
            manager.Form = Form;

            Bar bar = new Bar(manager);
            bar.OptionsBar.UseWholeRow = true;
            bar.OptionsBar.DrawBorder = false;
            bar.DockStyle = BarDockStyle.Top;

            manager.ForceInitialize();

            RepositoryItemMemoExEdit edit = new RepositoryItemMemoExEdit();
            edit.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search));
            edit.Tag = manager;
            edit.ButtonClick += OnExecuteScriptButtonClick;

            manager.RepositoryItems.Add(edit);

            BarEditItem item = new BarEditItem(manager);
            item.EditWidth = 200;
            item.Edit = edit;

            bar.ItemLinks.Add(item);

            Form.Show();
        }

        private async void OnExecuteScriptButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if(e.Button.Kind != DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
                return;
            MemoExEdit edit = (MemoExEdit)sender;
            bool result = await ExecuteScript<bool>(3000, edit.Text, false);
            if(!result)
                XtraMessageBox.Show("script execution timeout");
        }

        protected virtual void Form_Shown(object sender, EventArgs e) {
        }

        public void Dispose() {
            if(this.chromium != null) {
                chromium.LoadingStateChanged -= OnLoadingStateChanged;
                chromium.AddressChanged -= OnAddressChanged;
                chromium.IsBrowserInitializedChanged -= OnBrowserInitializedChanged;
            }
            if(Form != null)
                Form.Dispose();
        }

        protected virtual void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
        }
        protected virtual void OnBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e) {
        }

        public PortalState State { get; set; }

        protected virtual void OnEsiaDoneDocumentLoaded() {
            //State = PortalState.LkDone;
        }

        protected virtual void AddError(string error) {

        }

        public bool WaitUntil(int timeout, IfDelegate ifDelegate) {
            RestartTimer();
            bool startFormVisible = Form != null && Form.Visible;
            while(ElapsedMs < timeout) {
                if(ifDelegate())
                    return true;
                bool actualVisible = Form != null && Form.Visible;
                if(startFormVisible != actualVisible) {
                    State = PortalState.ClosedUnexpectedly;
                    return false;
                }
                Thread.Sleep(50);
                Application.DoEvents();
            }
            AddError("Operation timeout.");
            return false;
        }

        protected void PrintHtmlElement(string tabs, HtmlElement elem) {
            Debug.WriteLine(tabs + "<" + elem.TagName + " class=" + elem.GetAttribute("className") + ">");
            foreach(HtmlElement child in elem.Children) {
                PrintHtmlElement(tabs + "\t", child);
            }
            Debug.WriteLine(tabs + "</" + elem.TagName + ">");
        }
        protected void PrintDocument(HtmlDocument doc) {
            foreach(HtmlElement elem in doc.Body.Children) {
                PrintHtmlElement("", elem);
            }
        }
        
        protected async Task PopulateInputFile(HtmlElement file, string fileName) {
            Form.Activate();
            file.Focus();
            var sendKeyTask = Task.Delay(500).ContinueWith((_) => {
                SendKeys.Send(fileName + "{ENTER}");
            }, TaskScheduler.FromCurrentSynchronizationContext());
            SendKeys.Send(" ");
            file.InvokeMember("Click");
            await sendKeyTask;
            await Task.Delay(500);
        }

        protected HtmlElement FindElementInFormContaining(HtmlElement container, string tag, string text) {
            foreach(HtmlElement elem in container.Children) {
                if(elem.TagName == tag && elem.InnerText.Contains(text))
                    return elem;
            }
            return null;
        }
        protected HtmlElement FindElementInFormContaining(HtmlElement container, string tag, string text, bool recursive) {
            foreach(HtmlElement elem in container.Children) {
                if(elem.TagName == tag && elem.InnerText.Contains(text))
                    return elem;
                if(recursive) {
                    HtmlElement res = FindElementInFormContaining(elem, tag, text, recursive);
                    if(res != null)
                        return res;
                }
            }
            return null;
        }
        protected HtmlElement FindElementByValue(HtmlElement container, string tag, string type, string value) {
            foreach(HtmlElement child in container.Children) {
                if(child.TagName == tag && child.GetAttribute("type") == type && child.GetAttribute("value") == value)
                    return child;
                HtmlElement res = FindElementByValue(child, tag, type, value);
                if(res != null)
                    return res;
            }
            return null;
        }
        public void Wait(int ms) {
            long start = ElapsedMs;
            if(!this.timer.IsRunning)
                this.timer.Start();
            while(ElapsedMs - start < ms) {
                Application.DoEvents();
            }
            return;
        }
        CultureInfo russianCulture;
        protected CultureInfo RussianCulture {
            get {
                if(russianCulture == null)
                    russianCulture = new CultureInfo("ru-Ru");
                return russianCulture;
            }
        }

        protected bool Contains(string sub, string str) {
            if(string.IsNullOrEmpty(sub)) return true;
            return str.Contains(sub);
        }
        public abstract void Enter(string email, string password);
    }

    public enum PortalState {
        None, LoginPage, LoginPageLoaded, Autorization, AutorizationDone, LoadPage, PageLoaded,
        ClosedUnexpectedly
    }

    //internal class RuRequestHandler : IRequestHandler {
    //    bool IRequestHandler.CanGetCookies(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request) {
    //        return true;
    //    }

    //    bool IRequestHandler.CanSetCookie(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, CefSharp.Cookie cookie) {
    //        return true;
    //    }

    //    bool IRequestHandler.GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback) {
    //        return true;
    //    }

    //    IResponseFilter IRequestHandler.GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response) {
    //        if(SudRfResponceFilter.Intercept)
    //            return new SudRfResponceFilter();
    //        return null;
    //    }

    //    bool IRequestHandler.OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect) {
    //        return false;
    //    }

    //    CefReturnValue IRequestHandler.OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback) {
    //        var headers = request.Headers;
    //        headers.Add("Accept-Language", "en-US,en;q=0.9,ru;q=0.8");
    //        headers.Add("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
    //        request.Headers = new System.Collections.Specialized.NameValueCollection();
    //        request.Headers = headers;
    //        return CefReturnValue.Continue;
    //    }

    //    bool IRequestHandler.OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback) {
    //        return false;
    //    }

    //    bool IRequestHandler.OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture) {
    //        return false;
    //    }

    //    void IRequestHandler.OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath) {

    //    }

    //    bool IRequestHandler.OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url) {
    //        return false;
    //    }

    //    bool IRequestHandler.OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback) {
    //        return false;
    //    }

    //    void IRequestHandler.OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status) {

    //    }

    //    void IRequestHandler.OnRenderViewReady(IWebBrowser browserControl, IBrowser browser) {

    //    }

    //    void IRequestHandler.OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength) {

    //    }

    //    void IRequestHandler.OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl) {

    //    }

    //    bool IRequestHandler.OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response) {
    //        return false;
    //    }

    //    bool IRequestHandler.OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback) {
    //        return false;
    //    }
    //}

    public delegate bool IfDelegate();

    public class WalletInvestorPortalHelper : PortalHelperBase {
        public WalletInvestorPortalHelper() {
            StartAddress = LoginPageAdress;
        }
        protected override string LoginPageAdress => "https://walletinvestor.com/user/login";

        public override void Enter(string email, string password) {
            Login = email;
            Password = password;
            StartAddress = LoginPageAdress;

            State = PortalState.LoginPage;
            ClearCache();
            
            ShowForm();
            RestartTimer();
        }

        async void OnAutorizationDocumentLoadedCore() {
            StopTimer();

            Task<JavascriptResponse> task = Chromium.EvaluateScriptAsync(
                "var me = document.getElementById('loginform-email'); me.focus();" +
                "me.value ='" + Login + "'; me.blur();");
            await task;
            if(!task.Result.Success) {
                return;
            }

            task = Chromium.EvaluateScriptAsync(
                "var pass = document.getElementById('loginform-password'); pass.focus();" +
                "pass.value = '" + Password + "'; pass.blur();");
            await task;
            if(!task.Result.Success) {
                return;
            }

            State = PortalState.Autorization;
            task = Chromium.EvaluateScriptAsync(
                "var button = document.getElementsByClassName('btn btn-primary'); " +
                "button[0].click();");
            await task;
            if(!task.Result.Success) {
                return;
            }
        }

        protected override void OnLoginPageLoaded(WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs) {
            OnAutorizationDocumentLoadedCore();

            //Debug.WriteLine("set login");
            //Application.DoEvents();
            //if(!SetElementValueById("loginform-email", Login)) {
            //    Debug.WriteLine("error entering login");
            //    return;
            //}
            //Application.DoEvents();
            //Debug.WriteLine("set password");
            //if(!SetElementValueById("loginform-password", Password)) {
            //    Debug.WriteLine("error entering password");
            //    return;
            //}
            //Application.DoEvents();
            //State = PortalState.Autorization;
            //Debug.WriteLine("click");
            //if(!ClickOnObjectByClass("btn btn-primary", 0)) {
            //    Debug.WriteLine("error clicking enter");
            //    return;
            //}
        }

        protected override void OnAutorizationLoaded(WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs) {
            base.OnAutorizationLoaded(webBrowserDocumentCompletedEventArgs);
            State = PortalState.AutorizationDone;
        }

        public void CloseForm() {
            Form.Close();
        }
    }
}
