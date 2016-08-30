namespace IcerDesign.CCHelper
{
    using System;

    public class ClearCommand : CommandBase
    {
        public ClearCommand(string command, string workingDirectory, Action<Exception> exceptionRaised = null)
            : base(command, workingDirectory, exceptionRaised)
        {
        }

        public override string BatchCommand => $@"cd /d {this.WorkingDirectory}
cleartool {this.Command}";
    }
}