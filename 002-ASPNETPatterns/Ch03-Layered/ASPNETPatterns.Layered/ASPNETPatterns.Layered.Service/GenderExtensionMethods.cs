using ASPNETPatterns.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public static class GenderExtensionMethods
    {
        public static string GetDisplayName(this Gender gender)
        {
            if (gender == Gender.Male)
            {
                return "男";
            }
            else if (gender == Gender.Female)
            {
                return "女";
            }
            else
            {
                throw new Exception("Not exist: " + gender);
            }
        }
    }
}
