using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

using System.IO;
using System.Net;
using System.Security.Cryptography;

using System.Threading.Tasks;

namespace GRIDCO.SharedKernel
{
  public  class MainModel
    {
        private WebProxy objProxy1 = null;

        public string SendSMS(string User, string password, string Mobile_Number, string Message)
        {
            string stringpost = null;
            stringpost = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message;

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;
                objWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice_bulk.aspx");
                objWebRequest.Method = "POST";
                if ((objProxy1 != null))
                {
                    objWebRequest.Proxy = objProxy1;
                }
                // Use below code if you want to SETUP PROXY.
                //Parameters to pass: 1. ProxyAddress 2. Port
                //You can find both the parameters in Connection settings of your internet explorer.

                //WebProxy myProxy = new WebProxy("YOUR PROXY", PROXPORT);
                //myProxy.BypassProxyOnLocal = true;
                //wrGETURL.Proxy = myProxy;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";
                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return stringResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }
        }
        public static string ReturnFileSize(string path)
        {
            string filepath = Directory.GetCurrentDirectory() + "/wwwroot" + path;
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)
            {
                decimal fileSize = fi.Length / 1024;
                if (fileSize > 1024)
                {
                    fileSize = fileSize / 1024;
                    fileSize = Math.Round(fileSize, 2);
                    return fileSize.ToString() + " mb";
                }
                else
                {
                    fileSize = Math.Round(fileSize, 2);
                    return fileSize.ToString() + " kb";
                }
            }
            else { return ""; }
        }
        public static string ReturnFileType(string path)
        {
            string filepath = Directory.GetCurrentDirectory() + "/wwwroot" + path;
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)
            {
                var filename = fi.Extension;
                return filename.Replace(".", "");
            }
            else { return ""; }
        }
        public static string GenerateFileName(string fileextenstion)
        {
            if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
            return $"{Guid.NewGuid():N}_{DateTime.Now:yyyyMMddHHmmssfff}{fileextenstion}";
        }
        public static string FileUpload(IFormFile pdf_attachment, string fileextenstion, string folderName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", folderName);
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            var filepath = new PhysicalFileProvider(path).Root;
            var attachileName = GenerateFileName(fileextenstion);
            var filepath_Attach = filepath + $@"\{attachileName}";
            var stream = new FileStream(filepath_Attach, FileMode.Create);
            pdf_attachment.CopyTo(stream);
            return "/Upload/" + folderName + "/" + attachileName;
        }
        public static int CreateOtp()
        {
            int min = 100000;
            int max = 999999;
            int otp = 0;
            Random rdm = new Random();
            otp = rdm.Next(min, max);
            return otp;
        }

        public class Encrypt_Password
        {
            public static string HashPassword(string pasword)
            {
                byte[] arrbyte = Encoding.UTF8.GetBytes(string.Concat(pasword, "#s$"));
                SHA256 hash = new SHA256CryptoServiceProvider();
                arrbyte = hash.ComputeHash(arrbyte);
                return Convert.ToBase64String(arrbyte);
            }
        }

        public static DateTime datetoserver()
        {
            string zoneId = "India Standard Time";
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime result = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            return result;
        }

        public static DateTime convertdateformat(string date)
        {
            try
            {
                string[] a = date.Split('/');
                string day = a[0];
                string month = a[1];
                string year = a[2];
                string fulldate = year + "-" + month + "-" + day;
                DateTime data = Convert.ToDateTime(fulldate);
                return data;
            }
            catch (Exception)
            {
                try
                {
                    string[] a = date.Split('-');
                    string day = a[0];
                    string month = a[1];
                    string year = a[2];
                    string fulldate = year + "-" + month + "-" + day;
                    DateTime data = Convert.ToDateTime(fulldate);
                    return data;
                }
                catch (Exception)
                {
                    try
                    {
                        string[] a = date.Split('-');
                        string day = a[0];
                        string month = a[1];
                        string year = a[2];
                        string fulldate = year + "-" + month + "-" + day;
                        DateTime data = Convert.ToDateTime(fulldate);
                        return data;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            string[] a = date.Split('-');
                            string day = a[0];
                            string month = a[1];
                            string year = a[2];
                            string fulldate = year + "-" + month + "-" + day;
                            DateTime data = Convert.ToDateTime(fulldate);
                            return data;
                        }
                        catch (Exception ex) { throw ex; }

                    }

                }
            }
        }

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string ReturnStrEncryptCode(string id)
        {
            string msg = string.Empty;
            try
            {
                msg = Encrypt(id, "sblw-3hn9-sqoy59");
            }
            catch (Exception)
            {

            }
            return msg;
        }

        public static string ReturnStrDecryptCode(string id)
        {
            string msg = string.Empty;
            try
            {
                string ids = id.Replace(" ", "+");
                msg = Decrypt(ids, "sblw-3hn9-sqoy59");
            }
            catch (Exception)
            {

            }
            return msg;
        }

        //------------------------------------------------------------------Base64 Encode & Decode-------------------------------------------------------------------------------------//
        public static string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}
