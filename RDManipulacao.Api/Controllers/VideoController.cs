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

        [HttpPost("fetch-and-insert")]
        public async Task<IActionResult> FetchAndInsertVideos()
        {
            try
            {
                await _mediator.Send(new FetchAndInsertVideosCommand());
                return Ok("Vídeos inseridos com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir vídeos: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredVideos([FromQuery] string? titulo,
                                                  [FromQuery] string? duracao,
                                                  [FromQuery] string? autor,
                                                  [FromQuery] DateTime? dataPublicacao,
                                                  [FromQuery] string? q)
        {
            var videos = await _mediator.Send(new GetFilteredVideosQuery(titulo, duracao, autor, dataPublicacao, q));

            if (!videos.Any())
                return NotFound("Nenhum vídeo encontrado com os critérios fornecidos.");

            return Ok(videos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVideoCommand command)
        {
            var videoId = await _mediator.Send(command);

            var video = await _mediator.Send(new GetVideoByIdQuery(videoId));

            if (video == null)
                return NotFound();

            return CreatedAtAction(nameof(Create), new { id = videoId }, video);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVideoCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id incompatível");
            

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteVideoCommand(id));
            if (!success)
                return NotFound();
            
            return NoContent();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var video = await _mediator.Send(new GetVideoByIdQuery(id));
        //    if (video == null)
        //        return NotFound();

        //    return Ok(video);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var videos = await _mediator.Send(new GetAllVideosQuery());
        //    return Ok(videos);
        //}
    }
}
