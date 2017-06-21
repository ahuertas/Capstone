using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneFinal.ViewModels
{
    public class AssignmentViewModel
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public string Html_Url { get; set; }

        public string Points_Possible { get; set; }

        public string Due_at { get; set; }

        public long Course_Id { get; set; }
    }
}
