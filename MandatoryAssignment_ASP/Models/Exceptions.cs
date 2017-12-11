using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandatoryAssignment_ASP.Models
{
    class Trying_To_Open_File_That_Does_Not_Exist : ApplicationException
    {
        public Trying_To_Open_File_That_Does_Not_Exist(String problem) : base(problem)
        {
        }
    }
}
