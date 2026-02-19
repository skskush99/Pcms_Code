using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.Global
{
    public static class ExtensionMethod
    {
        public static int TableSkipRecord(this int value, int? CurrentPageSize)
        {
            if (CurrentPageSize.HasValue)
            {
                value = value == 0 ? 0 : (value - 1) * (CurrentPageSize.Value);
            }
            return value;
        }

        public static string EncryptID(this int id)
        {
            string EncryptedID = string.Empty;
            try
            {
                EncryptedID = new Cryptology().EncryptID(id);

            }
            catch
            {

            }
            return EncryptedID;
        }

        public static int DecryptID(this String id)
        {
            int DecryptID = 0;
            try
            {
                DecryptID = new Cryptology().DecryptID(id);

            }
            catch
            {

            }
            return DecryptID;
        }
    }
}
