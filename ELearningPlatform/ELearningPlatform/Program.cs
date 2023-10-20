using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Channels;

namespace ELearningPlatform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initialize Variables
            //Todo Initialize User or Teacher
            Teacher currentTeacher = InitializeTeacher();
            Student currentStudent = InitializeStudent();
            Dictionary<string,User> platformUser = InitializeUser();
            User? currentUser;




            //Todo Remove the casting after all 
            Dictionary<string, Subject> subjects = InitializeSubjects(currentTeacher);

            //Todo Use this to check if it is a teacher
            //if (currentUser is Teacher)
            //{
            //    Console.WriteLine("Is a teacher");
            //    currentUser = (Teacher)currentUser;
            //}
            if (true)
            {
                int choice;
                do
                {
                    ClearScreen();
                    choice = MainMenu();
                    ClearScreen();
                    switch (choice)
                    {
                        case 1:
                            currentUser = Login(platformUser);
                            if (currentUser != null)
                            {
                                ClearScreen();
                                Console.WriteLine("Login Sucessfully");
                                if (currentUser is Teacher)
                                {
                                    Console.WriteLine("Teacher Main Menu");
                                    TeacherSystem((Teacher)currentUser, subjects);
                                }
                                else if (currentUser is Student)
                                {
                                    Console.WriteLine("Student Main Menu");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cannot Login");
                                Stop();
                            }
                                
                            break;
                        case 2:
                            break;
                        default:
                            ClearScreen();
                            Console.WriteLine("See You !");
                            break;
                    }
                } while (choice!=3);
                
                
            }
            else
            {
                StudyLesson(GetToSpecificSubject(subjects));
                Setting(currentTeacher);

                Setting(currentStudent);
                //===================================

                Lesson lesson = new Lesson("Testing", ".mp4", 5, currentTeacher);
                char tempKey;
                

                DoQuiz(GetToSpecificSubject(subjects), currentStudent);

                //Edit subject still need add one more Lesson;
                EditSubject(GetToSpecificSubject(subjects), currentTeacher);



                //testing();
                //switch (MainMenu())
                //{
                //    case 1: break;
                //    case 2: break;
                //    case 3: break;
                //    default:
                //        break;
                //}
            }


        }
        public static void StudentSystem(Student currStudent, Dictionary<string, Subject> subjects)
        {
            int choice;
            do
            {
                Console.WriteLine("1. Do quiz");
                Console.WriteLine("2. Watch Lesson");
                Console.WriteLine("3. Account");
                Console.WriteLine("4. Log out");
                choice = ChoiceSelection("Select your choice \t:", 4);
            } while (choice != 4);
        }
        public static void TeacherSystem(Teacher currTeacher, Dictionary<string, Subject> subjects)
        {
            int choice;
            do
            {
                Console.WriteLine("1. View Study Materials");
                Console.WriteLine("2. Add Study Materials");
                Console.WriteLine("3. Account Setting");
                Console.WriteLine("4. Log out");
                choice =ChoiceSelection("Select your choice \t:", 4);
                ClearScreen();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("View Study Materials");
                        ShowSubjectDetails(GetToSpecificSubject(subjects));
                        break;
                    case 2: EditSubject(GetToSpecificSubject(subjects), currTeacher); break;
                    case 3: Setting(currTeacher); break;
                    default: Console.WriteLine("Logging out"); Stop(); break;
                }
            } while (choice!=4);
        }

        public static void ShowSubjectDetails(Subject subject)
        {
            ClearScreen();
            int choice;
            do
            {
                Console.WriteLine(subject.SubjectTitle);
                Console.WriteLine("1. Lessons");
                Console.WriteLine("2. Quizzes");
                Console.WriteLine("3. Back");
                choice = ChoiceSelection("Enter your choice", 3);
                ClearScreen();
                switch (choice )
                {
                    case 1:
                        Console.WriteLine($"Lessons in {subject.SubjectTitle}");
                        subject.ShowDetailsLessons();
                        Stop();
                        break;
                    case 2:
                        Console.WriteLine($"Quizz in {subject.SubjectTitle}");
                        subject.ShowDetailsQuizzes();
                        Stop(); 
                        break;
                    default:break;
                }
                ClearScreen();
            } while (choice!=3);
            
        }

        public static User? Login(Dictionary<string, User> platformUser)
        {
            User currentUser;
            Console.WriteLine("Login");
            string tempUsername = CheckInputNotNull("Username\t:");
            if (platformUser.ContainsKey(tempUsername))
            {
                string password = CheckInputNotNull("Password\t:");
                if (platformUser[tempUsername].checkPasswor(password))
                {

                    currentUser = platformUser[tempUsername];
                    Console.WriteLine("Welcome back {0}!", currentUser.Username);
                    return currentUser;
                }
                else
                    Console.WriteLine("Wrong Password");
            }
            else
                Console.WriteLine("No Such User");
            return null;
        }

        public static Dictionary<string,User> InitializeUser()
        {
            Dictionary<string, User> initializeUser = new Dictionary<string, User>()
            {
                {"Poh",new Teacher("Poh", "1100", "iot@gmail", 3500, "Male") },
                {"Jiayin",new Student("Jiayin","0214", "kitty@gmail", 300, "Female")},
                {"Simon",new Student("Simon","unknowns", "boom@gmail", 400, "Rather not to say")}
            };
            return initializeUser;
        }

        public static void PlayLesson(Lesson lesson)
        {
            char tempKey;
            do
            {
                Stop("stop the video");
                lesson.Play();
                Console.ReadKey();
                Console.WriteLine();
                if (!lesson.IsPlaying)
                {
                    Console.WriteLine("The Video Ends");
                    tempKey = 'q';
                }
                else
                {
                    lesson.Pause();
                    Console.WriteLine("Enter q to quit others to play");
                    tempKey = Console.ReadLine()[0];
                }
                
               
            } while (tempKey != 'q');
        }
        public static void StudyLesson(Subject targetedSubject)
        {
            targetedSubject.ShowDetailsLessons();
            Console.WriteLine();
            int choice=ChoiceSelection("Which u want to watch? (0 Back)", targetedSubject.Lessons.Count,0);
            if (choice != 0) {
                PlayLesson(targetedSubject.Lessons[choice-1]);
            }
        }


        public static void Setting(User currUser)
        {
            Console.WriteLine("1. Profile");
            Console.WriteLine("2. Back");
            int choice=ChoiceSelection("Pick one \t:", 2);
            ClearScreen();
            if (choice == 1)
            {
                currUser.ShowDetails();
                if (YesOrNo("Edit details ? (Y for Yes, N for No)"))
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Username");
                    Console.WriteLine("2. Email");
                    int choiceEdit = ChoiceSelection("Pick one \t:", 2);
                    string tempInput;
                    switch (choiceEdit)
                    {
                        case 1:
                            tempInput = CheckInputNotNull("Enter your new username\t:");
                            if (YesOrNo("Confirm? (Y for Yes,N for No)"))
                            {
                                currUser.Username = tempInput;
                            }

                            break;
                        case 2:
                            tempInput = CheckInputNotNull("Enter your new email\t:");
                            if (YesOrNo("Confirm? (Y for Yes,N for No)"))
                            {
                                currUser.Email = tempInput;
                            }
                            break;
                    }
                    ClearScreen();
                    Console.WriteLine("Successfully Edit");
                    currUser.ShowDetails();
                    Stop();
                }
                else
                    ClearScreen();
            }
                

            //switch (choice)
            //{
            //    case 1: 


            //        break;
            //    case 2: break;
            //}

        }
        public static void Stop(string words="Continue")
        {
            Console.WriteLine();
            Console.WriteLine($"Enter Any Key To {words}.");
            Console.ReadKey();
        }
        public static void Logo()
        {
            Console.WriteLine("  _____________________________________ ");
            Console.WriteLine(" |                                     |");
            Console.WriteLine(" |         E-Learning Platform         |");
            Console.WriteLine(" |_____________________________________|");
        }

        public static int MainMenu()
        {
            ClearScreen();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Quit");
            return ChoiceSelection("Please enter your choice.", 3);
        }

        //public static void AddQuestionToSubject(List)

        public static void DisplaySubjects(Dictionary<string,Subject> subjects)
        {
            Console.WriteLine();
            Console.WriteLine("Shortform \tSubject Title");
            Console.WriteLine("=============================");
            foreach (string key in subjects.Keys)
            { 
                Console.WriteLine(key + "\t\t" + subjects[key].SubjectTitle);
            }
        }

        //Todo Put temparary User, Remove it after all  
        public static Dictionary<string,Subject> InitializeSubjects(Teacher currentUser)
        {
            Dictionary<string, Subject> subjects = new Dictionary<string, Subject>{
                { "MT",new Subject("MT", "Mathematic", "Calculations") },
                { "EN",new Subject("EN", "English", "International Language")}
            };
            subjects["MT"].AddQuizz(new Quiz("math 2021", currentUser, 3.5));
            subjects["MT"].AddQuizz(new Quiz("math 2022", currentUser, 4.0));
            subjects["EN"].AddQuizz(new Quiz("English 2021", currentUser, 2.8));
            subjects["EN"].AddQuizz(new Quiz("English 2022", currentUser, 3.9));
            //Todo edit the length ? 

            subjects["MT"].AddLesson(new Lesson("Math Chapter 1",@"Math\Chap1.mp3",5,currentUser));
            subjects["MT"].AddLesson(new Lesson("Math Chapter 2", @"Math\Chap2.mp3", 22, currentUser));

            subjects["MT"].Quizzes[0].AddQuestion(new Question("12+22", "34", "12+22=34"));
            subjects["MT"].Quizzes[0].AddQuestion(new Question("12x2", "24", "12x2=24"));
            return subjects;
        }
        //Todo initialize teacher or user
        public static Teacher InitializeTeacher()
        {
            return new Teacher("Sean", "1100", "Email", 3500, "Male");
        }
        public static Student InitializeStudent()
        {
            return new Student("Jayden", "1", "@gmail", 300, "male");
        }


        //public static void InitializeVariables(User currentUser,Dictionary<string, Subject> subjects)
        //{
        //    currentUser = new Teacher("Sean", "1100", "Email", 3500, "Male");
            
        //}

        public static void ClearScreen()
        {
            Console.Clear();
            Logo();
            Console.WriteLine();
        }
        public static bool YesOrNo(string question)
        {
            string? choice;
            Console.WriteLine($"{question}:");
            do
            {
                Console.WriteLine("Please Enter Y or N");
                choice = Console.ReadLine();
            } while (choice != "Y"&&choice!="N");
            return choice == "Y";
        }

        public static string CheckInputNotNull(string question)
        {
            string tempInput;
            do
            {
                Console.Write(question);
                tempInput = Console.ReadLine();
            } while (tempInput == "");
            return tempInput;
        }
        public static Subject GetToSpecificSubject(Dictionary<string, Subject> subjects)
        {
            ClearScreen();
            DisplaySubjects(subjects);
            Console.WriteLine("Which Subject do u want ?");
            string subjectWantToAdd;
            do
            {
                subjectWantToAdd = CheckInputNotNull("Enter the shortform\t:");
            } while (!subjects.ContainsKey(subjectWantToAdd));

            return subjects[subjectWantToAdd];

        }

        public static int ChoiceSelection(string question,int max, int min = 1)
        {
            Console.WriteLine();
            int choice;
            Console.WriteLine(question);
            do
            {
                Console.WriteLine("Please Enter the Number accordingly. ");
            } while (!int.TryParse(Console.ReadLine(),out choice)||!(choice>=min&&choice<=max));
            return choice;
        }
        public static void EditSubject(Subject targetedSubject,Teacher currTeacher)
        {
           
            //Console.WriteLine("Which Subject u want to add questions or add subject (Enter the shortform) ");
            //string subjectWantToAdd;
            //do
            //{
            //    subjectWantToAdd = Console.ReadLine();
            //} while (!subjects.ContainsKey(subjectWantToAdd));
            

            ClearScreen();
            Console.WriteLine("1. Add Question");
            Console.WriteLine("2. Add Quiz");
            Console.WriteLine("3. Add Lesson");
            Console.WriteLine("4. Back");
            int choice = ChoiceSelection("Enter your choice", 4);
            ClearScreen();
            switch (choice)
            {
                case 1: AddQuestion(targetedSubject); break;
                case 2: AddQuiz(targetedSubject,currTeacher); break;
                case 3: AddLesson(targetedSubject, currTeacher);break;
                default: break;
            }



            //if (YesOrNo("Y for add question to quiz, N for add quiz."))
            //{
            //    targetedSubject.ShowDetailsQuizzes();
            //    int numberQuiz = ChoiceSelection("Which Quiz u want to add question to", targetedSubject.Quizzes.Count);
            //    ClearScreen();
            //    Console.WriteLine(targetedSubject.Quizzes[numberQuiz - 1].Title);

            //    do
            //    {
            //        Console.WriteLine("Enter the question :");
            //        string? tempQuestion = Console.ReadLine();
            //        Console.WriteLine("Enter the answer for the questions");
            //        string? tempAnswer = Console.ReadLine();
            //        Console.WriteLine("Explainations ? ");
            //        string? tempExplainations = Console.ReadLine();
            //        Console.WriteLine();
            //        Console.WriteLine("The questions is :{0}", tempQuestion);
            //        Console.WriteLine("The answer is :{0}", tempAnswer);
            //        Console.WriteLine("The explainations is :{0}", tempExplainations);
            //        Console.WriteLine();
            //        if (YesOrNo("Confirm detials ? (Y for yes, N for no)"))
            //        {
            //            targetedSubject.Quizzes[numberQuiz - 1].AddQuestion(new Question(tempQuestion, tempAnswer, tempExplainations));
            //            ClearScreen();
            //            Console.WriteLine("Added Successfully");
            //            targetedSubject.Quizzes[numberQuiz - 1].ShowQuestion();
            //        }
            //        else
            //        {
            //            ClearScreen();
            //            Console.WriteLine("Cancelled Modification");
            //        }
            //    } while (YesOrNo("Continue Add Questions?"));

            //    //Console.WriteLine("Enter the question :");
            //    //string? tempQuestion = Console.ReadLine();
            //    //Console.WriteLine("Enter the answer for the questions");
            //    //string? tempAnswer = Console.ReadLine();
            //    //Console.WriteLine("Explainations ? ");
            //    //string? tempExplainations = Console.ReadLine();
            //    //Console.WriteLine();
            //    //Console.WriteLine("The questions is :{0}", tempQuestion);
            //    //Console.WriteLine("The answer is :{0}", tempAnswer);
            //    //Console.WriteLine("The explainations is :{0}", tempExplainations);
            //    //Console.WriteLine();
            //    //if (YesOrNo("Confirm detials ? (Y for yes, N for no)"))
            //    //{
            //    //    targetedSubject.Quizzes[numberQuiz - 1].AddQuestion(new Question(tempQuestion, tempAnswer, tempExplainations));
            //    //    ClearScreen();
            //    //    Console.WriteLine("Added Successfully");
            //    //    targetedSubject.Quizzes[numberQuiz - 1].ShowQuestion();
            //    //}
            //    //else
            //    //{
            //    //    ClearScreen();
            //    //    Console.WriteLine("Cancelled Modification");
            //    //}
            //}
            //else
            //{
            //    ClearScreen();
            //    Console.WriteLine("Create new Quiz");
            //    string tempTitle;
            //    do
            //    {
            //        Console.WriteLine("Enter the Title for the quiz :");
            //        tempTitle = Console.ReadLine();
            //    } while (tempTitle.Equals(""));

            //    double tempDifficulty;
            //    do
            //    {
            //        Console.WriteLine("Enter the Difficulty for the quiz :");
            //    } while (!double.TryParse(Console.ReadLine(), out tempDifficulty));
            //    Console.WriteLine("The Title for the quiz is \t:{0}", tempTitle);
            //    Console.WriteLine("The Difficulty is \t\t:{0}", tempDifficulty);
            //    if (YesOrNo("Confirm add ? (Y for yes, N for No)"))
            //    {
            //        targetedSubject.AddQuizz(new Quiz(tempTitle, currTeacher, tempDifficulty));
            //        Console.WriteLine("Sucessfully add Quiz");
            //    }
            //    else
            //    {
            //        ClearScreen();
            //        Console.WriteLine("Cancelled Modification");
            //    }
            //}
        }
        public static void AddQuestion(Subject targetedSubject)
        {
            targetedSubject.ShowDetailsQuizzes();
            int numberQuiz = ChoiceSelection("Which Quiz u want to add question to", targetedSubject.Quizzes.Count);
            ClearScreen();
            Console.WriteLine(targetedSubject.Quizzes[numberQuiz - 1].Title);
            do
            {
                Console.WriteLine("Enter the question :");
                string? tempQuestion = Console.ReadLine();
                Console.WriteLine("Enter the answer for the questions");
                string? tempAnswer = Console.ReadLine();
                Console.WriteLine("Explainations ? ");
                string? tempExplainations = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("The questions is :{0}", tempQuestion);
                Console.WriteLine("The answer is :{0}", tempAnswer);
                Console.WriteLine("The explainations is :{0}", tempExplainations);
                Console.WriteLine();
                if (YesOrNo("Confirm detials ? (Y for yes, N for no)"))
                {
                    targetedSubject.Quizzes[numberQuiz - 1].AddQuestion(new Question(tempQuestion, tempAnswer, tempExplainations));
                    ClearScreen();
                    Console.WriteLine("Added Successfully");
                    targetedSubject.Quizzes[numberQuiz - 1].ShowQuestion();
                }
                else
                {
                    ClearScreen();
                    Console.WriteLine("Cancelled Modification");
                }
            } while (YesOrNo("Continue Add Questions?"));
        }

        public static void AddQuiz(Subject targetedSubject,Teacher currTeacher)
        {
            ClearScreen();
            Console.WriteLine("Create new Quiz");
            string tempTitle;
            do
            {
                Console.WriteLine("Enter the Title for the quiz :");
                tempTitle = Console.ReadLine();
            } while (tempTitle.Equals(""));

            double tempDifficulty;
            do
            {
                Console.WriteLine("Enter the Difficulty for the quiz :");
            } while (!double.TryParse(Console.ReadLine(), out tempDifficulty));
            Console.WriteLine("The Title for the quiz is \t:{0}", tempTitle);
            Console.WriteLine("The Difficulty is \t\t:{0}", tempDifficulty);
            if (YesOrNo("Confirm add ? (Y for yes, N for No)"))
            {
                targetedSubject.AddQuizz(new Quiz(tempTitle, currTeacher, tempDifficulty));
                Console.WriteLine("Sucessfully add Quiz");
            }
            else
            {
                ClearScreen();
                Console.WriteLine("Cancelled Modification");
            }
        }

        public static void AddLesson(Subject targetedSubject, Teacher currTeacher)
        {
            string tempLessonName = CheckInputNotNull("Enter Lesson Name\t:");
            string tempVideoPath = CheckInputNotNull("Enter Video Path\t:");
            int tempVideoLength = ChoiceSelection("Video Length (max 20)\t:",20);
            Console.WriteLine("Lesson Name \t:"+ tempLessonName);
            Console.WriteLine("Video Path \t:" + tempVideoPath);
            Console.WriteLine("Video Length \t:" + tempVideoLength +"s");
            if (YesOrNo("Confirm ?"))
            {
                targetedSubject.AddLesson(new Lesson(tempLessonName, tempVideoPath, tempVideoLength, currTeacher));
            }
            else
                Console.WriteLine("Cancelled");
        }
        public static void DoQuiz(Subject targetedSubject, Student currectUser)
        {

            //DisplaySubjects(subjects);
            
            ////string subjectWantToDo = GetSubjectWantToModify(subjects);
            //Subject targetedSubject= GetSubjectWantToModify(subjects);
            targetedSubject.ShowDetailsQuizzes();
            int numberQuiz = ChoiceSelection("Which Quiz u want to Do", targetedSubject.Quizzes.Count);
            ClearScreen();
            int correct = 0;
            Quiz targetedQuiz = targetedSubject.Quizzes[numberQuiz-1];
            Console.WriteLine(targetedQuiz.Title);
            Console.WriteLine();
            foreach (Question question in targetedQuiz.Question)
            {
                Console.WriteLine(question.Topic);
                Console.Write("What is your answer :");
                string tempAnswer = Console.ReadLine();
                if (tempAnswer ==question.Answer)
                    correct++;
            }
            //Todo Avoid Null Exception
            double finalScore = (double)correct / targetedQuiz.Question.Count * 100;
            Console.WriteLine("Average Score is : {0}%", finalScore);
            if (finalScore>targetedQuiz.HighScore)
            {
                Console.WriteLine("You have become the high score holder with {0}",finalScore);
                targetedQuiz.HighScore = finalScore;
                targetedQuiz.HighScoreHolder = currectUser;
                
            }
            else
                Console.WriteLine("Keep it up !");
            currectUser.FinishAQuiz(targetedQuiz.Title, finalScore);
            Console.WriteLine();
            targetedQuiz.ShowDetails();
            currectUser.ShowDetails();


        }
        public static void testing()
        {
            //Subject mathSubject = new Subject("MT", "Math", "Primary School Math");
            //Quiz quizz1 = new Quiz("math 2021", (Teacher)currentUser, 3.5);
            //if (YesOrNo("add question"))
            //{
                
            //    mathSubject.AddQuizz(quizz1);

            //    mathSubject.ShowDetails();
                

            //    //SubjectQuizz mathSubject = new("Math", "Primary School Math");
            //    //Dictionary<string, SubjectQuizz> subjectInPlatform = new Dictionary<string, SubjectQuizz>();
            //    //subjectInPlatform.Add(mathSubject.SubjectTitle, mathSubject);
            //    List<Subject> subjectQuizzs = new List<Subject>();
            //    List<Subject> subjectInPlatform = subjectQuizzs;
            //    //Console.WriteLine(subjectInPlatform.Count);
            //    subjectInPlatform.Add(mathSubject);
            //    //Console.WriteLine(subjectInPlatform[0]);

            //    //Console.WriteLine("helo");

            //    //Console.WriteLine(mathSubject.ToString());



            //    Console.WriteLine("Enter the question :");
            //    string? tempQuestion = Console.ReadLine();
            //    Console.WriteLine("Enter the answer for the questions");
            //    string? tempAnswer = Console.ReadLine();
            //    Console.WriteLine("Explainations ? ");
            //    string? tempExplainations = Console.ReadLine();

            //    //for (int i = 0; i < subjectInPlatform.Count; i++)
            //    //{
            //    //    Console.WriteLine($"{i + 1}. " + subjectInPlatform.ElementAt(i).Value.SubjectTitle);
            //    //}
            //    for (int i = 0; i < subjectInPlatform.Count; i++)
            //    {
            //        Console.WriteLine($"{i + 1}. " + subjectInPlatform[i].SubjectTitle);
            //    }


            //    int subjectNumber;
            //    do
            //    {
            //        Console.WriteLine("Number for the subject u want to add");
            //    } while (!int.TryParse(Console.ReadLine(), out subjectNumber) || subjectNumber - 1 > subjectInPlatform.Count);
            //    Console.WriteLine("The questions is :{0}", tempQuestion);
            //    Console.WriteLine("The answer is :{0}", tempAnswer);
            //    Console.WriteLine("The explainations is :{0}", tempExplainations);
            //    //Console.WriteLine($"The subject is {subjectInPlatform.ElementAt(subjectNumber-1).Value.SubjectTitle}");
            //    string? tempYesOrNo;
            //    do
            //    {
            //        Console.WriteLine("Confirm ? (Y for Yes, N for No)");
            //        tempYesOrNo = Console.ReadLine();
            //    } while (tempYesOrNo != "Y" && tempYesOrNo != "N");

                //foreach (SubjectQuizz quizz in subjectInPlatform)
                //{
                //    Console.WriteLine(quizz.SubjectTitle);
                //    Console.WriteLine("Helo");
                //}

                //for(int i = 0;i<subjectInPlatform.Count;i++)
                //{
                //    Console.WriteLine($"{i+1}. "+subjectInPlatform.ElementAt(i).Value.SubjectTitle);
                //}


                //Console.ReadLine();








                //User testing = new User("Jayden", "1", "@gmail.com");
                //Console.WriteLine(testing);
                //Teacher testingTeacher = new Teacher("Sean", "wowwa", "@gmail.com", 5000);
                //testingTeacher.editPassword("@gmail.com", "2");
                //Console.WriteLine(testingTeacher.ToString());
                //Student stud1 = new Student("Jiayin", "hehe", "@gmail.com", 1000);
                //Console.WriteLine(stud1.ToString());
            //}
        }
    }
}