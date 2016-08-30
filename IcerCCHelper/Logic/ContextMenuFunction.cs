namespace IcerDesign.CCHelper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    internal class ContextMenuFunction
    {
        private static log4net.ILog mainLog = log4net.LogManager.GetLogger("main");

        internal static void AddToSourceControl(string[] paths)
        {
            var commands = new List<CommandBase>();
            var eles = new List<string>();
            var totalNumber = 0;

            try
            {
                foreach (var path in paths)
                {
                    string[] elements;
                    int pathNumber;
                    commands.AddRange(ClearCommands.AddToSourceControl(path, out elements, out pathNumber));
                    if (eles.Count < 20) eles.AddRange(elements.Take(20 - eles.Count));
                    totalNumber += pathNumber;
                }

                var msg = string.Format(
                    "Are you sure to check in following elements:\r\n{0}\r\n{1}\r\nTotal: {2} element(s)",
                    string.Join(Environment.NewLine, eles),
                    totalNumber > eles.Count ? "...\r\n" : "",
                    totalNumber);
                var diagRet = MessageBox.Show(msg, "Add element(s)", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (diagRet != DialogResult.OK) return;

                frmRunCommand.RunClearCommand(commands.ToArray());
            }
            catch (FileNotFoundException fex)
            {
                MessageBox.Show($"element [{fex.FileName}] not exist!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                mainLog.Error("error when add to source control", ex);
            }
        }

        internal static void RemoveFromSourceControl(string[] paths)
        {
            var commands = new List<CommandBase>();
            foreach (var path in paths)
            {
                commands.AddRange(ClearCommands.RemoveFromSourceControl(path));
            }

            frmRunCommand.RunClearCommand(commands.ToArray());
        }
    }
}