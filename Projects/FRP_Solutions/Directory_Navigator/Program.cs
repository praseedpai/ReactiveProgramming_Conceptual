using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Navigator
{
    class Program
    {
        public static void DumpDirectory(DirectoryNode ds)
        {
            Console.WriteLine("Directory ->" + ds.getname());
            List<EntryNode> list = ds.GetAllFiles();
            IEnumerator<EntryNode> itr = list.GetEnumerator();
            while (itr.MoveNext())
            {
                EntryNode nt = (EntryNode)itr.Current;
                if (nt.Isdir())
                    DumpDirectory((DirectoryNode)nt);
                else
                    Console.WriteLine("    File -> " + ((FileNode)nt).getname() + "Size ->" + ((FileNode)nt).getSize());
            }




        }
        static void Main(string[] args)
        {
#if false
            DirectoryInfo2 dn = new DirectoryInfo2("D:\\PRASEEDPAI\\BOOKS\\advaita");
            EntryNode en = dn.Traverse();

            DumpDirectory((DirectoryNode)en);

            Console.WriteLine();

#endif
            Extensions.GetAllFiles("D:\\PRASEEDPAI\\BOOKS\\advaita").FlattenPath().PrintAll();
            Console.ReadLine();
        }
    }
}
