namespace IcerDesign.CCHelper
{
    using System.Diagnostics;

    internal enum ElementType
    {
        File,
        Directory,
    }

    internal enum CompareResult
    {
        Update,
        Create,
        Delete,
    }

    [DebuggerDisplay("{Result} {Type}: {Path, nq}")]
    internal class ElementCompareResult
    {
        public ElementCompareResult(string path, string srcpath, string destpath, CompareResult result, ElementType type)
        {
            this.Result = result;
            this.SourcePath = srcpath;
            this.DestPath = destpath;
            this.Path = path;
            this.Type = type;
        }

        public string Path { get; set; }
        public string SourcePath { get; set; }
        public string DestPath { get; set; }
        public CompareResult Result { get; set; }
        public ElementType Type { get; set; }
    }
}