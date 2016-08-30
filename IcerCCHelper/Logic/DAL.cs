namespace IcerDesign.CCHelper
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;

    public class DAL
    {
        private static log4net.ILog Log = log4net.LogManager.GetLogger("DAL");

        public static string ClearTool(string arg, string path)
        {
            return RunBatch(@"@echo off
cd /d " + path + @"
cleartool " + arg);
        }

        public static string RunBatch(string content)
        {
            var systmpFile = Path.GetTempFileName();
            File.Delete(systmpFile);
            var tempFilename = systmpFile + ".bat";
            File.WriteAllText(tempFilename, content, Encoding.ASCII);
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = tempFilename,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            process.StartInfo = startInfo;

            string error = "";
            string output = "";
            process.Start();
            InputAndOutputToEnd(process, null, ref output, ref error);
            process.WaitForExit();

            File.Delete(tempFilename);
            if (process.ExitCode != 0)
            {
                Log.DebugFormat("batch:[{0}]\n\nerror:[{1}]\n\noutput:[{2}]", content, error, output);
                throw new ExecutionException("error: " + error);
            }

            return output;
        }

        /// <summary>
        /// InputAndOutputToEnd: a handy way to use redirected input/output/error on a p.
        /// </summary>
        /// <param name="p">The p to redirect. Must have UseShellExecute set to false.</param>
        /// <param name="StandardInput">This string will be sent as input to the p. (must be Nothing if not StartInfo.RedirectStandardInput)</param>
        /// <param name="StandardOutput">The p's output will be collected in this ByRef string. (must be Nothing if not StartInfo.RedirectStandardOutput)</param>
        /// <param name="StandardError">The p's error will be collected in this ByRef string. (must be Nothing if not StartInfo.RedirectStandardError)</param>
        /// <remarks>This function solves the deadlock problem mentioned at http://msdn.microsoft.com/en-us/library/system.diagnostics.p.standardoutput.aspx </remarks>
        private static void InputAndOutputToEnd(Process p, string StandardInput, ref string StandardOutput, ref string StandardError)
        {
            if (p == null)
                throw new ArgumentException("p must be non-null");

            //Assume p has started. Alas there's no way to check.
            if (p.StartInfo.UseShellExecute)
                throw new ArgumentException("Set StartInfo.UseShellExecute to false");

            if (p.StartInfo.RedirectStandardInput != (StandardInput != null))
                throw new ArgumentException("Provide a non-null Input only when StartInfo.RedirectStandardInput");

            if (p.StartInfo.RedirectStandardOutput != (StandardOutput != null))
                throw new ArgumentException("Provide a non-null Output only when StartInfo.RedirectStandardOutput");

            if (p.StartInfo.RedirectStandardError != (StandardError != null))
                throw new ArgumentException("Provide a non-null Error only when StartInfo.RedirectStandardError");

            InputAndOutputToEndData outputData = new InputAndOutputToEndData();
            InputAndOutputToEndData errorData = new InputAndOutputToEndData();

            if (p.StartInfo.RedirectStandardOutput)
            {
                outputData.Stream = p.StandardOutput;
                outputData.Thread = new Thread(InputAndOutputToEndProc);
                outputData.Thread.Start(outputData);
            }

            if (p.StartInfo.RedirectStandardError)
            {
                errorData.Stream = p.StandardError;
                errorData.Thread = new Thread(InputAndOutputToEndProc);
                errorData.Thread.Start(errorData);
            }

            if (p.StartInfo.RedirectStandardInput)
            {
                p.StandardInput.Write(StandardInput);
                p.StandardInput.Close();
            }

            if (p.StartInfo.RedirectStandardOutput)
            {
                outputData.Thread.Join();
                StandardOutput = outputData.Output;
            }
            if (p.StartInfo.RedirectStandardError)
            {
                errorData.Thread.Join();
                StandardError = errorData.Output;
            }
            if (outputData.Exception != null)
                throw outputData.Exception;
            if (errorData.Exception != null)
                throw errorData.Exception;
        }

        private static void InputAndOutputToEndProc(Object data_)
        {
            InputAndOutputToEndData data = (InputAndOutputToEndData)data_;
            try
            {
                data.Output = data.Stream.ReadToEnd();
            }
            catch (Exception e)
            {
                data.Exception = e;
            }
        }

        private class InputAndOutputToEndData
        {
            public Thread Thread;
            public StreamReader Stream;
            public String Output;
            public Exception Exception;
        }
    }
}