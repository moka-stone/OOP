using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RecordManager.Application.DTOs;

namespace RecordManager.Infrastructure.External
{
    public class QuoteApiAdapter
    {
        private readonly HttpClient _httpClient;
        private const string QuoteApiUrl = "https://api.quotable.io/random";

        public QuoteApiAdapter()
        {
            _httpClient = new HttpClient();
        }

        public async Task<QuoteDTO> GetRandomQuoteAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(QuoteApiUrl);
                var quoteData = JsonSerializer.Deserialize<QuoteApiResponse>(response);
                
                return new QuoteDTO
                {
                    Content = quoteData.Content,
                    Author = quoteData.Author
                };
            }
            catch (Exception)
            {
                return new QuoteDTO
                {
                    Content = "Success is not final, failure is not fatal: it is the courage to continue that counts.",
                    Author = "Winston Churchill"
                };
            }
        }

        private class QuoteApiResponse
        {
            public string Content { get; set; }
            public string Author { get; set; }
        }
    }
} 