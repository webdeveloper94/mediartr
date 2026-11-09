using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using MediaSavodxonlik;

internal static class ApiTests
{
    private sealed class Handler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> send) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token) => send(request, token);
    }
    public static async Task Run()
    {
        static void Check(bool condition, string message) { if (!condition) throw new Exception(message); }
        using var http = new HttpClient(new Handler(async (request, token) =>
        {
            Check(request.RequestUri!.AbsoluteUri == "https://api.openai.com/v1/responses", "Wrong API endpoint");
            Check(request.Headers.Authorization?.Scheme == "Bearer", "Missing bearer auth");
            using var json = JsonDocument.Parse(await request.Content!.ReadAsStringAsync(token));
            var root = json.RootElement;
            Check(root.GetProperty("model").GetString() == "gpt-6-luna", "Wrong model");
            Check(root.GetProperty("reasoning").GetProperty("effort").GetString() == "none", "Wrong reasoning effort");
            Check(!root.GetProperty("store").GetBoolean(), "Response storage enabled");
            string input = root.GetProperty("input").GetRawText();
            Check(input.Contains("3-mavzu") && input.Contains("sample context"), "Missing topic context");
            Check(root.GetProperty("input").GetArrayLength() == 4, "Missing history");
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"status":"completed","output":[{"type":"reasoning"},{"type":"message","content":[{"type":"output_text","text":"Birinchi javob."},{"type":"output_text","text":"Ikkinchi qism."}]}]}""", Encoding.UTF8, "application/json")
            };
        }));
        string result = await new MediChatClient(http).AskAsync("mock-key", 3, "sample context", [new("user", "old question"), new("assistant", "old answer")], "new question", CancellationToken.None);
        Check(result.Contains("Birinchi javob.") && result.Contains("Ikkinchi qism."), "Response not parsed");
        foreach (var status in new[] { HttpStatusCode.Unauthorized, HttpStatusCode.TooManyRequests, HttpStatusCode.InternalServerError })
        {
            using var failing = new HttpClient(new Handler((_, _) => Task.FromResult(new HttpResponseMessage(status))));
            bool caught = false;
            try { await new MediChatClient(failing).AskAsync("mock-key", 1, "", [], "question", CancellationToken.None); }
            catch (InvalidOperationException e) { caught = !e.Message.Contains("mock-key"); }
            Check(caught, "API failure not handled safely");
        }
        using var cancelHttp = new HttpClient(new Handler(async (_, token) => { await Task.Delay(10000, token); return new HttpResponseMessage(HttpStatusCode.OK); }));
        using var cancellation = new CancellationTokenSource(TimeSpan.FromMilliseconds(30));
        bool cancelled = false;
        try { await new MediChatClient(cancelHttp).AskAsync("mock-key", 1, "", [], "question", cancellation.Token); }
        catch (OperationCanceledException) { cancelled = true; }
        Check(cancelled, "Cancellation not propagated");
        Console.WriteLine("PASS: mocked OpenAI request model/auth/context/history, output parsing, 401/429/500 errors and cancellation. No live API call.");
    }
}
