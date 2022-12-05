using Project_VIS.Domain.DTO;
using Project_VIS.Domain.Exceptions;
using Project_VIS.Domain.TableModule;
using Project_VIS.Domain.TransactionScript;
using System.Text.RegularExpressions;

namespace Project_VIS
{
    internal class Program
    {
        public TeacherDTO LoginTeacher()
        {
            TeacherDTO teacher = new TeacherDTO();

            Console.WriteLine("Enter your login (email):");
            string email = Console.ReadLine();
            if (!IsValid(email))
            {
                Console.WriteLine("Nevalidni email");
                return LoginTeacher();
            }
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            try
            {
                teacher = teacher.LogIn(email, password) as TeacherDTO;
                return teacher;
            }
            catch(EntityNotFoundExeption ex)
            {
                Console.WriteLine("Nevalidni prihlasovaci udaje");
                return null;
            }
            
        }
        public StudentDTO LoginStudent()
        {
            StudentDTO student = new StudentDTO();

            Console.WriteLine("Enter your login (email):");
            string email = Console.ReadLine();
            if (!IsValid(email))
            {
                Console.WriteLine("Nevalidni email");
                return LoginStudent();
            }
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            try
            {
                student = student.LogIn(email, password) as StudentDTO;
                return student;
            }
            catch (EntityNotFoundExeption ex)
            {
                Console.WriteLine("Nevalidni prihlasovaci udaje");
                return null;
            }
        }

        private bool IsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|cz|org)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        public void LogOut(ref TeacherDTO t)
        {
            Console.WriteLine("Uživatel {0} {1} úspěšně odhlášen",t.First_Name,t.Last_Name);
            t = null;
        }
        public void LogOut(ref StudentDTO s)
        {
            Console.WriteLine("Uživatel {0} {1} úspěšně odhlášen", s.First_Name, s.Last_Name);
            s = null;
        }
        public TeacherProfile Update(TeacherDTO teacher)
        {
            string line;
            TeacherProfile profile = new TeacherProfile();
            Console.WriteLine("AKTUALIZUJTE PROFIL:");
            Console.Write($"Jméno ({teacher.First_Name}): ");
            line = Console.ReadLine();
            if (line != "")
                profile.first_name = line;
            Console.Write($"Příjmení ({teacher.Last_Name}): ");
            line = Console.ReadLine();
            if (line != "")
                profile.last_name = line;
            Console.Write($"Hledám žáky ({teacher.Active}) false/true: ");
            line = Console.ReadLine();
            if (line != "")
                profile.active = Convert.ToBoolean(line);
            Console.WriteLine($"Popis: ({teacher.Offer_Text}): ");
            line = Console.ReadLine();
            if (line != "")
                profile.offer_text = line;
            return profile;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("\t\tVÍTEJ UŽIVATELI V E-LEARNINGOVEM SYSTEMU\n");
            Console.WriteLine("Pro přihlášení jako UČITEL:");
            Program p = new Program();
            // USE CASE 1 prihlaseni
            TeacherDTO teacher = null;
            while (teacher is null)
            {
                teacher = p.LoginTeacher();
                try
                {
                    string name = teacher.ToString();
                    Console.WriteLine(name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Chyba: {0}; {1}", ex.Message, "Neplatné přihlašovací údaje"));
                }
            }
            IEnumerable<StudentDTO> st;
            try
            {
                st = teacher.GetMyStudents();
                Console.WriteLine("SEZNAM PŘIDĚLENÝCH ŽÁKŮ:");
                foreach (var item in st)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine($"POČET ŽÁKŮ:{st.Count()}");
            }
            catch
            {
                Console.WriteLine("Žádní žáci nenalezeni");
            }

            //USE CASE 2,3 pridani zaka + aktualizace zaznamu
            bool success = true;
            while (success)
            {
                Console.WriteLine("Pridejte žáka id: ");
                int student_id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    teacher.AddStudent(student_id);
                    Console.WriteLine("Žák úspěšně přidán.");
                    success = false;
                }
                catch(RelationAlreadyExists rex)
                {
                    Console.WriteLine(rex.Message);
                }
                catch(NoActiveTeacher nex)
                {
                    Console.WriteLine(nex.Message);
                    teacher.Update(p.Update(teacher));
                }
            }

            Console.WriteLine("Enter for LogOut");
            Console.ReadLine();
            p.LogOut(ref teacher);

            

            //Console.WriteLine("Pro přihlášení jako ŽÁK:");
            //StudentDTO student = null;
            //while (student is null)
            //{
            //    student = p.LoginStudent();
            //    try
            //    {
            //        Console.WriteLine(student.ToString());
            //    }
            //    catch (NullReferenceException ex)
            //    {
            //        Console.WriteLine(string.Format("Chyba: {0}; {1}", ex.Message, "Neplatné přihlašovací údaje"));
            //    }
            //}
            //p.LogOut(ref student);
            //UserDTO user = new UserDTO();
            //user.LoginIn(out teacher);

            //Console.WriteLine(teacher.ToString());

            //StudentDTO student;

            //var items = new StudentTM().GetAll(null);

            //foreach (var item in items)
            //{

            //    Console.WriteLine(item.ToString());

            //}

            //var tablemodule = new TeacherTM();

            //var newItemId = new CreateTeacher().Execute("Market", "Novotná", "marketa.novotna@volny.cz", "Slunecnice+2002");
            //Console.WriteLine(newItemId);
            //int newItemId = 2;
            //try
            //{
            //    var person = tablemodule.GetById(newItemId);
            //    Console.WriteLine(person.ToString());

            //    person.Active = false;
            //    tablemodule.Update(person.Id);

            //    person = tablemodule.GetById(newItemId);
            //    Console.WriteLine(person.ToString());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            // tablemodule.Delete(newItemId);
        }

    }
}