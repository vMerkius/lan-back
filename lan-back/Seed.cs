
using lan_back.Data;
using lan_back.Models;
using System.Diagnostics.Metrics;

namespace lan_back
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            /* if (!dataContext.Users.Any())
             {
                 var users = new List<User>()
                 {
                     new User{Name = "Mat",
                             Email = "elo@wp.pl",
                             Password="es" }


                 };
                 dataContext.Users.AddRange(users);
                 dataContext.SaveChanges();
             }*/

            if (!dataContext.UserCourses.Any())
            {
                var userCourses = new List<UserCourse>()
                {
                    new UserCourse()
                    {
                        User = new User()
                        {
                            Name = "essa",
                            Email = "a@wp.pk",
                            Password = "123",
                            Age = 10,
                            Country = "Poland"
                        },
                        Course = new Course()
                        {
                            Language = "English",
                            Level = "B1",
                            Modules = new List<Module>()
                            {
                                new Module {
                                    Name="module1",
                                    Description = "es",
                                    Lessons = new List<Lesson>()
                                    {
                                        new Lesson {
                                            Name="Lesson1",
                                            Description = "desc",
                                            Subjects = new List<Subject>()
                                            {
                                                new Subject
                                                {
                                                    Name = "name_subject",
                                                    Desription = "desc2"
                                                }
                                            }
                                        }
                                    },
                                    Flashcards = new List<Flashcard>()
                                    {
                                        new Flashcard
                                        {
                                            Name="Flashcard1",
                                            Words= new List<Word>()
                                            {
                                                new Word
                                                {
                                                    OriginalWord="cze",
                                                    TranslatedWord="hi"
                                                },
                                                new Word
                                                {
                                                    OriginalWord="ta",
                                                    TranslatedWord="yes"
                                                }
                                            }
                                        }
                                    }

                                },
                                new Module {
                                    Name="module2",
                                    Description = "es2",
                                    Lessons = new List<Lesson>()
                                    {
                                        new Lesson {
                                            Name="lesson2",
                                            Description = "desc2",
                                            Subjects = new List<Subject>()
                                            {
                                                new Subject
                                                {
                                                    Name = "name_subject2",
                                                    Desription = "desc22"
                                                }
                                            }
                                        }
                                    },
                                     Flashcards = new List<Flashcard>()
                                    {
                                        new Flashcard
                                        {
                                            Name="flahscard2",
                                            Words= new List<Word>()
                                            {
                                                new Word
                                                {
                                                    OriginalWord="cze2",
                                                    TranslatedWord="hi2"
                                                },
                                                new Word
                                                {
                                                    OriginalWord="ta2",
                                                    TranslatedWord="yes2"
                                                }
                                            }
                                        }
                                    }


                                }
                            },
                            Quizzes = new List<Quiz>()
                            {
                                new Quiz {
                                    Name="quiz1",
                                    Description="desc11",
                                    Questions = new List<Question>()
                                    {
                                        new Question {
                                            Description = "desc",
                                            CorrectAnswer = "de",
                                            Answers = new List<Answer>()
                                            {
                                                new Answer
                                                {
                                                    Name = "name_subject",
                                                }
                                            }
                                        }
                                    }

                                },
                                new Quiz {
                                    Name="quiz2",
                                    Description="desc2",
                                    Questions = new List<Question>()
                                    {
                                        new Question {
                                            Description = "desc2",
                                            CorrectAnswer = "fe",

                                            Answers = new List<Answer>()
                                            {
                                                new Answer
                                                {
                                                    Name = "name_subject2",
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }
                    },
                };


                dataContext.UserCourses.AddRange(userCourses);
                dataContext.SaveChanges();
                
            }
        }
    }
}
