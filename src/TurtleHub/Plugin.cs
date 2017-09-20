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
using System.Windows.Forms;
using System.Linq;

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
                Parameters parms = new Parameters(parameters);
                IssueBrowserDialog form = new IssueBrowserDialog(parms);
                if (form.ShowDialog(WindowHandleWrapper.TryCreate(hParentWnd)) != DialogResult.OK)
                    return originalMessage;

                if (originalMessage.Length > 0 && !Char.IsWhiteSpace(originalMessage[originalMessage.Length - 1]))
                    originalMessage += " ";

                return originalMessage + parms.CreateReferenceMessage(form.IssuesFixed.Select(issue => issue.Number).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TurtleHub Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
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
            Logger.LogMessageWithData("ShowOptionsDialog: " + parameters);
            Parameters parms;

            try
            {
                parms = new Parameters(parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TurtleHub", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return parameters;
            }

            OptionsDialog form = new OptionsDialog(parms);
            if (form.ShowDialog(WindowHandleWrapper.TryCreate(hParentWnd)) == DialogResult.OK) return form.Params.ToString();
            else return parameters;
        }

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            Logger.LogMessageWithData("ValidateParameters: " + parameters);
            Parameters parms;
            Repository repo;

            try
            {
                parms = new Parameters(parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TurtleHub", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (parms.Owner.Length == 0 || parms.Repository.Length == 0)
            {
                MessageBox.Show("Invalid parameters.", "TurtleHub", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            try
            {
                var client = new GitHubClient(new ProductHeaderValue("TurtleHub"));
                var task = client.Repository.Get(parms.Owner, parms.Repository);
                task.Wait();
                repo = task.Result;
            }
            catch(AggregateException aex)
            {
                // NOTE: since we are waiting on the task, an AggregateException is thrown instead of just an ApiExcpetion
                foreach (Exception ex in aex.InnerExceptions)
                {
                    if(ex is NotFoundException)
                    {
                        if (((NotFoundException)ex).HttpResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            var res = MessageBox.Show("This repository cannot be found on the server. Continue anyways?", "TurtleHub", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            return res == DialogResult.Yes;
                        }
                    }
                }

                // Some other exception happend. Silently fail, not a big deal
                return true;
            }

            // Do a bit more to validate it
            if (!repo.HasIssues)
            {
                var res = MessageBox.Show("This repository doesn't allow issues to be created. Continue anyways?", "TurtleHub", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) return false;
            }
            else if (repo.OpenIssuesCount == 0)
            {
                var res = MessageBox.Show("This repository doesn't have any open issues. Continue anyways?", "TurtleHub", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) return false;
            }

            return true;
        }
    }
}