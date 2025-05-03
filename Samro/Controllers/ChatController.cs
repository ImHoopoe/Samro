using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using WinWin.DataLayer.Entities.ChatHub;
using WinWin.DataLayer.Entities.Roles;
using System.Security.Claims;
using WinWin.DataLayer.DTOS.ChatHub;

namespace WinWin.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "https://localhost:5003";
        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<IActionResult> ChatRoom(Guid id)
        {
            var roomId = id;

            using (var client = _httpClientFactory.CreateClient())
            {
                try
                {

                    Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var response = await client.GetAsync($"{_apiBaseUrl}/api/Messages/room/{roomId}?userId={userId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var messagesJson = await response.Content.ReadAsStringAsync();
                        var messages = JsonConvert.DeserializeObject<List<ChatMessageViewModel>>(messagesJson);
                        ViewBag.RoomId = id;
                        ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        return View(messages);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Error fetching messages.";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

       
    }
}

