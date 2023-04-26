using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRIDCO.SharedKernel;
using GRIDCO.SharedKernel.Interfaces;

namespace GRIDCO.Core.ProjectAggregate.Entity
{
  public  class GRIDCO_User_Credential : BaseEntity, IAggregateRoot
    {
        public string User_Name { get; set; }
        public string Password { get; set; }
        public int User_Type { get; set; }
        public int User_Id { get; set; }

        public string Mobile_No { get; set; }
    }
}
