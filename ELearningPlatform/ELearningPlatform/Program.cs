using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Teacher currentTeacher = InitializeTeacher();
            Student currentStudent = InitializeStudent();
            Dictionary<string,User> platformUser = InitializeUser();
            User? currentUser;
            Dictionary<string, Subject> subjects = InitializeSubjects(currentTeacher);

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
                            Console.WriteLine("Welcome back {0}!", currentUser.Username);
                            Console.WriteLine("Login Sucessfully");
                            Console.WriteLine();
                            if (currentUser is Teacher)
                            {
                                Console.WriteLine("Teacher Main Menu");
                                TeacherSystem((Teacher)currentUser, subjects, platformUser);
                            }
                            else if (currentUser is Student)
                            {
                                Console.WriteLine("Student Main Menu");
                                StudentSystem((Student)currentUser, subjects, platformUser);
                            }
                            else
                            {
                                Console.WriteLine("Admin Main Menu");
                                AdminSystem((Admin)currentUser, subjects, platformUser);

                            }
                        }
                        else
                        {
                            Console.WriteLine("Cannot Login");
                            Stop();
                        }
                                
                        break;
                    case 2:
                        RegisterUser(platformUser);
                        break;
                    default:
                        ClearScreen();
                        Console.WriteLine("See You !");
                        break;
                }
            } while (choice!=3);
                
                
            

            //StudyLesson(GetToSpecificSubject(subjects),currentStudent);
            //Setting(currentTeacher);

            //Setting(currentStudent);
            ////===================================

            //Lesson lesson = new Lesson("Testing", ".mp4", 5, currentTeacher);
            //char tempKey;
                

            //DoQuiz(GetToSpecificSubject(subjects), currentStudent);

            ////Edit subject still need add one more Lesson;
            //EditSubject(GetToSpecificSubject(subjects), currentTeacher);
        }
        public static void RegisterUser(Dictionary<string, User> platformUser)
        {

            ClearScreen();
            Console.WriteLine("Register User");
            Console.WriteLine();
            Console.WriteLine("1. Student");
            Console.WriteLine("2. Teacher");
            Console.WriteLine("3. Back");
            int choice = ChoiceSelection("What is your role or identity? :", 3);
            ClearScreen();
            
            if (choice == 3){
                return;
            }
            else 
            {
                string tempUsername;
                tempUsername = CheckInputNotNull("Enter your username\t\t:");
                while (platformUser.ContainsKey(tempUsername)){
                    Console.WriteLine("Current username is already used");
                    Console.WriteLine("Try Again");
                    tempUsername = CheckInputNotNull("Enter your username\t\t:");
                }
                string tempPasswrord = CheckInputNotNull("Enter your password\t\t:");
                string tempGmail = CheckInputNotNull("Enter your gmail \t\t:");
                string tempGender = CheckInputNotNull("Enter your Gender (Male\\Female)\t:");
                double tempFigure;
                Console.WriteLine();
                Console.WriteLine($"Your username\t:{tempUsername}");
                Console.WriteLine($"Your password\t:{tempPasswrord}");
                Console.WriteLine($"Your gmail\t:{tempGmail}");
                Console.WriteLine($"Your gender\t:{tempGender}");
                if (choice == 1)
                {
                    tempFigure = 300;
                    Console.WriteLine("Your fee will be RM{0}.", tempFigure);
                }
                else 
                {
                    tempFigure = 3000;
                    Console.WriteLine("Your salary will be RM{0} at the beginning.", tempFigure);
                }
                Console.WriteLine();
                if (YesOrNo("Confirm ? (Y for Yes, N for No)"))
                {
                    if (choice == 1)
                    {
                        platformUser.Add(tempUsername, new Student(tempUsername, tempPasswrord, tempGmail, tempFigure, tempGender));
                    }
                    else if (choice == 2)
                    {
                        platformUser.Add(tempUsername, new Teacher(tempUsername, tempPasswrord, tempGmail, tempFigure, tempGender));
                    }
                    ClearScreen();
                    Console.WriteLine("Successfully register");
                    Stop();
                }
            }
        }
        public static void AdminSystem(Admin currAdmin, Dictionary<string, Subject> subjects, Dictionary<string, User> platformUser)
        {
            int choice;
            do
            {
                ClearScreen();
                Console.WriteLine("1. View All User");
                Console.WriteLine("2. View All Subject");
                Console.WriteLine("3. Account");
                Console.WriteLine("4. Log out");
                choice = ChoiceSelection("Select your choice \t:", 4);
                ClearScreen();
                switch (choice)
                {
                    case 1:
                        ViewAllUser(currAdmin,platformUser);
                        break;
                    case 2:
                        ViewAllSubject(subjects);
                        break;
                    case 3:
                        Setting(currAdmin,platformUser);
                        break;
                    default:
                        Console.WriteLine("Logging out"); Stop();
                        break;
                }
            } while (choice != 4);
        }

        public static void ViewAllUser(Admin currAdmin,Dictionary<string, User> platformUser)
        {
            foreach (KeyValuePair<string,User> user in platformUser)
            {
                user.Value.ShowDetails();
                Console.WriteLine("=========================");
            }
            if (YesOrNo("Delete User? (Y for yes,N for no)"))
            {
                string tempUsername = CheckInputNotNull("Enter username to delete the user\t: ");
                if (currAdmin.Username.Equals(tempUsername))
                {
                    ClearScreen();
                    Console.WriteLine("You cant delete yourself !");
                }
                else
                {
                    if (platformUser.ContainsKey(tempUsername))
                    {
                        if (YesOrNo("Confirm? (Y for yes,N for no)"))
                        {
                            platformUser.Remove(tempUsername);
                            ClearScreen();
                            Console.WriteLine("Delete user successfully");
                        }
                        else
                        {
                            ClearScreen();
                            Console.WriteLine("Cancelled delete user");
                        }
                    }
                    else
                    {
                        ClearScreen();
                        Console.WriteLine("No such user");
                    }
                }
                
                Stop();
            }
        }

        public static void ViewAllSubject(Dictionary<string, Subject> subjects)
        {
            int choice;
            do
            {
                ClearScreen();
                Console.WriteLine("1. View Subject");
                Console.WriteLine("2. Add Subject");
                Console.WriteLine("3. Delete Lesson");
                Console.WriteLine("4. Delete Quiz");
                Console.WriteLine("5. Back");
                choice = ChoiceSelection("Select your choice \t:", 5);
                ClearScreen();
                switch (choice)
                {
                    case 1:
                        DisplaySubjects(subjects);
                        Stop();
                        ClearScreen();
                        break;
                    case 2:
                        AddSubject(subjects);
                        break;
                    case 3:
                        DeleteLesson(GetToSpecificSubject(subjects));
                        break;
                    case 4:
                        DeleteQuiz(GetToSpecificSubject(subjects));
                        break;
                    default:
                        break;
                }
            } while (choice!=5);
            
        }
        public static void DeleteLesson(Subject targetedSubject)
        {
            if (targetedSubject.Lessons.Count!=0)
            {
                targetedSubject.ShowDetailsLessons();
                int choice = ChoiceSelection("Which lesson u want to delete\t: ", targetedSubject.Lessons.Count);

                if (YesOrNo("Confirm? (Y for yes,N for no)"))
                {
                    ClearScreen();
                    targetedSubject.Lessons.RemoveAt(choice - 1);
                    Console.WriteLine("Delete lesson Successfully");
                }
                else
                {
                    ClearScreen();
                    Console.WriteLine("Cancelled delete lesson");
                }
                Stop();
            }
            else
            {
                ClearScreen();
                Console.WriteLine("No available Lesson");
                Stop();
            }
            
        }
        public static void DeleteQuiz(Subject targetedSubject)
        {
            if (targetedSubject.Quizzes.Count != 0)
            {
                targetedSubject.ShowDetailsQuizzes();
                int choice = ChoiceSelection("Which quiz u want to delete\t: ", targetedSubject.Quizzes.Count);

                if (YesOrNo("Confirm? (Y for yes,N for no)"))
                {
                    ClearScreen();
                    targetedSubject.Quizzes.RemoveAt(choice - 1);
                    Console.WriteLine("Delete quiz Successfully");
                }
                else
                {
                    ClearScreen();
                    Console.WriteLine("Cancelled delete quiz");
                }
                Stop();
            }
            else
            {
                ClearScreen();
                Console.WriteLine("No available Quizz");
                Stop();
            }
        }

        public static void AddSubject(Dictionary<string, Subject> subjects)
        {
            //Dictionary<string, Subject> subjects = new Dictionary<string, Subject>{
            //    { "MT",new Subject("MT", "Mathematic", "Calculations") },
            //    { "EN",new Subject("EN", "English", "International Language")}
            //};
            string tempShortform = CheckInputNotNull("Enter the shortform for the subject\t: ");
            while (subjects.ContainsKey(tempShortform))
            {
                Console.WriteLine("Current subject shortform is already used");
                Console.WriteLine("Try Again");
                tempShortform = CheckInputNotNull("Enter the shortform for the subject\t: ");
            }
            string tempTitle= CheckInputNotNull("Enter the title for the subject\t\t: ");
            string tempDescription= CheckInputNotNull("Enter the description for the subject\t: ");


            ClearScreen();
            Console.WriteLine($"Subject shortform \t:{tempShortform}");
            Console.WriteLine($"Subject Title\t\t:{tempTitle}");
            Console.WriteLine($"Subject Description\t:{tempDescription}");
            Console.WriteLine();
            if (YesOrNo("Confirm? (Y for yes,N for no)"))
            {
                subjects.Add(tempShortform, new Subject(tempShortform, tempTitle, tempDescription));
                ClearScreen();
                Console.WriteLine("Added Subject Successfully");
            }
            else
            {
                ClearScreen();
                Console.WriteLine("Cancelled add subject");
            }
            Stop();
        }

        public static void StudentSystem(Student currStudent, Dictionary<string, Subject> subjects, Dictionary<string, User> platformUser)
        {
            int choice;
            do
            {
                
                Console.WriteLine("1. Do quiz");
                Console.WriteLine("2. Watch Lesson");
                Console.WriteLine("3. Account");
                Console.WriteLine("4. Log out");
                choice = ChoiceSelection("Select your choice \t:", 4);

                ClearScreen();
                switch(choice)
                {
                    case 1:
                        DoQuiz(GetToSpecificSubject(subjects),currStudent);
                        break;
                    case 2:
                        StudyLesson(GetToSpecificSubject(subjects),currStudent);
                        break;
                    case 3: Setting(currStudent, platformUser);
                        break;
                    default:
                        Console.WriteLine("Logging out"); Stop();
                        break;
                }
            } while (choice != 4);

        }

        public static void TeacherSystem(Teacher currTeacher, Dictionary<string, Subject> subjects, Dictionary<string, User> platformUser)
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
                    case 3: Setting(currTeacher,platformUser); break;
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
                if (platformUser[tempUsername].CheckPasswor(password))
                {
                    
                    currentUser = platformUser[tempUsername];
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
                {"Simon",new Student("Simon","unknowns", "boom@gmail", 400, "Rather not to say")},
                {"Admin",new Admin("Admin","0","admin@gmail.com","rather not to say","temp@email") }
            };
            return initializeUser;
        }
        public static void StudyLesson(Subject targetedSubject,Student currStudent)
        {
            if (targetedSubject.Lessons.Count!=0)
            {
                targetedSubject.ShowDetailsLessons();
                int choice = ChoiceSelection("Which u want to watch? (0 Back)", targetedSubject.Lessons.Count, 0);
                ClearScreen();
                if (choice != 0)
                    targetedSubject.Lessons[choice - 1].Learning(currStudent);
                
            }
            else
            {
                ClearScreen();
                Console.WriteLine("No availble Lessons here");
                Stop();
                ClearScreen();
            }
            
        }

        public static void Setting(User currUser, Dictionary<string, User> platformUser)
        {
            Console.WriteLine("1. Profile");
            Console.WriteLine("2. Change Password");
            Console.WriteLine("3. Back");
            int choice=ChoiceSelection("Pick one \t:", 3);
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
                                //to check
                                platformUser.Add(tempInput, currUser);
                                
                                platformUser[tempInput].Username= tempInput;
                                platformUser.Remove(currUser.Username);


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
                    ClearScreen();
                }
                else
                    ClearScreen();
            }
            else if (choice == 2)
            {
                Console.WriteLine("Change Password");
                string tempOldPassword = CheckInputNotNull("Enter Your Old Password\t: ");
                string tempNewPassword = CheckInputNotNull("Enter Your New Password\t: ");
                bool successfully;
                if(currUser is Admin)
                {
                    string tempBackupEmail = CheckInputNotNull("Enter Your Backup Email\t: ");
                    successfully=((Admin)currUser).EditPassword(tempBackupEmail, tempOldPassword, tempNewPassword);
                }
                else
                {
                    successfully = currUser.EditPassword(tempOldPassword, tempNewPassword);
                }
                ClearScreen();
                if (successfully)
                    Console.WriteLine("Change Successfully");
                else
                    Console.WriteLine("Change Unsuccessfully");
                Stop();
                ClearScreen();
            }
            else
                ClearScreen();
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
            Console.WriteLine();
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
            ClearScreen();
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
            int choice;
            do
            {
                ClearScreen();
                Console.WriteLine("1. Add Question");
                Console.WriteLine("2. Add Quiz");
                Console.WriteLine("3. Add Lesson");
                Console.WriteLine("4. Back");
                choice = ChoiceSelection("Enter your choice", 4);
                ClearScreen();
                switch (choice)
                {
                    case 1: AddQuestion(targetedSubject); break;
                    case 2: AddQuiz(targetedSubject, currTeacher); break;
                    case 3: AddLesson(targetedSubject, currTeacher); break;
                    default: break;
                }
            } while (choice!=4);
            



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
                ClearScreen();
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
                    Stop();
                }
                Console.WriteLine();
            } while (YesOrNo("Continue Add Questions?"));
            ClearScreen();
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
                Console.WriteLine("Enter the Difficulty for the quiz ( range from 1-5):");
            } while (!double.TryParse(Console.ReadLine(), out tempDifficulty) || !(tempDifficulty >= 1 && tempDifficulty<=5) );
            Console.WriteLine("The Title for the quiz is \t:{0}", tempTitle);
            Console.WriteLine("The Difficulty is \t\t:{0}", tempDifficulty);
            if (YesOrNo("Confirm add ? (Y for yes, N for No)"))
            {
                targetedSubject.AddQuizz(new Quiz(tempTitle, currTeacher, tempDifficulty));
                ClearScreen();
                Console.WriteLine("Sucessfully add Quiz");
                Stop();
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
            if (targetedSubject.Quizzes.Count!=0)
            {
                targetedSubject.ShowDetailsQuizzes();
                int numberQuiz = ChoiceSelection("Which Quiz u want to Do", targetedSubject.Quizzes.Count);
                ClearScreen();
                
                Quiz targetedQuiz = targetedSubject.Quizzes[numberQuiz - 1];
                if (targetedQuiz.Question.Count!=0) {
                    targetedQuiz.Learning(currectUser);
                    Stop();
                        //Todo what if we do not have finalScore.
                        //Console.WriteLine("You have become the high score holder with {0}", targetedQuiz.HighScore);
                        //targetedQuiz.HighScoreHolder = currectUser;
                        //Console.WriteLine("Keep it up !");

                    //int correct = 0;
                    //Console.WriteLine(targetedQuiz.Title);
                    //Console.WriteLine();
                    //foreach (Question question in targetedQuiz.Question)
                    //{
                    //    Console.WriteLine(question.Topic);
                    //    Console.Write("What is your answer :");
                    //    string tempAnswer = Console.ReadLine();
                    //    if (tempAnswer == question.Answer)
                    //        correct++;
                    //}
                    ////Todo Avoid Null Exception
                    //double finalScore = (double)correct / targetedQuiz.Question.Count * 100;
                    //Console.WriteLine("Average Score is : {0}%", finalScore);
                    //if (finalScore > targetedQuiz.HighScore)
                    //{
                    //    Console.WriteLine("You have become the high score holder with {0}", finalScore);
                    //    targetedQuiz.HighScore = finalScore;
                    //    targetedQuiz.HighScoreHolder = currectUser;

                    //}
                    //else
                    //    Console.WriteLine("Keep it up !");
                    //currectUser.FinishAQuiz(targetedQuiz.Title, finalScore);
                    //Console.WriteLine();
                    //targetedQuiz.ShowDetails();
                }
                else
                {
                    Console.WriteLine("Sorry this quiz is not unavailable temporary.");
                    Stop();
                }
                
            }
            else
            {
                Console.WriteLine("Sorry there isnt have any quiz for this subject.");
                Stop();
            }
            ClearScreen();
        }
    }
}