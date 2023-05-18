using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Markdig;
using Microsoft.JSInterop;
using WEBAPP.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using CHAT.Models;

namespace CHAT.Pages
{
    public partial class Index
    {
        [Inject]
        public IJSRuntime JSTrigger { get; set; }
        public string Response { get; set; }
        public string Message = String.Empty;
        public string Question = String.Empty;
        string message;
        string apikey;
        public static string APIKEY = String.Empty;
        public static List<RequestMessage> ListOfMessages = new List<RequestMessage>();
        [Inject]
        RequestRoot requestToSerialize { get; set; }
        protected async override Task OnInitializedAsync()
        {
        }
        private MarkupString RenderMarkdown(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var html = Markdig.Markdown.ToHtml(markdown, pipeline);
            return new MarkupString(html);
        }
        private async Task NewChat()
        {
            Message = "";
            Question = "";
            Response = "";
            message = "";
            StateHasChanged();
        }
        private async Task Resend()
        {
            message = Question;
            Question = "";
            Response = "";
            Message = "";
            StateHasChanged();
            await SendRequest();
        }
        #region SendRequestToAPI
        private async Task SendRequest()
        {
            Message = message;
            Question = Message;
            Response = "";
            Message = "";
            message = "";
            StateHasChanged();

            ListOfMessages.Add(new RequestMessage() { role = "user", content = Question });
            HttpClient _httpClient = new();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKEY);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));

            var RequestLog = new WEBAPP.Entities.FinalClass();
            RequestLog.question = Message;
            requestToSerialize.messages = ListOfMessages;
            string requestRoot = JsonConvert.SerializeObject(requestToSerialize);
            var content = new StringContent(requestRoot, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress)
            {
                Content = content
            };
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var test = await response.Content.ReadAsStringAsync();
            RootStatic myDeserializedClass = JsonConvert.DeserializeObject<RootStatic>(test);
            Response = myDeserializedClass.choices[0].message.content;
            Thread.Sleep(500);
        }
        private async Task AddApiKey()
        {
            APIKEY = apikey;
        }
        private async Task Invalid()
        {
        }
        #endregion
        #region TechnicalMethodsForBetterUsage
        public async Task Enter(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !e.ShiftKey)
            {
                await SendRequest();
            }
        }
        public async Task Input(ChangeEventArgs e)
        {
            message = e.Value.ToString();

        }
        #endregion
    }
}
