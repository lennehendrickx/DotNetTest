using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTest
{
    public class IndexerExample<T>
    {
        private readonly T[] _arr = new T[100];

        public T this[int i]
        {
            get
            {
                return _arr[i];
            }
            set
            {
                _arr[i] = value;
            }
        }
    }
}
