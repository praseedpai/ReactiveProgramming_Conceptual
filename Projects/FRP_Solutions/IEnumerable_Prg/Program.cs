using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerable_Prg
{

    class EvenNumberEnumerable: IEnumerable<int>
    {
        public EvenNumberEnumerable(IEnumerable<int> numbers)
        {
            this._numbers = numbers;
        }

        private IEnumerable<int> _numbers;

        public IEnumerator<int> GetEnumerator()
        {
            return _numbers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            EvenNumberEnumerable evn = new EvenNumberEnumerable(
              new[] { 1, 2, 3, 4, 6, 7, 8 });

            IEnumerator<int> s = evn.GetEnumerator();
            while (s.MoveNext())
            {
                Console.WriteLine(s.Current);
            }
            
            Console.ReadLine();

        }
    }
}
