using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRIDCO.Core.Model
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string Oldpassword { get; set; }

        public string Newpassword { get; set; }

        public string ConfirmNewpassword { get; set; }
    }
}
