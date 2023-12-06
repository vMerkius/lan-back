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
        private readonly ISentenceRepository _sentenceRepository;
        private readonly IMapper _mapper;


        public LearningController(IFlashcardRepository flashcardRepository, IQuizRepository quizRepository,  IMapper mapper, IQuestionRepository questionRepository, ISentenceRepository sentenceRepository)
        {
            _flashcardRepository = flashcardRepository;
            _quizRepository = quizRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _sentenceRepository = sentenceRepository;
        }

          [HttpGet("{moduleId}")]
          [ProducesResponseType(200)]
          public IActionResult GetSet(int moduleId)
          {
              var words = _flashcardRepository.GetRandomFlashcardsWords(moduleId, 7);
              var quizQuestions = _questionRepository.GetRandomQuestions(moduleId, 7);
              var sentences = _sentenceRepository.GetRandomSentences(moduleId, 7);


              if (words == null || quizQuestions == null || sentences==null)
              {
                  return NotFound();
              }

              var wordDtos = _mapper.Map<IEnumerable<WordDto>>(words);
              var quizQuestionDtos = _mapper.Map<IEnumerable<QuestionDto>>(quizQuestions);
              var sentenceDtos = _mapper.Map<IEnumerable<SentenceDto>>(sentences);

              return Ok(new { Flashcards = wordDtos, QuizQuestions = quizQuestionDtos, Sentences = sentenceDtos });

          }





    }
}