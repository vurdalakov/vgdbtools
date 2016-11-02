namespace Vurdalakov
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using EnvDTE80;
    using EnvDTE;
    using Microsoft.VisualStudio.Shell;

    public static class VsixHelper
    {
        public static DTE2 Ide { get { return Package.GetGlobalService(typeof(DTE)) as DTE2; } }

        public static Project[] GetProjects(this Solution solution)
        {
            var projects = new List<Project>();

            var enumerator = solution.Projects.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var project = enumerator.Current as Project;
                projects.AddRange(project.GetProjects());
            }

            return projects.ToArray();
        }

        private static Project[] GetProjects(this Project project)
        {
            if (null == project)
            {
                return new Project[0];
            }

            var projects = new List<Project>();

            if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
            {
                projects.Add(project);
            }

            if (project.ProjectItems != null)
            {
                for (var i = 1; i <= project.ProjectItems.Count; i++)
                {
                    var subProject = project.ProjectItems.Item(i).SubProject;
                    projects.AddRange(GetProjects(subProject));
                }
            }

            return projects.ToArray();
        }

        public static String GetDirectory(this Project project)
        {
            return String.IsNullOrEmpty(project.FullName) ? "" : Path.GetDirectoryName(project.FullName);
        }

        public static Configuration[] GetConfigurations(this Project project)
        {
            if ((null == project) || (null == project.ConfigurationManager))
            {
                return new Configuration[0];
            }

            var configurations = new List<Configuration>();

            for (int i = 1; i <= project.ConfigurationManager.Count; i++)
            {
                configurations.Add(project.ConfigurationManager.Item(i));
            }

            return configurations.ToArray();
        }

        public static ProjectItem FindItem(this Project project, String fileName)
        {
            if ((null == project) || (null == project.ProjectItems))
            {
                return null;
            }

            for (var i = 1; i <= project.ProjectItems.Count; i++)
            {
                var projectItem = project.ProjectItems.Item(i).FindItem(fileName);
                if (projectItem != null)
                {
                    return projectItem;
                }
            }

            return null;
        }

        private static ProjectItem FindItem(this ProjectItem projectItem, String fileName)
        {
            if (projectItem.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return projectItem;
            }

            for (Int16 i = 1; i <= projectItem.FileCount; i++)
            {
                if (Path.GetFileName(projectItem.FileNames[i]).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    return projectItem;
                }
            }

            for (var i = 1; i <= projectItem.ProjectItems.Count; i++)
            {
                var subProjectItem = projectItem.ProjectItems.Item(i).FindItem(fileName);
                if (subProjectItem != null)
                {
                    return subProjectItem;
                }
            }

            return null;
        }
    }
}
