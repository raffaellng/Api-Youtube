using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDManipulacao.Application.Commands;
using RDManipulacao.Application.Queries;

namespace RDManipulacao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IMediator _mediator;
        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/videos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVideoCommand command)
        {
            var videoId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = videoId }, videoId);
        }

        // PUT: api/videos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVideoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            var success = await _mediator.Send(command);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/videos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteVideoCommand(id));
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/videos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var video = await _mediator.Send(new GetVideoByIdQuery(id));
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        // GET: api/videos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var videos = await _mediator.Send(new GetAllVideosQuery());
            return Ok(videos);
        }
    }
}
