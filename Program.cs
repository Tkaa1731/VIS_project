using Project_VIS.Domain.TableModule;
using Project_VIS.Domain.TransactionScript;

namespace Project_VIS
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var items = new TeachersList().Execute();

            foreach (var item in items)
            {
                Console.Write(item.Id + " ");
                Console.WriteLine(item.First_Name + " " + item.Last_Name + " aktivni: " + item.Active + " login: "+ item.Email);
                Console.WriteLine(item.Offer_Text);
                Console.WriteLine();
            }

            //var tablemodule = new TeacherTM();

            //var newItemId = new CreateTeacher().Execute("Market", "Novotná", "marketa.novotna@volny.cz","Slunecnice+2002");
            //Console.WriteLine(newItemId);

            //var person = tablemodule.GetById(newItemId);
            //Console.WriteLine("Jmeno a příjmení: " + person.First_Name + " " + person.Last_Name);

            //tablemodule.Update(person.Id, "Markéta", person.Last_Name, person.Email);

            //person = tablemodule.GetById(newItemId);
            //Console.WriteLine("Jmeno a příjmení: " + person.First_Name + " " + person.Last_Name);

            //tablemodule.Delete(newItemId);
        }
    }
}