namespace IcerDesign.CCHelper.Guide
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal class GuideAction
    {
        private readonly DriverVolume driver = DriverVolume.Z;

        public void Execute(string action)
        {
            var cmd = GuideCommand.Parse(action);
            var method = this.GetType().GetMethod(cmd.Command, BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                MessageBox.Show("Method is not available");
                return;
            }

            var pars = method
                .GetParameters()
                .Select((pi, i) => ChangeType(i >= (cmd.Parameters?.Length ?? 0) ? null : cmd.Parameters[i], pi.ParameterType))
                .ToArray();
            method?.Invoke(this, pars);
        }

        private static object ChangeType(string value, Type type)
        {
            if (value == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

            if (type.IsEnum) return Enum.Parse(type, value, true);

            try
            {
                return Convert.ChangeType(value, type);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Cannot convert [{value}] to type [{type}]", ex);
            }
        }

        private class GuideCommand
        {
            public static GuideCommand Parse(string action)
            {
                var cmdRx = new Regex(@"(?<action>[^\(]*)(\((?<pars>.*)\))?");
                var dataRx = new Regex(@"(?:^|, ?)(?<par>\""(?:[^\""]+|\""\"")*\""|[^,]*)");

                var m = cmdRx.Match(action);
                var cmd = m.Groups["action"].Value;
                var p = m.Groups["pars"].Value;
                var pars = dataRx
                    .Matches(m.Groups["pars"].Value)
                    .OfType<Match>()
                    .Select(_ => _.Groups["par"].Value)
                    .Where(_ => !string.IsNullOrEmpty(_))
                    .ToArray();
                pars = pars.Length == 0 ? null : pars;

                return new GuideCommand { Command = cmd, Parameters = pars };
            }

            public string Command { get; private set; }
            public string[] Parameters { get; private set; }
        }

        private void @switch(string vobName, string configSpecFilePath, frmSwitch.SwitchType swtype = frmSwitch.SwitchType.None)
            => new frmSwitch(new LocationInfo(this.driver, vobName), configSpecFilePath, swtype).Show();

        private void sync() => new frmSync().Show();

        private void checkin(string vobName) => new LocationInfo(this.driver, vobName).StartFindCheckouts();

        private void merge(string vobName) => new frmMerge(new LocationInfo(this.driver, vobName)).Show();

        private void commit(string vobName, string configSpecFilePath, string labelPattern)
            => new frmCommitter(new LocationInfo(this.driver, vobName), configSpecFilePath, labelPattern).Show();

        private void inspect() => MessageBox.Show(this.driver.GetConfigSpecOfView());
    }
}