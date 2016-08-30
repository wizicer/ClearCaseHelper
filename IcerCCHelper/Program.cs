namespace IcerDesign.CCHelper
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    internal static class Program
    {
        private static log4net.ILog mainLog = log4net.LogManager.GetLogger("main");

        private static string errorMsg = "Sorry, something unexpected happened, please report to Icer[E585909] with log.xml file\r\n Thanks!\r\n\r\nBest Regards\r\nIcer";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                return;
            }
            else if (args.Length == 1)
            {
                if (args[0] == "viewcs")
                {
                    // TODO: need to reimplement to remove this dependency, as it should read drive and vob from clearcase parameter
                    ////MessageBox.Show(BLL.GetConfigSpecOfView());
                    return;
                }
            }
            else if (args.Length >= 2)
            {
                var filelist = args.Skip(1).ToArray();
                if (args[0] == "remove")
                {
                    ContextMenuFunction.RemoveFromSourceControl(filelist);
                    return;
                }
                if (args[0] == "add")
                {
                    ContextMenuFunction.AddToSourceControl(filelist);
                    return;
                }
            }
            MessageBox.Show("Wrong arguments: " + string.Join("|", args));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(errorMsg, "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
            {
                var ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    mainLog.Fatal("unhandled exception", ex);
                }
                else
                {
                    mainLog.Fatal("unhandled exception, error exception object is null");
                }
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(errorMsg, "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
            {
                mainLog.Fatal("unhandled exception", e.Exception);
            }
        }
    }
}