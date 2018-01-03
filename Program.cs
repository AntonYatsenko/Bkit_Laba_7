using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA7LINQ
{
    class Program
    {
        public class Sotrudnik
        {
            public int ID;
            public string name;
            public int otdelID;

            public Sotrudnik(int a, string b, int c)
            {
                this.ID = a;
                this.name = b;
                this.otdelID = c;
            }

            public override string ToString()
            {
                return "(ID=" + this.ID.ToString() + "; name=" + this.name + "; otdelID=" + this.otdelID + ")";
            }
        }

        public class Otdel
        {
            public int otdelID;
            public string title;

            public Otdel(int a, string b)
            {
                this.otdelID = a;
                this.title = b;
            }

            public override string ToString()
            {
                return "(otdelID=" + this.otdelID.ToString() + "; title=" + this.title + ")";
            }
        }

        public class SotrudnikOtdel
        {
            public int IDS;
            public int IDO;

            public SotrudnikOtdel(int a, int b)
            {
                this.IDS = a;
                this.IDO = b;
            }
        }

        static List<Sotrudnik> S = new List<Sotrudnik>()
        {
            new Sotrudnik(1, "Иванов Иван Иванович", 1),
            new Sotrudnik(2, "Петров Петр Петрович", 1),
            new Sotrudnik(3, "Сидоров Сидор Сидорович", 1),
            new Sotrudnik(4, "Антонов Антон Антонович", 2),
            new Sotrudnik(5, "Андропов Борис Борисович", 3),
            new Sotrudnik(6, "Владиславов Владислав Владиславович", 3),
            new Sotrudnik(7, "Кириллов Кирилл Кириллович", 3),
        };

        static List<Otdel> O = new List<Otdel>()
        {
            new Otdel(1, "Мясо"),
            new Otdel(2, "Молоко"),
            new Otdel(3, "Сок"),
        };

        static List<SotrudnikOtdel> SO = new List<SotrudnikOtdel>()
        {
            new SotrudnikOtdel(1,1),
            new SotrudnikOtdel(2,1),
            new SotrudnikOtdel(3,2),
            new SotrudnikOtdel(4,2),
            new SotrudnikOtdel(5,3),
            new SotrudnikOtdel(6,3),
            new SotrudnikOtdel(7,3)
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Cписок всех сотрудников и отделов, отсортированный по отделам:");

            var q1 = O.GroupJoin(S,
                o => o.otdelID,
                s => s.otdelID,
                (otd, sot) => new
                {
                    ID = otd.otdelID,
                    Title = otd.title,
                    sotr = sot.Select(so => so.name)
                });
            foreach (var otd in q1)
            {
                Console.WriteLine(otd.ID + "  " + otd.Title);
                foreach (string sot in otd.sotr)
                {
                    Console.WriteLine(sot);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Cписок всех сотрудников, у которых фамилия начинается с буквы «А»:");

            var q2 = from s in S where s.name[0].Equals('А') select s;
            foreach (var x in q2) Console.WriteLine(x);
            Console.WriteLine();

            Console.WriteLine("Cписок всех отделов и количество сотрудников в каждом отделе:");

            var q3 = from o in O
                       join s in S on o.otdelID equals s.otdelID into temp
                       select new { ID = o.otdelID, Title = o.title, count = temp.Count() };
            foreach (var x in q3) Console.WriteLine(x.ID + "  " + x.Title + "  количество:" + x.count);
            Console.WriteLine();

            Console.WriteLine("Cписок отделов, в которых у всех сотрудников фамилия начинается с буквы «А»:");

            var q4 = from o in O
                       join s in S on o.otdelID equals s.otdelID into temp
                       where temp.All(s => s.name[0].Equals('А'))
                       select o;
            foreach (var x in q4) Console.WriteLine(x);
            Console.WriteLine();

            Console.WriteLine("Cписок отделов, в которых хотя бы у одного сотрудника фамилия начинается с буквы «А»:");

            var q5 = from o in O
                       join s in S on o.otdelID equals s.otdelID into temp
                       where temp.Any(s => s.name[0].Equals('А'))
                       select o;
            foreach (var x in q5) Console.WriteLine(x);
            Console.WriteLine();

            Console.WriteLine("Cписок всех отделов и список сотрудников в каждом отделе:");

            var q6 = from o in O
                       join so in SO on o.otdelID equals so.IDO into temp
                       select new { Otd = o, Sot = temp };
            foreach (var x in q6)
            {
                Console.WriteLine(x.Otd.title);
                foreach (SotrudnikOtdel sot in x.Sot)
                {
                    var i = from s in S where s.ID == sot.IDS select s;
                    foreach (var y in i) Console.WriteLine(y);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Cписок всех отделов и количество сотрудников в каждом отделе:");

            var q7 = from o in O
                       join so in SO on o.otdelID equals so.IDO into temp
                       select new { Otd = o, Sot = temp };
            foreach (var x in q7)
            {
                Console.WriteLine(x.Otd.title);
                Console.WriteLine(x.Sot.Count());
                Console.WriteLine();
            }
        }
    }
}
