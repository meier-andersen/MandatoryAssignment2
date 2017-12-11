using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/* Submission Class. Each object of this class is a submission to the lottery. 
 * Variables: 
 *      FirstName
 *      SurName
 *      EmailAdress
 *      PhoneNumber
 *      TicketID
 *      AlreadyTaken* 
 *      EntryNumber
 * Functions:
 *      Constructor 
 *      LoadFromFile
 *      SaveToFile
 *  
 *  All variables are public and can be get/set. 
 *  Most variables are needed in the ASP.Net interface and therfor have additional code for validation
 *  
 *  *AlreadyTaken is used to identify if a submission have already been made to that object. 
 */
namespace MandatoryAssignment_ASP.Models
{
    [Serializable]
    public class Submission
    {
        //constructors
        public Submission() { }
        public Submission(int NewEntryNumber, int NewTicketID) { EntryNumber = NewEntryNumber; TicketID = NewTicketID; }
        public Submission(int NewEntryNumber) { EntryNumber = NewEntryNumber; }

        //Functions
        public void LoadFromFile() //Loads the object from a file. 
        {
            try
            {
                Submission SubmissionFromFile = new Submission();
                using (FileStream strm = new FileStream("Submissions/Entry" + EntryNumber + ".dat", FileMode.OpenOrCreate)) //opens a file stream to the corresponding entry number
                {
                    IFormatter fmt = new BinaryFormatter();
                    SubmissionFromFile = fmt.Deserialize(strm) as Submission; //loads the object from the file.

                    //Overwrites valid information from the file to our real obejct
                    FirstName = SubmissionFromFile.FirstName;
                    SurName = SubmissionFromFile.SurName;
                    PhoneNumber = SubmissionFromFile.PhoneNumber;
                    EmailAdress = SubmissionFromFile.EmailAdress;
                    AlreadyTaken = SubmissionFromFile.AlreadyTaken;
                    TicketID = SubmissionFromFile.TicketID;
                }
            }
            catch (System.Runtime.Serialization.SerializationException) //in case that the files doesn't excist, a new version is created. 
            {
                if(TicketID == 0)
                {
                    Random random = new Random();
                    TicketID = random.Next(1, 2000);
                }
                SaveToFile();
                Console.Error.WriteLine("ERROR, Couldnt find the file: Entry{0].dat, a new version has been created with the TicketID", EntryNumber);
            }
        }
        public void SaveToFile() //saves the object to a file
        {
            using (FileStream strm = new FileStream("Submissions/Entry" + EntryNumber + ".dat", FileMode.Create))
            {
                IFormatter fmt = new BinaryFormatter();
                fmt.Serialize(strm, this);
            }
        }

        //Variables + relevant ASP.Net code for each
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Sur name")]
        public string SurName { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        [Required]
        [Display(Name = "E-mail adress")]
        public string EmailAdress { get; set; }

        [RegularExpression(@"^[0-9]{8,10}$")]
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name ="Valid Ticket ID")]
        public int TicketID { get; set; }

        public bool AlreadyTaken { get; set; } 
        public int EntryNumber { get; set; }
    }
}
