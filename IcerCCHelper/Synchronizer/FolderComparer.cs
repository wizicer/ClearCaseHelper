namespace IcerDesign.CCHelper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    internal class FolderComparer
    {
        private IFileSystem fileSystem;

        public FolderComparer(IFileSystem fs)
        {
            this.fileSystem = fs;
        }

        public FolderComparer()
            : this(new FileSystem())
        {
        }

        internal async Task<ElementCompareResult[]> CompareAsync(string source, FilterRule[] srcRules,
            string destination, FilterRule[] dstRules, IProgress<WorkProgress> progress)
        {
            if (!source.EndsWith("\\")) source += "\\";
            if (!destination.EndsWith("\\")) destination += "\\";
            var srcdir = this.fileSystem.DirectoryInfo.FromDirectoryName(source);
            var dstdir = this.fileSystem.DirectoryInfo.FromDirectoryName(destination);

            var srctask = IterateDirectoriesAsync(srcdir.FullName, srcRules, progress);
            var dsttask = IterateDirectoriesAsync(dstdir.FullName, dstRules, progress);
            await Task.WhenAll(srctask, dsttask);

            var srcfiles = srctask.Result;
            var dstfiles = dsttask.Result;

            var srcfilesstr = srcfiles
                .Select(_ => _.FullName.Remove(0, srcdir.FullName.Length))
                .ToArray();
            var dstfilesstr = dstfiles
                .Select(_ => _.FullName.Remove(0, dstdir.FullName.Length))
                .ToArray();
            progress.Report(new WorkProgress("files retrieved."));

            return await CompareAsync(srcdir.FullName, srcfilesstr, dstdir.FullName, dstfilesstr, progress);
        }

        private async Task<FileSystemInfoBase[]> IterateDirectoriesAsync(string root, FilterRule[] rules,
            IProgress<WorkProgress> progress)
        {
            return await IterateDirectoriesAsync(root, root, rules, progress);
        }

        private async Task<FileSystemInfoBase[]> IterateDirectoriesAsync(string root, string current, FilterRule[] rules,
            IProgress<WorkProgress> progress)
        {
            var list = new List<FileSystemInfoBase>();
            foreach (var dir in await Task.Run(() => this.fileSystem.Directory.EnumerateDirectories(current)))
            {
                var path = dir;
                if (!path.EndsWith("\\")) path += "\\";
                if (rules.MatchExclude(path, root)) continue;

                list.Add(this.fileSystem.DirectoryInfo.FromDirectoryName(dir));
                progress?.Report(new WorkProgress(dir));
                list.AddRange(await IterateDirectoriesAsync(root, dir, rules, progress));
            }

            foreach (var file in await Task.Run(() => this.fileSystem.Directory.EnumerateFiles(current, "*", SearchOption.TopDirectoryOnly)))
            {
                if (rules.MatchExclude(file, root)) continue;
                list.Add(this.fileSystem.FileInfo.FromFileName(file));
            }

            return list.ToArray();
        }

        private async Task<ElementCompareResult[]> CompareAsync(string srcDir, string[] sourceFiles, string destDir,
            string[] destFiles, IProgress<WorkProgress> progress)
        {
            var result = new List<ElementCompareResult>();
            for (int i = 0; i < sourceFiles.Length; i++)
            {
                var srcfile = sourceFiles[i];
                var originsrcfile = Path.Combine(srcDir, srcfile);
                var r = CompareResult.Create;
                var destfile = destFiles.FirstOrDefault(_ => _.ToLower() == srcfile.ToLower());
                var origindestfile = destfile == null ? Path.Combine(destDir, srcfile) : Path.Combine(destDir, destfile);
                var eletype = File.Exists(originsrcfile) ? ElementType.File : ElementType.Directory;
                if (destfile != null)
                {
                    if (eletype == ElementType.Directory)
                    {
                        continue;
                    }

                    progress?.Report(new WorkProgress($"comparing {srcfile}...", i, sourceFiles.Length));
                    if (!await Task.Run(() => CompareContent(originsrcfile, origindestfile)))
                    {
                        r = CompareResult.Update;
                    }
                    else
                    {
                        continue;
                    }
                }


                if (destfile == null)
                {
                    if (r == CompareResult.Create)
                    {
                        // fix file path casing as ClearCase is case sensitive
                        origindestfile = FixFilePathCasing(origindestfile);
                        destfile = origindestfile.Replace(destDir, "");
                    }
                }

                result.Add(new ElementCompareResult(destfile, originsrcfile, origindestfile, r, eletype));
            }

            progress?.Report(new WorkProgress($"finding deleted files..."));
            var deletedResult = new List<ElementCompareResult>();
            foreach (var destfile in destFiles)
            {
                var origindestfile = Path.Combine(destDir, destfile);
                var srcfile = sourceFiles.FirstOrDefault(_ => _.ToLower() == destfile.ToLower());
                var originsrcfile = srcfile == null ? null : Path.Combine(srcDir, srcfile);
                var eletype = await Task.Run(() => File.Exists(origindestfile)) ? ElementType.File : ElementType.Directory;

                if (srcfile == null && !deletedResult.Where(_ => _.Type == ElementType.Directory).Any(_ => destfile.StartsWith(_.Path)))
                {
                    deletedResult.Add(new ElementCompareResult(destfile, originsrcfile, origindestfile, CompareResult.Delete, eletype));
                }
            }

            result.AddRange(deletedResult);

            return result.ToArray();
        }

        private string FixFilePathCasing(string filePath)
        {
            var fullFilePath = Path.GetFullPath(filePath);

            string fixedPath = "";
            foreach (var token in fullFilePath.Split('\\'))
            {
                //first token should be drive token
                if (fixedPath == "")
                {
                    fixedPath = string.Concat(token, "\\");
                }
                else
                {
                    string item = null;
                    try { item = Directory.GetFileSystemEntries(fixedPath, token).FirstOrDefault(); }
                    catch (DirectoryNotFoundException) { }
                    fixedPath = item ?? Path.Combine(fixedPath, token);
                }
            }

            return fixedPath;
        }

        private bool CompareContent(string srcfile, string destfile)
        {
            var srccontent = this.fileSystem.File.ReadAllText(srcfile);
            var destcontent = this.fileSystem.File.ReadAllText(destfile);

            Func<string, string> filter = (_) => _.Replace("\r", "");
            srccontent = filter(srccontent);
            destcontent = filter(destcontent);

            return srccontent.Equals(destcontent, StringComparison.Ordinal);
        }
    }
}