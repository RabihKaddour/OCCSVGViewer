using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCCSVGViewer.Helper
{
    public static class FileHelper
    {

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        public static string? ApplicationDir
        {
            get { return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); }
        }

        /// <summary>
        /// Returns the absolute path for the specified path string.
        /// </summary>
        public static string ProjectDataDir
        {
            get
            {
                if (ApplicationDir == null)
                {
                    return string.Empty;
                }
                return Path.GetFullPath(Path.Combine(ApplicationDir, @"../../../Data"));
            }
        }
    }
}
