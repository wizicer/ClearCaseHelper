namespace IcerDesign.CCHelper
{
    using System;

    public class ShellCommand : CommandBase
    {
        public ShellCommand(string command, string workingDirectory, Action<Exception> exceptionRaised = null)
            : base(command, workingDirectory, exceptionRaised)
        {
        }

        public override string BatchCommand => this.WorkingDirectory == null ? this.Command : $@"cd /d {this.WorkingDirectory}
{this.Command}";
    }
}