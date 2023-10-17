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
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportController(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        [HttpGet("{reportId}")]
        [ProducesResponseType(200, Type = typeof(Report))]
        [ProducesResponseType(400)]
        public IActionResult GetReport(int reportId)
        {
            if (!_reportRepository.ReportExists(reportId))
                return NotFound();

            var report = _mapper.Map<ReportDto>(_reportRepository.GetReport(reportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(report);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Report>))]
        public IActionResult GetReports()
        {
            var reports = _mapper.Map<List<ReportDto>>(_reportRepository.GetReports());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reports);
        }

     
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReport([FromBody] ReportDto reportCreate)
        {
            if (reportCreate == null)
                return BadRequest(ModelState);
            var report = _reportRepository.GetReports()
                .Where(r => r.Topic.Trim().ToUpper() == reportCreate.Topic.Trim().ToUpper())
                .FirstOrDefault();
            if (report != null)
            {
                ModelState.AddModelError("", "Report already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reportMap = _mapper.Map<Report>(reportCreate);

            if (!_reportRepository.CreateReport(reportMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{reportId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReport(int reportId, [FromBody] ReportDto updatedReport)
        {
            if (updatedReport == null)
            {
                return BadRequest(ModelState);
            }
            if (reportId != updatedReport.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reportRepository.ReportExists(reportId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var reportMap = _mapper.Map<Report>(updatedReport);
            if (!_reportRepository.UpdateReport(reportMap))
            {
                ModelState.AddModelError("", "Something went wrong updating report");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{reportId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReport(int reportId)
        {
            if (!_reportRepository.ReportExists(reportId))
            {
                return NotFound();
            }
            var reportToDelete = _reportRepository.GetReport(reportId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reportRepository.DeleteReport(reportToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting report");
            }
            return NoContent();

        }

        [HttpGet("Reply/{reportId}")]
        [ProducesResponseType(200, Type = typeof(Reply))]
        [ProducesResponseType(400)]
        public IActionResult GetReplyToReport(int reportId)
        {
            if (!_reportRepository.ReportExists(reportId))
                return NotFound();

            var reply = _mapper.Map<ReplyDto>(_reportRepository.GetReplyToReport(reportId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reply);
        }

    }

}
