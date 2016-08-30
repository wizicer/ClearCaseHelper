namespace IcerDesign.CCHelper
{
    using System;

    public abstract class CommandBase
    {
        protected CommandBase(string command, string workingDirectory, Action<Exception> exceptionRaised = null)
        {
            this.Command = command;
            this.WorkingDirectory = workingDirectory;
            this.ExceptionRaised = exceptionRaised;
        }

        public string Command { get; set; }

        public string WorkingDirectory { get; set; }

        public Action<Exception> ExceptionRaised { get; set; }

        public virtual string Description => this.Command;

        public abstract string BatchCommand { get; }
    }
}