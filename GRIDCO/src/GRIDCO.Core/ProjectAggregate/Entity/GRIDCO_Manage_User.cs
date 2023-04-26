using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRIDCO.SharedKernel;
using GRIDCO.SharedKernel.Interfaces;

namespace GRIDCO.Core.ProjectAggregate.Entity
{
   public class GRIDCO_Manage_User : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email_Id { get; set; }
        public string Contact_Number { get; set; }
        public int User_Type { get; set; }
       // public int District_Id { get; set; }
    }
}
