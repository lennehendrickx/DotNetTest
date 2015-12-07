using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTest
{
    public partial class Employee
    {
        public void DoWork()
        {
            // do some work
            GoHome(); // GoHome can be implemented in another Employee partial class. If not this call to GoHome will be removed at compile time;
        }

        partial void GoHome();
    }

    public partial class Employee
    {
        public void GoToLunch()
        {
        }

        partial void GoHome()
        {
            // go home
        }
    }
}
