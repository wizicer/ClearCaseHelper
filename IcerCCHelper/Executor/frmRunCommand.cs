namespace IcerDesign.CCHelper
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class frmRunCommand : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;

        public frmRunCommand()
        {
            this.InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public static bool RunClearCommand(CommandBase[] commands, bool closeWhenFinished = false, bool stopWhenError = true)
        {
            var frm = new frmRunCommand();
            frm.Show();
            frm.BringToFront();
            frm.Focus();
            Application.DoEvents();
            frm.prgbarCommands.Maximum = commands.Length;
            Action<int, string> addStatus = (prg, status) =>
                {
                    Application.DoEvents();
                    frm.prgbarCommands.Value = prg;
                    frm.txtStatus.Text = status
                        + Environment.NewLine
                        + frm.txtStatus.Text.Substring(0, frm.txtStatus.Text.Length > 1000 ? 1000 : frm.txtStatus.Text.Length);
                };

            for (int i = 0; i < commands.Length; i++)
            {
                var command = commands[i];
                addStatus(i, $"Running {command.GetType().Name}[{command.Description}]...");
                try
                {
                    var ret = DAL.RunBatch(command.BatchCommand);
                    addStatus(i, string.Format("Success: {0}", ret));
                }
                catch (Exception outterex)
                {
                    try
                    {
                        var handler = command.ExceptionRaised;
                        if (handler != null)
                        {
                            handler(outterex);
                        }
                        else
                        {
                            throw;
                        }

                        continue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Type manager \"text_file_delta\" failed create_version operation."))
                        {
                            log4net.LogManager.GetLogger("runcommand").InfoFormat("Identical file: {0}", command.Command);
                            // ignore identical file
                            continue;
                        }
                        log4net.LogManager.GetLogger("runcommand").Error(
                            string.Format("failed execute command[{0}] in [{1}]", command.Command, command.WorkingDirectory),
                            ex);
                        addStatus(i, string.Format("Failed: {0}", ex.Message));

                        var dlgRet = MessageBox.Show(
                            string.Format("Execute command[{0}] failed due to [{1}]", command.Command, ex.Message),
                            "Failed",
                            MessageBoxButtons.AbortRetryIgnore,
                            MessageBoxIcon.Error);

                        if (dlgRet == DialogResult.Abort)
                        {
                            frm.btnClose.Enabled = true;
                            return false;
                        }
                        else if (dlgRet == DialogResult.Retry)
                        {
                            i--;
                            continue;
                        }
                        else if (dlgRet == DialogResult.Ignore)
                        {
                            continue;
                        }
                        else
                        {
                            log4net.LogManager.GetLogger("runcommand").ErrorFormat("unexpected dialog result: {0}", dlgRet);
                        }
                    }
                }
            }

            addStatus(frm.prgbarCommands.Maximum, "All Success!");
            frm.btnClose.Enabled = true;

            if (closeWhenFinished)
            {
                frm.Close();
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}