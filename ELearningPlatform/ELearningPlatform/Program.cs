using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace ELearningPlatform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initialize Variables
            //Todo Initialize User or Teacher
            Teacher currentUser = InitializeUser();
            //Todo Remove the casting after all 
            Dictionary<string, Subject> subjects = InitializeSubjects(currentUser);

            //Todo Use this to check if it is a teacher
            //if (currentUser is Teacher)
            //{
            //    Console.WriteLine("Is a teacher");
            //    currentUser = (Teacher)currentUser;
            //}

            EditSubject(subjects);
            
                
            
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
        public static void Logo()
        {
            Console.WriteLine("  _____________________________________ ");
            Console.WriteLine(" |                                     |");
            Console.WriteLine(" |         E-Learning Platform         |");
            Console.WriteLine(" |_____________________________________|");
        }

        public static int MainMenu()
        {
            Logo();
            int output;

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Quit");

            do
            {
                Console.WriteLine("Enter Your Choice! Only Integer");
            } while (!int.TryParse(Console.ReadLine(), out output));

            return output;
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
            return subjects;
        }
        public static Teacher InitializeUser()
        {
            return new Teacher("Sean", "1100", "Email", 3500, "Male");
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
            Console.WriteLine($"Do you want to {question} (Y for yes, N for no):");
            do
            {
                Console.WriteLine("Please Enter Y or N");
                choice = Console.ReadLine();
            } while (choice != "Y"&&choice!="N");
            return choice == "Y";
        }

        public static int ChoiceSelection(string question,int max, int min = 1)
        {
            int choice;
            Console.WriteLine(question);
            do
            {
                Console.WriteLine("Please Enter the Number accordingly. ");
            } while (!int.TryParse(Console.ReadLine(),out choice)&&choice>=min&&choice<=max);
            return choice;
        }
        public static void EditSubject(Dictionary<string, Subject> subjects)
        {
            ClearScreen();
            DisplaySubjects(subjects);
            Console.WriteLine("Which Subject u want to add questions(Enter the shortform) ");
            string subjectWantToAdd;
            do
            {
                subjectWantToAdd = Console.ReadLine();
            } while (!subjects.ContainsKey(subjectWantToAdd));

            ClearScreen();
            Subject targetSubject = subjects[subjectWantToAdd];
            targetSubject.ShowDetails();
            if (YesOrNo("Add Question"))
            {
                int numberQuiz = ChoiceSelection("Which Quiz u want to add question to", targetSubject.Quizzes.Count);
                ClearScreen();
                Console.WriteLine(numberQuiz + " " + targetSubject.Quizzes[numberQuiz - 1].Title);
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
                if (YesOrNo("add"))
                {
                    targetSubject.Quizzes[numberQuiz - 1].AddQuestion(new Question(tempQuestion, tempAnswer, tempExplainations));
                    ClearScreen();
                    Console.WriteLine("Added Successfully");
                    targetSubject.Quizzes[numberQuiz - 1].ShowQuestion();
                }
                else
                {
                    ClearScreen();
                    Console.WriteLine("Cancelled Modification");
                }

            }
            else
            {
                Console.WriteLine("Create new Quiz");
            }
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