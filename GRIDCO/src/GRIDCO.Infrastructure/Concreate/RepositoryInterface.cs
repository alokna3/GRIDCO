using GRIDCO.Core.Abstract;
using GRIDCO.Core.Model;
using GRIDCO.Core.ProjectAggregate.Entity;
using GRIDCO.Infrastructure.Data;
using GRIDCO.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;


namespace GRIDCO.Infrastructure.Concreate
{
   public class RepositoryInterface:IRepositoryInterface
    {
        public bool SendMail(string to, string subject, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("transactiondomain@gmail.com");
            mail.Subject = subject;
            string Body = body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("transactiondomain@gmail.com", "zaienkklwnmholof"); // Enter seders User name and password       
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return true;
        }

        AppDbContext con;
        public RepositoryInterface(AppDbContext obj)
        {
            this.con = obj;
        }
       public GRIDCO_User_Credential UserCredntialCheck(int userid, string username)
        {
            var data = con.GRIDCO_User_Credential.Where(x => x.User_Id == userid && x.User_Name == username).FirstOrDefault();
            return data;
        }
        public int ChangePassword(ChangePasswordModel changePassword)
        {
            var data = con.GRIDCO_User_Credential.Where(x => x.User_Id == changePassword.UserId && x.Is_Active == true && x.Is_Deleted == false).FirstOrDefault();
            data.Password = MainModel.EncodeBase64(changePassword.Newpassword);
            con.SaveChanges();
            return 1;
        }

        public int SaveRole(GRIDCO_Role_Mst RL)

        {


            if (RL.User_Type_Id != 0)
            {
                var CC = con.GRIDCO_Role_Mst.Where(x => x.User_Type_Id == RL.User_Type_Id).FirstOrDefault();
                CC.User_Type = RL.User_Type;

                CC.Modified_On = DateTime.Now;
                con.SaveChanges();
                return 2;
            }
            else
            {
                GRIDCO_Role_Mst R = new GRIDCO_Role_Mst();
                R.User_Type = RL.User_Type;

                R.Is_Active = true;
                R.Is_Deleted = false;

                con.GRIDCO_Role_Mst.Add(R);
                con.SaveChanges();
                return 1;
            }

        }

        public bool DeleteRole(int id)
        {
            var data = con.GRIDCO_Role_Mst.Where(x => x.User_Type_Id == id).FirstOrDefault();
            data.Is_Active = false;
            data.Is_Deleted = true;
            con.SaveChanges();
            return true;
        }
    }
}
