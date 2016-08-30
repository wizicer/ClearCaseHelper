namespace IcerDesign.CCHelper
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class BLL
    {
        private static log4net.ILog Log = log4net.LogManager.GetLogger("BLL");

        private static string findExecutablePath = @"C:\Program Files (x86)\IBM\RationalSDLC\ClearCase\bin\clearfindco.exe";
        private static string mergeManagerExecutablePath = @"C:\Program Files (x86)\IBM\RationalSDLC\ClearCase\bin\clearmrgman.exe";

        public static string[] GetCheckedOutFiles(this LocationInfo location)
        {
            // indeed we don't need `-avobs` option, however it would not work without this option, so we should filter other files out after this command
            var ls = DAL.ClearTool("lscheckout -avobs -me -s -cvi", location.BasePath)
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.StartsWith("\\" + location.VOBName)) // filter only in this vob
                .Select(_ => $"{location.Volume}:{_}")
                .ToArray();
            return ls;
        }

        public static string[] GetCheckedOutChangedFiles(this LocationInfo location)
        {
            // output example:
            // >>> file 2: \vob\path\to\solution.sln
            try
            {
                // indeed we don't need `-avobs` option, however it would not work without this option, so we should filter other files out after this command
                var ls = DAL.ClearTool(@"lscheckout -avobs -me -cvi -fmt ""diff -options \""-hea\"" -pred \""%%n\""\n""|cleartool|findstr "">>>""", location.BasePath)
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Last().Trim())
                    .Where(s => s.StartsWith("\\" + location.VOBName)) // filter only in this vob
                    .ToArray();
                return ls;
            }
            catch (Exception ex)
            {
                Log.Error("error happened in get checked out changed file", ex);
                return new string[] { };
            }
        }

        public static string GetConfigSpecOfView(this LocationInfo location)
        {
            return location.Volume.GetConfigSpecOfView();
        }

        public static string GetConfigSpecOfView(this DriverVolume driver)
        {
            return driver.ClearTool("catcs");
        }

        public static string GetConfigSpecOfFile(this LocationInfo location, string configSpecFilePath)
        {
            var cs = File.ReadAllText(Path.Combine(location.BasePath, configSpecFilePath));
            return cs.Replace("\r\r\n", "\r\n"); // somebody would submit configspec with this special return line
        }

        public static void CreateLabel(this LocationInfo location, string comment, string label)
        {
            try
            {
                location.ClearTool(String.Format("mklbtype -c \"{0}\" {1}", comment, label));
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("exist") == -1) throw;
            }
        }

        public static void UndoCheckOutFile(this LocationInfo location, string filename)
        {
            location.ClearTool(@"unco -rm " + filename);
        }

        public static void CheckInFile(this LocationInfo location, string comment, string filename)
        {
            location.ClearTool("ci -c \"" + comment + @""" " + filename);
        }

        public static void CheckOutFile(this LocationInfo location, string comment, string filename)
        {
            try
            {
                location.ClearTool("reqmaster " + filename + "@@/main");
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("The object is already mastered by replica") == -1) throw;
            }

            location.ClearTool("co -c \"" + comment + @""" " + filename);
        }

        public static void SetConfigSpec(this LocationInfo location, string content)
        {
            var filename = Path.GetTempFileName();
            File.WriteAllText(filename, content);
            location.ClearTool(@"setcs " + filename);
            File.Delete(filename);
        }

        public static bool IsBranchExist(this LocationInfo location, string branch)
        {
            try
            {
                var v = location.ClearTool("lstype brtype:" + branch);
                return true;
            }
            catch (Exception ex)
            {
                Log.Warn("failed to find the branch", ex);
            }

            return false;
        }

        public static void CreateBranch(this LocationInfo location, string branch)
        {
            location.ClearTool("mkbrtype -nc " + branch);
        }

        public static string[] GetLabels(this LocationInfo location, string filter)
        {
            // output example: (filter = "vob_112.7")
            ////Z:\vob>cleartool lstype -kind lbtype | findstr "vob_112.7"
            ////--03-09T16:54  E000000A    label type "vob_112.7"
            ////--03-12T14:26  E000000     label type "vob_112.7_1"
            ////--03-16T16:39  E000000     label type "vob_112.7_10"
            ////--03-13T10:07  e000000     label type "vob_112.7_2"
            ////--03-13T14:11  e000000     label type "vob_112.7_3"
            ////--03-13T15:26  H000000     label type "vob_112.7_4"
            ////--03-13T22:06  E000000     label type "vob_112.7_5"
            ////--03-16T14:28  E000000     label type "vob_112.7_6"
            ////--03-16T14:33  E000000     label type "vob_112.7_7"
            ////--03-16T14:48  E000000     label type "vob_112.7_8"
            ////--03-16T15:36  E000000     label type "vob_112.7_9"
            var ret = location.ClearTool("lstype -kind lbtype | findstr \"" + filter + "\"")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] { "\"" }, StringSplitOptions.RemoveEmptyEntries))
                .Select(t => t[1])
                .ToArray();
            return ret;
        }

        public static string[] GetBranches(this LocationInfo location)
        {
            // output example:
            ////2014-03-20     E000000     branch type "xl_vob_parfix6"
            ////--04-09T15:19  E000000     branch type "xl_vob_parfix7"
            ////--04-17T15:42  E000000     branch type "xl_vob_parfix8"
            ////2013-11-20     E000000     branch type "xl_vob_parfix9"
            ////2014-01-02     E000000     branch type "xl_vob_parfix10"
            ////2014-03-24     E000000     branch type "xl_vob_parfix11"
            ////2014-01-08     E000000     branch type "ydm_vob_mbmaster"
            ////  "Fix par for mbmaster"
            ////2014-01-14     E000000     branch type "ydm_vob_mbmaster_r101"
            ////  "Developer branch for modbus r101 scope"
            ////2013-12-19     E000000     branch type "zdt_resetvob_datalog"
            var brraw = location.ClearTool("lstype -kind brtype");
            var brreg = new Regex("branch type \"(.*)\"");
            var branches = brreg.Matches(brraw).OfType<Match>().Select(m => m.Groups[1].Value).ToArray();
            return branches;
        }

        public static void StartFindCheckouts(this LocationInfo location)
        {
            Process.Start(findExecutablePath, location.BasePath);
        }

        public static void UpdateConfigSpecMain(this LocationInfo location, string configSpecFilePath)
        {
            UpdateConfigSpec(location, (content) => GenerateConfigSpecMain(content), configSpecFilePath);
        }

        public static void UpdateConfigSpecBranch(this LocationInfo location, string branch, string configSpecFilePath)
        {
            UpdateConfigSpec(location, (content) => GenerateConfigSpecWithBranch(content, branch), configSpecFilePath);
        }

        internal static string GetBranchName(this LocationInfo location, string configSpecFilePath)
        {
            var currentCS = GetConfigSpecOfView(location);
            var fileCS = GetConfigSpecOfFile(location, configSpecFilePath);
            var status = "Outdated";
            if (currentCS == fileCS)
            {
                status = "Verifiable Main";
            }
            else if (currentCS == BLL.GenerateConfigSpecMain(fileCS))
            {
                status = "Developer Main";
            }
            else
            {
                var re = new Regex(@"element \* \.\.\./(?<branch>.*)/LATEST");
                var match = re.Match(currentCS);
                if (match.Success)
                {
                    var branch = match.Groups["branch"].Value;
                    if (currentCS == BLL.GenerateConfigSpecWithBranch(fileCS, branch))
                    {
                        status = $"[Branch]{branch}";
                    }
                }
            }

            return status;
        }

        internal static void ChangeToVerifyConfigSpec(this LocationInfo location, string configSpecFilePath)
        {
            var cs = GetConfigSpecOfFile(location, configSpecFilePath);
            SetConfigSpec(location, cs);
        }

        internal static void Merge(this LocationInfo location, string branch)
        {
            // value return like:            Z:        \\view\e000000_view
            // some time like this:          Z: \\view\E000000_view3 ClearCase Dynamic Views
            var path = DAL.RunBatch(string.Format("net use|find \"{0}:\" /I", location.Volume));
            var re = new Regex(@"\\\\view\\(?<name>[^\s]*)");
            var viewname = re.Match(path).Groups["name"].Value.Trim();
            var arg = string.Format("/t {0} /b {1}", viewname, branch);
            Log.DebugFormat("exec.uting {0} {1}", mergeManagerExecutablePath, arg);
            Process.Start(mergeManagerExecutablePath, arg);
        }

        private static string GenerateConfigSpecMain(string content)
        {
            content = content + Environment.NewLine + @"element * ...\main\LATEST" + Environment.NewLine;
            content = "element * CHECKEDOUT" + Environment.NewLine + content;
            return content;
        }

        private static string GenerateConfigSpecWithBranch(string content, string branch)
        {
            content = content + Environment.NewLine + @"element * ...\main\LATEST" + Environment.NewLine;

            content = Regex.Replace(content, @"(element \* [^#]*?)([#\n\r]+)", "$1 -mkbranch " + branch + " $2", RegexOptions.Singleline);

            content = "element * .../" + branch + "/LATEST" + Environment.NewLine + content;
            content = "element * CHECKEDOUT" + Environment.NewLine + content;
            return content;
        }

        private static string ClearTool(this DriverVolume driver, string arg)
        {
            return DAL.ClearTool(arg, $@"{driver}:\");
        }

        private static string ClearTool(this LocationInfo location, string arg)
        {
            return DAL.ClearTool(arg, location.BasePath);
        }

        private static void UpdateConfigSpec(this LocationInfo location, Func<string, string> generateConfigSpec, string configSpecFilePath)
        {
            var content = GetConfigSpecOfFile(location, configSpecFilePath);
            content = generateConfigSpec(content);
            SetConfigSpec(location, content);
        }
    }
}