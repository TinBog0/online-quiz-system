using System.Text.Json;
using QuizWebbApp.DTOs;

namespace QuizWebbApp.Services
{
    public class QuizService
    {
        private readonly HttpClient _httpClient;

        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<QuizDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

}
