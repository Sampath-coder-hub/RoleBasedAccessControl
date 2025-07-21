using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost("edit")]
        public IActionResult EditContent([FromBody] ContentUpdateModel model)
        {
            return Ok($"Content with ID {model.Id} updated to title: {model.Title}");
        }

        [Authorize(Roles = "Viewer,Editor,Admin")]
        [HttpGet("view")]
        public IActionResult ViewContent()
        {
            var contents = new List<string> { "Content 1", "Content 2", "Content 3" };
            return Ok(contents);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        public IActionResult AdminOnly() => Ok("Admin management zone.");

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteContent(int id)
        {
            return Ok($"Content with ID {id} has been deleted.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateContent([FromBody] ContentCreateModel model)
        {
            return Ok($"New content created with title: {model.Title} and body: {model.Body}");
        }
    }
}
