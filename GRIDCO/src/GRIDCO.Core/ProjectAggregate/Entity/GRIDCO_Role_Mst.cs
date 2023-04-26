using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRIDCO.SharedKernel.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace GRIDCO.Core.ProjectAggregate.Entity
{
   public class GRIDCO_Role_Mst : IAggregateRoot
    {
        [Key]
        public int User_Type_Id { get; set; }
        public string User_Type { get; set; }
        public string Created_By { get; set; }
        public DateTime? Created_On { get; set; }
        public string Modified_By { get; set; }
        public DateTime? Modified_On { get; set; }
        public bool? Is_Active { get; set; }
        public bool? Is_Deleted { get; set; }
    }
}
