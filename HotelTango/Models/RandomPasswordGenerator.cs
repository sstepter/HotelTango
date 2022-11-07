using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RandomPasswordGenerator
{

    public static string Random(int length)
    {
        try
        {
            byte[] result = new byte[length];
            for (int index = 0; index < length; index++)
            {
                result[index] = (byte)new Random().Next(33, 126);
            }
            return System.Text.Encoding.ASCII.GetString(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

}
