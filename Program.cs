using System.ComponentModel.Design;

namespace dtp6_contacts
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            DisplayCommands();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                    SafeQuit(lastFileName);
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2) // Ladda default-fil
                    {
                        //lastFileName = "address.lis"; FIXME: Redan satt
                        ReadFile(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1]; // Ladda fil i argument
                        ReadFile(lastFileName);
                    }
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        SaveContactList(lastFileName);
                    }
                    else
                    { 
                        lastFileName = commandLine[1];
                        SaveContactList(lastFileName);
                    }
                }
                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        Console.Write("personal name: ");
                        string persname = Console.ReadLine();
                        Console.Write("surname: ");
                        string surname = Console.ReadLine();
                        Console.Write("phone: ");
                        string phone = Console.ReadLine();
                    }
                    else
                    {
                        // NYI:
                        Console.WriteLine("Not yet implemented: new /person/");
                    }
                    // TBD: Save contactList
                }
                else if (commandLine[0] == "help")
                {
                    DisplayCommands();
                }
                else if (commandLine[0] == "edit")
                {
                    // NYI: edit a contact in external editor
                    Console.WriteLine("Not yet implemented - edit a contact in external editor");
                }


                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
                
            } while (commandLine[0] != "quit");
        }
                
        private static void SafeQuit(string lastFileName)
        {
            // Safe quit
            SaveContactList(lastFileName);
            Console.WriteLine("File saved. Bye!");
        }

        private static void SaveContactList(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(@"C:\Users\ÄGARE\" + lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                }
            }
        }

        // Method for loading a file and add to contactList[]
        private static void ReadFile(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(@"C:\Users\ÄGARE\"+ lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] attrs = line.Split('|');
                    Person contact = new Person();
                    contact.persname = attrs[0];
                    contact.surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    contact.phone = phones[0];
                    string[] addresses = attrs[3].Split(';');
                    contact.address = addresses[0];
                    for (int ix = 0; ix < contactList.Length; ix++)
                    {
                        if (contactList[ix] == null)
                        {
                            contactList[ix] = contact;
                            break;
                        }
                    }
                }
            }
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        }
    }
}
