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
    public class LearningController : Controller
    {
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;


        public LearningController(IFlashcardRepository flashcardRepository, IQuizRepository quizRepository,  IMapper mapper, IQuestionRepository questionRepository)
        {
            _flashcardRepository = flashcardRepository;
            _quizRepository = quizRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        [HttpGet("{moduleId}")]
        [ProducesResponseType(200)]
        public IActionResult GetSet(int moduleId)
        {
            var flashcards = _flashcardRepository.GetRandomFlashcards(moduleId, 10);
            var quizQuestions = _questionRepository.GetRandomQuestions(moduleId, 10);

            if (flashcards == null || quizQuestions == null)
            {
                return NotFound();
            }

            var flashcardDtos = _mapper.Map<IEnumerable<FlashcardDto>>(flashcards);
            var quizQuestionDtos = _mapper.Map<IEnumerable<QuestionDto>>(quizQuestions);

            return Ok(new { Flashcards = flashcardDtos, QuizQuestions = quizQuestionDtos });

        }




    }
}