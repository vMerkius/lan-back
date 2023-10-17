using AutoMapper;
using lan_back.Dto;
using lan_back.Interfaces;
using lan_back.Models;
using lan_back.Repository;
using Microsoft.AspNetCore.Mvc;

namespace lan_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : Controller
    {
        private readonly IReplyRepository _replyRepository;
        private readonly IMapper _mapper;

        public ReplyController(IReplyRepository replyRepository, IMapper mapper)
        {
            _replyRepository = replyRepository;
            _mapper = mapper;
        }
        [HttpGet("{replyId}")]
        [ProducesResponseType(200, Type = typeof(Reply))]
        [ProducesResponseType(400)]
        public IActionResult GetReply(int replyId)
        {
            if (!_replyRepository.ReplyExists(replyId))
                return NotFound();

            var reply = _mapper.Map<ReplyDto>(_replyRepository.GetReply(replyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reply);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reply>))]
        public IActionResult GetReplies()
        {
            var replies = _mapper.Map<List<ReplyDto>>(_replyRepository.GetReplies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(replies);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReply([FromBody] ReplyDto replyCreate)
        {
            if (replyCreate == null)
                return BadRequest(ModelState);
            var reply = _replyRepository.GetReplies()
                .Where(r => r.Message.Trim().ToUpper() == replyCreate.Message.Trim().ToUpper())
                .FirstOrDefault();
            if (reply != null)
            {
                ModelState.AddModelError("", "Reply already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var replyMap = _mapper.Map<Reply>(replyCreate);

            if (!_replyRepository.CreateReply(replyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{replyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReply(int replyId, [FromBody] ReplyDto updatedReply)
        {
            if (updatedReply == null)
            {
                return BadRequest(ModelState);
            }
            if (replyId != updatedReply.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_replyRepository.ReplyExists(replyId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var replyMap = _mapper.Map<Reply>(updatedReply);
            if (!_replyRepository.UpdateReply(replyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating reply");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{replyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReply(int replyId)
        {
            if (!_replyRepository.ReplyExists(replyId))
            {
                return NotFound();
            }
            var replyToDelete = _replyRepository.GetReply(replyId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_replyRepository.DeleteReply(replyToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reply");
            }
            return NoContent();

        }
    }
}
