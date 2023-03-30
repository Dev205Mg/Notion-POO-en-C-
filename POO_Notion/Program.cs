using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_Notion
{

    class Enfant : Etudiant
    {
        string classeEcole;
        Dictionary<string, float> notes;

        public Enfant(string name, int age, string classeEcole, Dictionary<string, float> notes) : base(name , age, null)
        {
            this.classeEcole = classeEcole;
            this.notes = notes;
        }

        public override void Afficher()
        {
            AfficherNomEtAge();
            Console.WriteLine(" Enfant en classe de " + classeEcole);
            if(notes != null && notes.Count > 0)
            {
                Console.WriteLine(" Notes moyennes :");
                foreach (var note in notes)
                {
                    Console.WriteLine("    " + note.Key + " : " + note.Value + " / 10");
                }
            }
            
            AfficherProfesseurPrincipale();
            Console.WriteLine();
        }
    }

    class Etudiant : Personne
    {
        protected string infoEtudes;
        public Personne professeurPrincipale;

        public Etudiant(string name, int age, string infoEtudes) : base(name, age)
        {
            this.infoEtudes = infoEtudes;
        }


        public override void Afficher()
        {
            AfficherNomEtAge();
            Console.WriteLine(" ETUDIANT EN " + infoEtudes);
            AfficherProfesseurPrincipale();
            Console.WriteLine();
        }

        protected void AfficherProfesseurPrincipale()
        {
            if (professeurPrincipale != null)
            {
                Console.WriteLine(" PROFESSEUR PRINCIPALE est :");
                professeurPrincipale.Afficher();
            }
        }
    }

    class Personne : IAffichable
    {
        // on le met en public pour qu'elle est accessible en dehors et seulle la personne qui peut l'appeler;
        static int nombreDePersonnes = 0; // variable de classe, ca ne change pas automatiquement si on instance la classe

        public string name { get; private set; }
        public int age { get; private set; }

        /*
        public string name { get; set; }// par defaut c'est private; // on peut faire init mais sur .Net 5; C# 9

        int _age;
        public int age { get {
                return _age;
            }
             set {
                if(value > 0)
                {
                    _age = value;
                }
            }
        }*/
        // variable d'instance
        protected string emploi; // par default les strings sont null, mais pas int, int 0;
        protected int numeroPersonne;
        /*
        public Personne(string name, int age) : this(name, age, "(non spécifié)") // "(non spécifié)"
        {
        }   on peut créer beaucoups de constructeurs
        */
        /*
        public void SetNom(string value)
        { // Mutateur : rend le nom en priver Modifiable
            this.name = value;
        }
        
        public string GetNom() // Accesseur : rend le nom en priver accassible
        {
            return name;
        }
        */
        /*
        public Personne()
        {
            nombreDePersonnes++;// on incremente a chaque instance et on le stock dans numeroPersonne
            this.numeroPersonne = nombreDePersonnes;// ici
        }*/

        public Personne(string name, int age, string emploi = null)// : this() // "(non spécifié)"
        {
            this.name = name;
            this.age = age;
            this.emploi = emploi;

            nombreDePersonnes++;// on incremente a chaque instance et on le stock dans numeroPersonne
            this.numeroPersonne = nombreDePersonnes;// ici
        }

        public virtual void Afficher()
        {
            AfficherNomEtAge();
            if (emploi == null)
            {
                Console.WriteLine(" Aucun emplois spécifié \n");
            }
            else
            {
                Console.WriteLine(" EMPLOI : " + emploi + "\n");
            }
        }

        protected void AfficherNomEtAge()
        {
            Console.WriteLine("PERSONNE N°" + numeroPersonne);
            Console.WriteLine(" NOM : " + name);
            Console.WriteLine(" AGE : " + age + " ans");
        }

        public static void AfficheNombreDePersonnes()
        {
            //on ne peut pas acceder  à une variable qui n'est pas statique 
            Console.WriteLine("Nombre total  de personnes : " + nombreDePersonnes);
        }
    }

    class TableDeMultilptiplication : IAffichable
    {
        int num;

        public TableDeMultilptiplication(int num)
        {
            this.num = num;
        }

        public void Afficher()
        {
            Console.WriteLine("Table de multiplication de " + num);
            for(int i = 0; i <= 10; i++)
            {
                Console.WriteLine(num + " x " + i + " = " + (num * i));
            }
        }
    }

    interface IAffichable
    {
        void Afficher();
    }

    class Program
    {
        static void Main(string[] args)
        {/*
            var personne1 = new Personne("Paul", 30, "Développeur");
            personne1.Afficher();

            var personne2 = new Personne("Jacques", 35, "Professeur");
            personne2.Afficher();*/
            
            var elements = new List<IAffichable> {
                new Personne("Paul", 30, "Développeur"),
                new Personne("Jacques", 35, "Professeur"),
                new Etudiant("David", 20, "Philosophie"),
                new Enfant("Juliette", 8, "CP", null),
                new TableDeMultilptiplication(2)
            };

            // on va trier par non mais il faut que le nom soit public
            //personnes = personnes.OrderBy(p => p.name).ToList();

            // Filtrer en utilisant WHERE et IS
            // en utilisant WHERE
            //personnes = personnes.Where(p => p.age >= 25).ToList();
            // en utilisant IS
            //personnes = personnes.Where(p => p is Enfant).ToList();
            //personnes = personnes.Where(p => (p.name[0] == 'J' && p.age > 25 )).ToList();

            foreach (var element in elements)
            {
                element.Afficher();
            }/*

            var table2 = new TableDeMultilptiplication(2);
            table2.Afficher();*/

            /*
            //Console.WriteLine("Nombre total  de personnes : " + Personne.nombreDePersonnes);// only the Personne class who can call him

            Personne.AfficheNombreDePersonnes();*/
            /*
            var personne1 = new Personne("Paul", 30);
            Console.WriteLine("Nombre de la personne : " + personne1.name);
            personne1.Afficher();
            
            
            var etudiant = new Etudiant("David", 20, "Sociologie Ankatso") {
                professeurPrincipale = new Personne("Jaques", 35, "Professeur")
            };
            etudiant.Afficher();
           

            var notesEnfant1 = new Dictionary<string, float> {
                { "Maths", 5f }, { "Geo", 8.5f }, { "Dictée", 3.3f } // f : c'est dire float
            };

            var enfant1 = new Enfant("Sofie", 7, "CP", notesEnfant1) 
            {
                professeurPrincipale = new Personne("Jean", 39, "Professeur des écoles")
            };
            enfant1.Afficher();
             */

            Console.ReadKey();
        }
    }
}
