using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lesson1Homework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeWorkController(HttpClient httpClient, IConfiguration configuration) : ControllerBase
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly string? urlJson = configuration["ApiSettings:UrlJson"] ?? throw new ArgumentNullException("BaseUrl is not configured.");

        [HttpGet("Posts")]
        public async Task<IActionResult> GetPostAsync([FromQuery] int? userId, [FromQuery] string title)
        {
            if (userId == null || string.IsNullOrEmpty(title))
                return BadRequest("userId and title are required.");

            var url = $"{urlJson}/posts?userId={userId}&title={Uri.EscapeDataString(title)}";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return StatusCode((int)response.StatusCode, "Failed to retrieve posts.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Post/{id}")]
        public async Task<IActionResult> GetPostByIdAsync(int id)
        {
            var url = $"{urlJson}/posts/{id}";
            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return StatusCode((int)response.StatusCode, "Failed to retrieve post.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Post/{id}")]
        public async Task<IActionResult> DeletePostById(int id)
        {
            var url = $"{urlJson}/posts/{id}";
            try
            {
                var response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { Message = $"Post {id} deleted successfully." });
                }

                return StatusCode((int)response.StatusCode, "Failed to delete post.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("UsersData")]
        public IActionResult PostUserData([FromBody] UsersDataResponse userData)
        {
            if (userData == null || userData.Data == null || userData.Data.Count == 0)
            {
                return BadRequest("Invalid or empty data.");
            }

            return Ok(userData);
        }

        [HttpPost("UserData")]
        public IActionResult PostUserData([FromBody] UserDataResponse userData)
        {
            if (userData == null || userData.Data == null)
            {
                return BadRequest("Invalid or empty data.");
            }

            return Ok(userData);
        }
    }
}
