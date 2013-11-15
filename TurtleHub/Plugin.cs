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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Interop.BugTraqProvider;

namespace TurtleHub
{
    [ComVisible(true), Guid("B2C6EC0F-8742-4792-9FDC-10635D2C118B"), ClassInterface(ClassInterfaceType.None)]
    public class Plugin : IBugTraqProvider2
    {
        public string GetLinkText(IntPtr hParentWnd, string parameters)
        {
            return "Select Issue";
        }

        public string GetCommitMessage(IntPtr hParentWnd, string parameters, string commonRoot, string[] pathList, string originalMessage)
        {
            MessageBox.Show("GetCommitMessage1");
            return "GetCommitMessage1";
        }

        public string GetCommitMessage2(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList,
            string originalMessage, string bugID, out string bugIDOut, out string[] revPropNames, out string[] revPropValues)
        {
            try
            {
                // TODO: get list of issues from github
                List<TicketItem> tickets = new List<TicketItem>();
                tickets.Add(new TicketItem(12, "Service doesn't start on Windows Vista"));
                tickets.Add(new TicketItem(19, "About box doesn't render correctly in large fonts mode"));
                tickets.Add(new TicketItem(21, "Only a test"));

                // Don't know what these do, they were copied from Gurtle
                revPropNames = new string[0];
                revPropValues = new string[0];
                bugIDOut = bugID;

                IssueBrowserDialog form = new IssueBrowserDialog(tickets);
                if (form.ShowDialog() != DialogResult.OK)
                    return originalMessage;

                StringBuilder result = new StringBuilder(originalMessage);
                if (originalMessage.Length != 0 && !originalMessage.EndsWith("\n"))
                    result.AppendLine();

                foreach (TicketItem ticket in form.TicketsFixed)
                {
                    result.AppendFormat("Fixed #{0}: {1}", ticket.Number, ticket.Summary);
                    result.AppendLine();
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public string CheckCommit(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string commitMessage)
        {
            return null;
        }

        public string OnCommitFinished(IntPtr hParentWnd, string commonRoot, string[] pathList, string logMessage, int revision)
        {
            return null;
        }

        public bool HasOptions()
        {
            return true;
        }

        public string ShowOptionsDialog(IntPtr hParentWnd, string parameters)
        {
            // Dialog to get username and repo
            return "username/repo";
        }

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            // ensure username/repo exists
            return true;
        }
    }
}
