# MandatoryAssignment2
My hand-in of the Mandatory Assignment 2 for C# .net Framework class. 
The code is written in .Net Core with ASP.Net. 

## First time:
- Launch the application 
- Select "Generate New Tickets" in the menu 
- Press the "Generate Tickets" button, in the "Generate New Tickets" menu 
- This will generate all that is needed, and the user can freely use the application

## Menues in the application:
**Submit:**
- Allows the user to submit an entry to the lottery with a valid Ticket ID and valid information.
**View All submissions:**
- Shows a list of all valid submissions, that have been made. 
**Generate new Tickets:**
- Overwrites all excisting data and creates 100 new entries with Unique ID numbers. 


## Finding valid ID numbers:
Once the user have created new tickets, a file names "ValidNumbers.txt can be found in the Submission folder, in the project. 
This file contains all valid Ticket ID numbers and their corresponding entry number.

## How the application works: 
When **creating new tickets**, each valid entry is created as an object with an EntryNumber and a unique TicketID. 
All theese are saved to seperate files in Submission/Entry #.dat. where # is the coresponding EntryNumber.

When a **new submission** is handed in, the application itirates threw all valid entries by loading them from the file. It compares the TicketID the object, with the TicketID that the user inputs. If they corespond, the application checks if a submission have already been made to the entry. If there isn't, it accepts the users submission. 

When **viewing all submission**, the application itirates threw all valid entries by loading them from the file. It checks if their value "AlreadyTaken" is true, and if it is, it adds the object to a list that is passes to the view. This means that it only shows entries that have been submitted to, not all 100 entries. 


## Submission class: 
 * Variables: 
 *      FirstName
 *      SurName
 *      EmailAdress
 *      PhoneNumber
 *      TicketID
 *      AlreadyTaken
 *      EntryNumber
 * Functions:
 *      Constructor 
 *      LoadFromFile
 *      SaveToFile
