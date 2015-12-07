using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTest
{
        public struct TestStruct
        {
            private string _aProperty;
            public string AnotherProperty;

            public string AProperty
            {
                get { return _aProperty; }
                set { _aProperty = value; }
            }

            public void AMethod()
            {
                // I do nothing
            }
        }
}
