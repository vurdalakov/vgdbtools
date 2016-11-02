namespace Vurdalakov
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    internal sealed class ExtensionCommandSet
    {
        public static readonly Guid CommandSetGuid = new Guid("4f218abc-b325-4fd0-9eec-a99b51612e8f");

        public const int ChangeRemoteConnectionCommandId = 0x0105;

        private readonly Package _package;
        private IServiceProvider ServiceProvider { get { return this._package; } }

        private ExtensionCommandSet(Package package)
        {
            if (null == package)
            {
                throw new ArgumentNullException("package");
            }
            this._package = package;

            var commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var commandID = new CommandID(CommandSetGuid, ChangeRemoteConnectionCommandId);
                var menuCommand = new MenuCommand(this.ChangeRemoteConnectionCommand, commandID);
                commandService.AddCommand(menuCommand);
            }
        }

        public static ExtensionCommandSet Instance { get; private set; }

        public static void Initialize(Package package)
        {
            Instance = new ExtensionCommandSet(package);
        }

        private void MsgBox(String message)
        {
            VsShellUtilities.ShowMessageBox(this.ServiceProvider, message, "", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        #region ChangeRemoteConnection command

        private void ChangeRemoteConnectionCommand(object sender, EventArgs e)
        {
            var fileNames = GetVgdbSettingsFileNames();

            if (0 == fileNames.Length)
            {
                MsgBox("No .vgdbsettings files found");
                return;
            }

            var existingConnections = GetExistingConnections();
            var currentConnection = GetCurrentConnection(fileNames);

            var form = new ChangeRemoteConnectionForm();
            form.AddConnections(existingConnections);
            form.SetConnection(currentConnection);
            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            SetCurrentConnection(fileNames, form.GetAddress(), form.GetUser());
        }

        private String[] GetVgdbSettingsFileNames()
        {
            var fileNames = new List<String>();

            var solution = VsixHelper.Ide.Solution;
            var projects = solution.GetProjects();

            foreach (var project in projects)
            {
                var directory = project.GetDirectory();
                if (String.IsNullOrEmpty(directory))
                {
                    continue;
                }

                var configurations = project.GetConfigurations();
                foreach (var configuration in configurations)
                {
                    var fileName = String.Format("{0}-{1}.vgdbsettings", project.Name, configuration.ConfigurationName);
                    fileName = Path.Combine(directory, fileName);
                    if (File.Exists(fileName))
                    {
                        fileNames.Add(fileName);
                    }
                }
            }

            return fileNames.ToArray();
        }

        private String[] GetExistingConnections()
        {
            var connections = new List<String>();

            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"VisualGDB\SSHConnections");
            var fileNames = Directory.GetFiles(directory, "*.sshconnection");

            foreach (var fileName in fileNames)
            {
                connections.Add(Path.GetFileNameWithoutExtension(fileName));
            }

            return connections.ToArray();
        }

        private String GetCurrentConnection(String[] fileNames)
        {
            var hostName = "";
            var userName = "";

            foreach (var fileName in fileNames)
            {
                using (var streamReader = File.OpenText(fileName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();

                        var match = Regex.Match(line, "<HostName>(.*)</HostName>");
                        if (match.Success && (2 == match.Groups.Count))
                        {
                            hostName = match.Groups[1].Value;
                        }
                        else
                        {
                            match = Regex.Match(line, "<UserName>(.*)</UserName>");
                            if (match.Success && (2 == match.Groups.Count))
                            {
                                userName = match.Groups[1].Value;
                            }
                        }

                        if (!String.IsNullOrEmpty(hostName) && !String.IsNullOrEmpty(userName))
                        {
                            return String.Format("{0}@{1}", userName, hostName);
                        }
                    }
                }
            }

            return null;
        }

        private void SetCurrentConnection(String[] fileNames, String newAddress, String newUser)
        {
            foreach (var fileName in fileNames)
            {
                var tempFileName = fileName + ".tmp";
                using (var streamWriter = new StreamWriter(tempFileName))
                {
                    using (var streamReader = File.OpenText(fileName))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = streamReader.ReadLine();

                            var match = Regex.Match(line, "<HostName>(.*)</HostName>");
                            if (match.Success && (2 == match.Groups.Count))
                            {
                                line = line.Replace(match.Groups[1].Value, newAddress);
                            }
                            else
                            {
                                match = Regex.Match(line, "<UserName>(.*)</UserName>");
                                if (match.Success && (2 == match.Groups.Count))
                                {
                                    line = line.Replace(match.Groups[1].Value, newUser);
                                }
                            }

                            streamWriter.WriteLine(line);
                        }
                    }
                }

                try
                {
                    File.Delete(fileName);
                    File.Move(tempFileName, fileName);
                }
                catch (Exception ex)
                {
                    MsgBox("Cannot replace file: " + ex.Message);
                }
            }
        }

        #endregion
    }
}
