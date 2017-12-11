using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MandatoryAssignment_ASP.Models;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

/*Controller for Submission. 
 * Pages:
 *      Index
 *      ViewSubmissions
 *      Submit 
 *      GenerateNewTickets
 * Post requests:
 *      Submit
 *      GenerateTickets
 * Error 
 * 
 */
namespace MandatoryAssignment_ASP.Controllers
{
    public class SubmissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewSubmission() //View of all submissions that have been submitted 
        {
            /* Iterates threw all 100 entries Submission/Entry #.dat
             * For each entry the object is loaded from the file and checked if a submission had been made to that Entry.
             * If AlreadyTaken = true: a submission to that entry have been made. 
             *      - The object is then added to an array that is passed to the view. 
             * If The file can't be found, it is handled in the LoadFromFile function. 
             */
            int TotalNumEntries = 100;
            List<Submission> ListOfValidSubmissions = new List<Submission>();
            string fileName = "Submissions/Test.txt";

            Directory.CreateDirectory(Path.GetDirectoryName(fileName)); //makes sure that a directory is created to where we store the objects
            for (int i = 0; i < TotalNumEntries; ++i) //Iterates threw all n objects 
            {
                Submission Temp = new Submission(i);
                Temp.LoadFromFile(); //Loads the file from the folder
                if(Temp.AlreadyTaken)
                    ListOfValidSubmissions.Add(Temp); //If the variable AlreadyTaken=true, the objects is added to the list 
            }
            return View(ListOfValidSubmissions); //Calls the view with the list of valid submissions as input
        }

        public IActionResult Submit() //The scene from where the user can submit a submission 
        {
            return View();
        }

        public IActionResult GenerateNewTickets() //The scene for generating new entries and Ticket ID
        {
            return View();
        }

        //Handling requrest from the pages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int id, [Bind("EntryNumber, TicketID, FirstName, SurName, EmailAdress, PhoneNumber")] Submission InputSubmission) //handles request for a new submission
        {
            /* Handles requests for creating a new submission 
             * 1) Ensures that inputs a valid(see submissioncs for requirements)
             * 2) Iterates threw all entries Entry #.dat 
             *      - Loads each object from the file 
             *      - Compares Ticket ID of the object(SubmissionFromFile) with the Ticket ID that the user has inputted(Inputsubmission)
             *      - if they are equal, 'SubmissionFromFile' is checked to see if it has already been submitted 
             *          - If it hasn't: The Entry number is transfered from SubmissionFromFile to Inputsubmission
             *          - Inputsubmission is saved and overwrites SubmissionFromFile in the folder. 
             *      - If the entry is already used, or no Ticket ID numbers fit, the submission isn't accepted 
             */
            if (ModelState.IsValid) //Ensures that inputs are valid(see submission.cs for requirements) 
            {
                int TotalNumEntries = 100;

                for (int i = 0; i < TotalNumEntries; ++i)
                {
                    Submission SubmissionFromFile = new Submission(i);
                    SubmissionFromFile.LoadFromFile();
                    if (SubmissionFromFile.TicketID == InputSubmission.TicketID)
                    {
                        if (!SubmissionFromFile.AlreadyTaken) //Checks if a submission have already been made to the entry
                        {
                            //if the entry isn't already used, the input saves itself to the gives entry
                            InputSubmission.AlreadyTaken = true;
                            InputSubmission.EntryNumber = SubmissionFromFile.EntryNumber;
                            InputSubmission.SaveToFile();
                            return RedirectToAction(nameof(Index));
                        }
                        break; //When the matching ticket ID has been found, the loop is broken. No need to check the rest of the objects
                    }
                        
                }         
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateTickets()
        {
            /* Handles requrest to generate new Entries and ticket ID numbers
             * Creates an object of the type NewTickets and calls its function generate new entries and Ticket ID
             * Returns the user to the index scene
             */
            if (ModelState.IsValid)
            {
                int TotalNumEntries = 100;
                GenerateTickets NewTickets = new GenerateTickets();
                NewTickets.Generate(TotalNumEntries);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}