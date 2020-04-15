using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Navigator
{
    /// <summary>
    /// 
    /// </summary>
   public abstract class EntryNode
    {
        protected String name;
        protected int isdir;
        protected long size;
        public abstract bool Isdir();
        public abstract long getSize();
        public abstract void Accept(IFileFolderVisitor ivis);

    }
    /// <summary>
    ///   A Visitor Interface for File/Folder
    /// </summary>
    public interface IFileFolderVisitor
    {
        void Visit(FileNode fn);
        void Visit(DirectoryNode dn);

    }
    /// <summary>
    ///   File Node class
    /// </summary>
   public class FileNode : EntryNode
    {

        public override long getSize()
        {
            return base.size;
        }
        public FileNode(String pname,long fsize)
        {
            isdir = 0; name = pname; size = fsize;
        }
        public override bool Isdir() { return isdir == 1; }
        public String getname() { return name; }
        public override void Accept(IFileFolderVisitor ivis)
        {
            ivis.Visit(this);
        }

    }

    public class DirectoryNode : EntryNode
    {
        List<EntryNode> files = new List<EntryNode>();
        public DirectoryNode(String pname)
        {
            isdir = 1; name = pname;
            //files.add(new DirectoryNode(pname));
        }
       public  List<EntryNode> GetAllFiles()
        {
            return files;
        }
        public bool AddFile(String pname,long fsize)
        {
            files.Add(new FileNode(pname,fsize));
            return true;
        }

        public bool AddDirectory(DirectoryNode dn)
        {
            files.Add(dn);
            return true;
        }
        public override long getSize()
        {
            return -1;
        }
        public override bool Isdir() { return isdir == 1; }
        public String getname() { return name; }
        public void setname(String pname) { name = pname; }
        public override void Accept(IFileFolderVisitor ivis)
        {
            ivis.Visit(this);
        }
    }


    class DirectoryInfo2
    {
        DirectoryNode root = null;
        DirectoryNode addptr = null;

        public DirectoryInfo2(String rootdir)
        {

            root = new DirectoryNode(rootdir);

        }

        public EntryNode Traverse()
        {

            EntryNode ns = null;

            DirectoryInfo dir = new DirectoryInfo(root.getname());

       
         
                ns = WalkDirectoryTree(dir);

            return ns;

        }

        public static EntryNode  WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                return null;
                
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            if (files != null)
            {
                DirectoryNode ds = new DirectoryNode(root.FullName);
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-ca tch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                   
                    ds.AddFile(fi.Name,fi.Length);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                   DirectoryNode dn =  (DirectoryNode)WalkDirectoryTree(dirInfo);
                    ds.AddDirectory(dn);
                }
                return ds;
            }
            return null;
        }
    }
}

