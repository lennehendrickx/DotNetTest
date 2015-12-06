using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTest
{
    public class LanguageFeature
    {
        public List<int> ParamsToList(params int[] integers)
        {
            return integers.ToList();
        }

    }
}
