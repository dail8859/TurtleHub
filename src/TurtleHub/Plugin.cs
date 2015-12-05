// This file is part of TurtleHub.
//
// Copyright (C)2013 Justin Dailey <dail8859@yahoo.com>
//
// TurtleHub is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.

using Interop.BugTraqProvider;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Octokit;

namespace TurtleHub
{
    [ComVisible(true)]
#if WIN64
    [Guid("B2C6EC0F-8742-4792-9FDC-10635D2C118B")]
#else
    [Guid("B2C6EC0F-8742-4792-9FDC-10635D2C118C")]
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public sealed class Plugin : IBugTraqProvider2
    {
        public string GetLinkText(IntPtr hParentWnd, string parameters)
        {
            Logger.LogMessageWithData("GetLinkText: " + parameters);
            return "Select Issue";
        }

        public string GetCommitMessage(IntPtr hParentWnd, string parameters, string commonRoot, string[] pathList, string originalMessage)
        {
            // This shouldn't ever get called
            Logger.LogMessageWithData("GetCommitMessage: " + parameters);
            Logger.LogMessageWithData("GetCommitMessage: " + commonRoot);
            foreach (var path in pathList) Logger.LogMessageWithData("GetCommitMessage: pathList: " + path);
            Logger.LogMessageWithData("GetCommitMessage: " + originalMessage);
            return "GetCommitMessage1";
        }

        public string GetCommitMessage2(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList,
            string originalMessage, string bugID, out string bugIDOut, out string[] revPropNames, out string[] revPropValues)
        {
            Logger.LogMessageWithData("GetCommitMessage2: DIC: " + System.IO.Directory.GetCurrentDirectory());
            Logger.LogMessageWithData("GetCommitMessage2: DIC-EXE: " + System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
            Logger.LogMessageWithData("GetCommitMessage2: DIC-ASS: " + System.IO.Path.GetDirectoryName(typeof(Logger).Assembly.Location));
            Logger.LogMessageWithData("GetCommitMessage2: parameters: " + parameters);
            Logger.LogMessageWithData("GetCommitMessage2: commonURL: " + commonURL);
            Logger.LogMessageWithData("GetCommitMessage2: commonRoot: " + commonRoot);
            foreach (var path in pathList) Logger.LogMessageWithData("GetCommitMessage2: pathList: " + path);
            Logger.LogMessageWithData("GetCommitMessage2: originalMessage: " + originalMessage);
            Logger.LogMessageWithData("GetCommitMessage2: bugID: " + bugID);
            
            // Don't know what these do, they were copied from Gurtle
            revPropNames = new string[0];
            revPropValues = new string[0];
            bugIDOut = bugID;

            try
            {
                IssueBrowserDialog form = new IssueBrowserDialog(new Parameters(parameters));
                if (form.ShowDialog(WindowHandleWrapper.TryCreate(hParentWnd)) != DialogResult.OK)
                    return originalMessage;

                StringBuilder result = new StringBuilder(originalMessage);
                if (originalMessage.Length != 0 && !originalMessage.EndsWith("\n"))
                    result.AppendLine();

                foreach (Issue issue in form.IssuesFixed)
                {
                    // TODO: make this string configurable
                    result.AppendFormat("Fixed #{0}: {1}", issue.Number, issue.Title);
                    result.AppendLine();
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "TurtleHub Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public string CheckCommit(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string commitMessage)
        {
            Logger.LogMessageWithData("CheckCommit: " + parameters);
            Logger.LogMessageWithData("CheckCommit: " + commonURL);
            Logger.LogMessageWithData("CheckCommit: " + commonRoot);
            foreach (var path in pathList) Logger.LogMessageWithData("CheckCommit: pathList: " + path);
            Logger.LogMessageWithData("CheckCommit: " + commitMessage);

            return null;
        }

        public string OnCommitFinished(IntPtr hParentWnd, string commonRoot, string[] pathList, string logMessage, int revision)
        {
            Logger.LogMessageWithData("OnCommitFinished:" + commonRoot);
            foreach (var path in pathList) Logger.LogMessageWithData("OnCommitFinished: pathList: " + path);
            Logger.LogMessageWithData("OnCommitFinished: " + logMessage);
            Logger.LogMessageWithData("OnCommitFinished: " + revision);
            return null;
        }

        public bool HasOptions()
        {
            Logger.LogMessageWithData("HasOptions");
            return true;
        }

        public string ShowOptionsDialog(IntPtr hParentWnd, string parameters)
        {
            Logger.LogMessageWithData("ShowOptionsDialog:" + parameters);

            OptionsDialog form = new OptionsDialog(new Parameters(parameters));
            if (form.ShowDialog(WindowHandleWrapper.TryCreate(hParentWnd)) == DialogResult.OK)
                return form.Params.ToString();

            // Else just return the original value
            return parameters;
        }

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            Logger.LogMessageWithData("ValidateParameters:" + parameters);
            Parameters p = new Parameters(parameters);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/" + p.Owner + "/" + p.Repository);
            webRequest.UserAgent = "TurtleHub"; // per GitHub's documentation
            webRequest.Method = "HEAD"; // we only need the status

            Logger.LogMessage("Sending Http request to validate " + parameters);

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                Logger.LogMessage("\tReceived response " + webResponse.StatusCode.ToString());

                if (webResponse.StatusCode == HttpStatusCode.OK) Logger.LogMessage("\tRepository found");
                else Logger.LogMessage("\tNot sure what happend but assume repository found");

                webResponse.Close();

                return true;
            }
            catch (WebException wex)
            {
                HttpWebResponse webResponse = (HttpWebResponse)wex.Response;

                Logger.LogMessage("\tWebException: Received response " + webResponse.StatusCode.ToString());

                MessageBox.Show(parameters + " does not exist.", "TurtleHub", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (webResponse.StatusCode == HttpStatusCode.NotFound) Logger.LogMessage("\tRepository does not exist");
                else Logger.LogMessage("\tNot sure what happend");

                webResponse.Close();

                return false;
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.ToString());
                MessageBox.Show(ex.ToString(), "TurtleHub Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}