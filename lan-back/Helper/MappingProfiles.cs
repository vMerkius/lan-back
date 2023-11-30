using AutoMapper;
using lan_back.Dto;
using lan_back.Models;

namespace lan_back.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();
            CreateMap<Flashcard, FlashcardDto>();
            CreateMap<FlashcardDto, Flashcard>();
            CreateMap<Word, WordDto>();
            CreateMap<WordDto, Word>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<SubjectDto, Subject>();
            CreateMap<Quiz, QuizDto>();
            CreateMap<QuizDto, Quiz>();
            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Report, ReportDto>();
            CreateMap<ReportDto, Report>();
            CreateMap<Reply, ReplyDto>();
            CreateMap<ReplyDto, Reply>();
            CreateMap<Sentence, SentenceDto>();
            CreateMap<SentenceDto, Sentence>(); 


        }
    }
}
