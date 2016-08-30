namespace IcerDesign.CCHelper
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Microsoft.Win32;
    using Properties;

    public class Installer
    {
        private static readonly int ExtensionVersion = 2;

        public enum InstallStatus
        {
            None,
            Newest,
            Outdated,
            Future,
        }

        public static void Install(string executablePath)
        {
            var s = Resources.install;
            s = s.Replace("{$executablepath}", executablePath.Replace(@"\", @"\\"))
                .Replace("{$extensionversion}", ExtensionVersion.ToString());
            MergeRegFile(s);

            // resolved the manual work issue by refer to http://www-01.ibm.com/support/docview.wss?uid=swg21245921
        }

        public static void Uninstall()
        {
            var s = Resources.uninstall;
            MergeRegFile(s);
        }

        public static InstallStatus GetInstallStatus()
        {
            var verstring = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Atria\ClearCase\CurrentVersion\ContextMenus",
                "IcerCCHelperExtensionVer",
                null) as string;
            if (string.IsNullOrEmpty(verstring))
            {
                return InstallStatus.None;
            }

            int ver;
            if (!int.TryParse(verstring, out ver))
            {
                return InstallStatus.None;
            }

            if (ver == ExtensionVersion)
            {
                return InstallStatus.Newest;
            }

            if (ver > ExtensionVersion)
            {
                return InstallStatus.Future;
            }

            return InstallStatus.Outdated;
        }

        private static void MergeRegFile(string content)
        {
            var filename = Path.Combine(Path.GetTempPath(), "reg.reg");
            File.WriteAllText(filename, content);

            var regeditProcess = Process.Start("regedit.exe", "/s " + filename);
            regeditProcess.WaitForExit();

            File.Delete(filename);
        }
    }
}