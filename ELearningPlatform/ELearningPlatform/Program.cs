namespace ELearningPlatform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            testing();
            //switch (MainMenu())
            //{
            //    case 1: break;
            //    case 2: break;
            //    case 3: break;
            //    default:
            //        break;
            //}

        }


        public static int MainMenu()
        {
            int output;
            Console.WriteLine("  _____________________________________ ");
            Console.WriteLine(" |                                     |");
            Console.WriteLine(" |         E-Learning Platform         |");
            Console.WriteLine(" |_____________________________________|");

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


        public static void testing()
        {

            SubjectQuizz mathSubject = new SubjectQuizz("Math", "Primary School Math");
            //Dictionary<string, SubjectQuizz> subjectInPlatform = new Dictionary<string, SubjectQuizz>();
            //subjectInPlatform.Add(mathSubject.SubjectTitle, mathSubject);
            List<SubjectQuizz> subjectInPlatform = new List<SubjectQuizz>();
            subjectInPlatform.Add(mathSubject);
            Console.WriteLine(subjectInPlatform[0]);
            Console.WriteLine("helo");
            
            Console.WriteLine(mathSubject.ToString());



            Console.WriteLine("Enter the question :");
            string tempQuestion = Console.ReadLine();
            Console.WriteLine("Enter the answer for the questions");
            string tempAnswer = Console.ReadLine();
            Console.WriteLine("Explainations ? ");
            string tempExplainations = Console.ReadLine();

            //for (int i = 0; i < subjectInPlatform.Count; i++)
            //{
            //    Console.WriteLine($"{i + 1}. " + subjectInPlatform.ElementAt(i).Value.SubjectTitle);
            //}
            int subjectNumber;
            do
            {
                Console.WriteLine("Number for the subject u want to add");
            } while (!int.TryParse(Console.ReadLine(), out subjectNumber) || subjectNumber-1> subjectInPlatform.Count);
            Console.WriteLine("The questions is :{0}", tempQuestion);
            Console.WriteLine("The answer is :{0}", tempAnswer);
            Console.WriteLine("The explainations is :{0}", tempExplainations);
            Console.WriteLine($"The subject is {subjectInPlatform.ElementAt(subjectNumber-1).Value.SubjectTitle}");
            string tempYesOrNo;
            do
            {
                Console.WriteLine("Confirm ? (Y for Yes, N for No)");
                tempYesOrNo= Console.ReadLine();
            } while (tempYesOrNo!="Y" && tempYesOrNo!="N");

            //foreach (SubjectQuizz quizz in subjectInPlatform)
            //{
            //    Console.WriteLine(quizz.SubjectTitle);
            //    Console.WriteLine("Helo");
            //}

            for(int i = 0;i<subjectInPlatform.Count;i++)
            {
                Console.WriteLine($"{i+1}. "+subjectInPlatform.ElementAt(i).Value.SubjectTitle);
            }


            Console.ReadLine();








            //User testing = new User("Jayden", "1", "@gmail.com");
            //Console.WriteLine(testing);
            //Teacher testingTeacher = new Teacher("Sean", "wowwa", "@gmail.com", 5000);
            //testingTeacher.editPassword("@gmail.com", "2");
            //Console.WriteLine(testingTeacher.ToString());
            //Student stud1 = new Student("Jiayin", "hehe", "@gmail.com", 1000);
            //Console.WriteLine(stud1.ToString());
        }

    }
}