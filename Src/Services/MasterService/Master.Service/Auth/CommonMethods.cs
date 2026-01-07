using Master.Dto.Shared;
using System.Runtime.CompilerServices;

namespace Master.Service.Auth
{
    public class CommonMethods
    {
        public static TokenAuthModel TokenAuth(string Token)
        {
            TokenAuthModel objData = new TokenAuthModel() { Token = Token };
            try
            {
                string[] arrObj = Core.Common.Decrypt(Token).Split('|');
                if (arrObj.Length == 4)
                {
                    objData.Status = true;
                    objData.Message = "";
                    objData.UserId = Convert.ToInt32(arrObj[0]);
                    objData.RoleId = Convert.ToInt32(arrObj[1]);
                    objData.LoginOn = arrObj[2];
                    objData.IPAddress = arrObj[3];
                }
                else
                {
                    objData.Status = true;
                    objData.Message = "Invalid token.";
                }

            }
            catch (Exception ex)
            {
                objData.Status = true;
                objData.Message = "Invalid token.";
            }
            return objData;
        }
    }
}
