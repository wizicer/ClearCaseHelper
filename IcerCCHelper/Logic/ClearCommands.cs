namespace IcerDesign.CCHelper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class ClearCommands
    {
        public static ClearCommand[] CheckInFiles(string comment, string[] files)
        {
            var lst = files
                .Select(s => String.Format(@"checkin -ide -c ""{1}"" ""{0}""", s, comment))
                .Select(s => new ClearCommand(s, new ElementPath(s).Parent))
                .ToArray();

            return lst;
        }

        public static ClearCommand[] ApplyLabelToFiles(string label, string[] files)
        {
            var lst = files
                .Select(s => String.Format(@"mklabel -replace {1} ""{0}""", s, label))
                //.Select(s => string.Format(@"reqmaster {0}@@/main&cleartool mklabel -replace {1} {0}", s, label))
                .Select(s => new ClearCommand(s, new ElementPath(s).Parent))
                .ToArray();

            return lst;
        }

        public static ClearCommand[] UndoCheckOut(string[] files)
        {
            var lst = files
                .Select(s => String.Format(@"unco -rm ""{0}""", s))
                .Select(s => new ClearCommand(s, new ElementPath(s).Parent))
                .ToArray();

            return lst;
        }

        public static ClearCommand[] CheckOutFile(string comment, string filename)
        {
            var ret = new ElementPath(filename);
            var cmds = new List<ClearCommand>();
            cmds.Add(new ClearCommand("reqmaster " + filename + "@@/main", ret.Parent, (ex) =>
            {
                if (ex.Message.IndexOf("The object is already mastered by replica") == -1) throw ex;
            }));

            cmds.Add(new ClearCommand("co -c \"" + comment + @""" " + filename, ret.Parent, (ex) =>
            {
                if (ex.Message.IndexOf("is already checked out to view") == -1) throw ex;
            }));

            return cmds.ToArray();
        }

        public static ClearCommand[] AddToSourceControl(string path, out string[] elements, out int totalNumber, int elementLimit = 10)
        {
            var ret = new ElementPath(path);
            var parent = ret.Parent;
            var element = ret.Element;
            var commands = new List<ClearCommand>();
            var eles = new List<string>();
            totalNumber = 0;

            if (File.Exists(path))
            {
                commands.AddRange(CheckOutFile("", parent));
                commands.Add(new ClearCommand("mkelem -master -nc " + element, parent));
                eles.Add(path);
                totalNumber++;
            }
            else if (Directory.Exists(path))
            {
                commands.AddRange(CheckOutFile("", parent));
                var directories = new[] { path }.Concat(Directory.GetDirectories(path, "*", SearchOption.AllDirectories));
                foreach (var dir in directories)
                {
                    var di = new DirectoryInfo(dir);
                    commands.Add(new ClearCommand(
                        string.Format("mkelem -mkpath -master -nc \"{0}\"", di.Name),
                        di.Parent.FullName));
                    if (eles.Count < elementLimit) eles.Add(Path.Combine(di.Parent.FullName, di.Name));
                    totalNumber++;
                    var files = Directory.GetFiles(dir, "*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        commands.Add(new ClearCommand(
                            string.Format("mkelem -mkpath -master -nc \"{0}\"", file),
                            dir));
                        if (eles.Count < elementLimit) eles.Add(file);
                        totalNumber++;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("file or directory cannot find", path);
            }

            elements = eles.ToArray();
            return commands.ToArray();
        }

        public static ClearCommand[] RemoveFromSourceControl(string path)
        {
            var commands = new List<ClearCommand>();
            var ret = new ElementPath(path);
            commands.AddRange(CheckOutFile("", ret.Parent));
            commands.Add(new ClearCommand(string.Format("rmname \"{0}\"", ret.Element), ret.Parent, (ex) =>
            {
                if (ex.Message.IndexOf("Not a vob object") == -1) throw ex;
            }));
            return commands.ToArray();
        }

        private class ElementPath
        {
            public ElementPath(string path)
            {
                if (File.Exists(path))
                {
                    var fi = new FileInfo(path);
                    this.Parent = fi.DirectoryName;
                    this.Element = fi.Name;
                }
                else if (Directory.Exists(path))
                {
                    var di = new DirectoryInfo(path);
                    this.Parent = di.Parent.FullName;
                    this.Element = di.Name;
                }
            }

            public ElementPath(string parent, string element)
            {
                this.Parent = parent;
                this.Element = element;
            }

            public string Parent { get; }
            public string Element { get; }
        }
    }
}