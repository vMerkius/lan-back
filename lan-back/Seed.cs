
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
                            DateOfBirth = new DateTime(1999, 3, 16),
                            Gender="M",
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
                                                    Description = "desc2",
                                                    imageUrl="jk"
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
                                                    TranslatedWord="hi",
                                                    ImageUrl = "dd"
                                                },
                                                new Word
                                                {
                                                    OriginalWord="ta",
                                                    TranslatedWord="yes",
                                                    ImageUrl = "g"
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
                                                    Description = "desc22",
                                                    imageUrl="jhk"
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
                                                    TranslatedWord="hi2",
                                                    ImageUrl="jh"
                                                },
                                                new Word
                                                {
                                                    OriginalWord="ta2",
                                                    TranslatedWord="hiihi2",
                                                    ImageUrl="lkj"
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
                                            CorrectAnswer = 1,
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
                                            CorrectAnswer = 1,

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
