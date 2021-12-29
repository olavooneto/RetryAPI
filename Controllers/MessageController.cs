using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            this._messageService = messageService;
        }

        [HttpGet("hello")]
        public async Task<IActionResult> Hello() => Ok(await this._messageService.GetHelloMessage());

        [HttpGet("goodbye")]
        public async Task<IActionResult> GoodBye() => Ok(await this._messageService.GetGoodbyMessage());

        [HttpGet("write")]
        public NoContentResult Write([FromQuery]string message)
        {
            System.Console.WriteLine(message);
            this._messageService.WriteMessage(message);

            return NoContent();
        }

        [HttpGet("read")]
        public async Task<IActionResult> Read() => Ok(await this._messageService.Read());
    }
}