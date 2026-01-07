using Core.Models;
namespace Core
{
    public class CommonMethods
    {
        public static TokenAuthModel TokenAuth(string Token)
        {
            TokenAuthModel objData = new TokenAuthModel() { Token = Token };
            try
            {
                string[] arrObj = Core.Common.Decrypt(Token).Split('|');
                if (arrObj.Length == 11)
                {
                    objData.Status = true;
                    objData.Message = "";
                    objData.UserId = Convert.ToInt32(arrObj[0]);
                    objData.RoleId = Convert.ToInt32(arrObj[1]);
                    objData.LoginOn = arrObj[9];
                    objData.IPAddress = arrObj[10];
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


        public static LoginUserDataModel GetLoginUserDataModelFromToken(string Token)
        {
            string[] arrObj = Core.Common.Decrypt(Token).Split('|');
            LoginUserDataModel objData = new()
            {
                UserId = Convert.ToInt32(arrObj[0]),
                RoleId = Convert.ToInt32(arrObj[1]),
                DepartmentId = Convert.ToInt32(arrObj[2]),
                UnitId = Convert.ToInt32(arrObj[3]),
                OfficeId = Convert.ToInt32(arrObj[4]),
                DistrictId = Convert.ToInt32(arrObj[5]),
                SSOID = Convert.ToString(arrObj[6]),
                LoginOn = Convert.ToString(arrObj[7]),
                IPAddress = Convert.ToString(arrObj[8])
            };
            return objData;
        }
    }
}
