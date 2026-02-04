using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RAG_Document_UI.Models;

namespace RAG_Document_UI.Controllers
{
    public class AiDocumentsController : Controller
    {
        private readonly HttpClient _http;
        private readonly EndpointSettings _endpointSettings;

        public AiDocumentsController(IHttpClientFactory factory, IOptions<EndpointSettings> endpointSettings)
        {
            _http = factory.CreateClient("AiApi");
            _endpointSettings = endpointSettings.Value;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> AddDocument(string content)
        {
            var form = new MultipartFormDataContent { { new StringContent(content), "Content" } };

            var response = await _http.PostAsync(_endpointSettings.AddDocumentUrl, form);
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Query(string question)
        {
            var form = new MultipartFormDataContent { { new StringContent(question), "Question" } };

            var response = await _http.PostAsync(_endpointSettings.QueryUrl, form);
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
