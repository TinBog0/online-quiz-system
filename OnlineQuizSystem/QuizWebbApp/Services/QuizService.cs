using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizWebbApp.DTOs;
using Microsoft.Extensions.Options;
using QuizWebbApp.Config;

namespace QuizWebbApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public QuizService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            var response = await _httpClient.GetAsync("api/Quizs");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<QuizDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Quizs/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<QuizDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<bool> SubmitAnswerAsync(int quizId, int questionIndex, string userAnswer)
        {
            var payload = new
            {
                QuizId = quizId,
                QuestionIndex = questionIndex,
                Answer = userAnswer
            };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Quizs/SubmitAnswer", content);
            return response.IsSuccessStatusCode;
        }
    }
}
