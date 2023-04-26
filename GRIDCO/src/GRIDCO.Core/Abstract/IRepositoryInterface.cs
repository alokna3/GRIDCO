using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRIDCO.Core.Model;
using GRIDCO.Core.ProjectAggregate.Entity;
using System;
using System.Collections.Generic;


namespace GRIDCO.Core.Abstract
{
  public  interface IRepositoryInterface
    {
        GRIDCO_User_Credential UserCredntialCheck(int userid, string username);



        int ChangePassword(ChangePasswordModel changePassword);

        int SaveRole(GRIDCO_Role_Mst DD);
        bool DeleteRole(int id);

    }
}
