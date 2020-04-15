using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Navigator
{
    class PrintFolderVisitor : IFileFolderVisitor
    {

         public void Visit(FileNode fn)
         {
             Console.WriteLine("  File -> " + fn.getname());
         }
         public void Visit(DirectoryNode dn)
         {
            Console.WriteLine("Directory -> " + dn.getname());

            //Traversing list through Iterator 
            List<EntryNode> list = dn.GetAllFiles();
             IEnumerator<EntryNode> itr = list.GetEnumerator();
             while (itr.MoveNext())
             {
                 EntryNode nt = (EntryNode)itr.Current;
                 if (nt.Isdir())
                     Visit((DirectoryNode)nt);
                 else
                ((FileNode)nt).Accept(this);
             }
         }

}


class FlattenVisitor : IFileFolderVisitor
{
    List<String> files = new List<String>();
    String CurrDir = "";
    public FlattenVisitor(){

         files = new List<String>();
    }

    public List<String> GetAllFiles()
    {
         return files;
    }

    public void Visit(FileNode fn){
         files.Add(CurrDir + "\\" + fn.getname());
    }
    public void Visit(DirectoryNode dn)
    {
        // System.out.println("Directory -> " + dn.getname() );
         CurrDir = dn.getname();
        //Traversing list through Iterator 
        List<EntryNode> list = dn.GetAllFiles();
        IEnumerator<EntryNode> itr = list.GetEnumerator();
         while (itr.MoveNext())
         {
                EntryNode nt = (EntryNode)itr.Current;
             if (nt.Isdir())
        {
            CurrDir = ((DirectoryNode)nt).getname();
            ((DirectoryNode)nt).Accept(this);
        }
        else
            ((FileNode)nt).Accept(this);
    }
}
 
}
}
