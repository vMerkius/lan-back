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
    public class StatsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;


        public StatsController(IUserRepository userRepository,IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetStats()
        {
            int women = _userRepository.GetWomen();
            int men = _userRepository.GetMen();
            int reportstoreply = _reportRepository.GetReportsToReply();
            double averageage = _userRepository.GetAverageAge();
            return Ok(new
            {
                Reports = reportstoreply,
                Men = men,
                Women = women,
                Averageage = averageage
            });

        }




    }
}