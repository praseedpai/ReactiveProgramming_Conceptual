using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Navigator
{
   public static  class Extensions
    {

        public static DirectoryNode GetAllFiles(string root)
        {
           return (DirectoryNode) new DirectoryInfo2(root).Traverse();

        }

        public static List <String> FlattenPath(this DirectoryNode en)
        {
            FlattenVisitor fl = new FlattenVisitor();
            en.Accept(fl);
            return fl.GetAllFiles();

        }

        public static void PrintAll( this List<string> files)
        {

            foreach(string s in files)
            {
                Console.WriteLine(s);
            }
        }
      

           
    }
}
