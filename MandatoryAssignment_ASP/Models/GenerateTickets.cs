using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* Classes used to generate new tickets. 
 * When the function "Generate" is called, # new entries is created.
 * Theese entries are saves as objects in the file system at the adress Project:Submissions/Entry #.dat
 * 1 file will be created for each object
 * 
 * Each object will contain its entry number and a unique TicketID number.
 * 
 * Functions:
 *      Generate
 */
namespace MandatoryAssignment_ASP.Models
{
    [Serializable]
    class GenerateTickets
    {
        public void Generate(int TotalNumEntries)
        {
            /*Generates n tickets 
             * Entry: 0-n
             * Ticket ID: Random number 1-20 above the last created Ticket ID
             * Creates n+1 files in the folder submissions
             * 1) ValidNumbers: All Valid TicketIDs + their corresponding Entry ID
             * 2) Entry #.dat: One object pr Entry (0-n)
            */
            Random random = new Random(); //needed for generating random Ticket ID numbers
            int RandomTicketID = 100;
            
            string fileName_ValidNumbers = "Submissions/ValidNumbers.txt"; 
            Directory.CreateDirectory(Path.GetDirectoryName(fileName_ValidNumbers)); //makes sure that the directory excists
            var ValidNumbersFile = new StreamWriter(new FileStream(fileName_ValidNumbers, FileMode.Create), Encoding.UTF8); //The file with vaild ID numbers
            ValidNumbersFile.WriteLine("EntryNumber \tTicketID");

            for (int i = 0; i < TotalNumEntries; i++)
            {
                RandomTicketID += random.Next(1, 20);
                ValidNumbersFile.WriteLine(i + "\t\t" + RandomTicketID); //writes the ID to the first file
                Submission p = new Submission(i, RandomTicketID); //Creates a new object with the EntryNumber and TicketID as input 
                p.SaveToFile(); //saves the object to Entry #.dat
            }
            ValidNumbersFile.Close(); //Closes the stream to ValidNumbers.txt.
            Console.Out.WriteLine("New tickets have been generated.");
        }
    }
}
