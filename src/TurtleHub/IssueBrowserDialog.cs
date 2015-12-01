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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
using System.Diagnostics;

using Octokit;

namespace TurtleHub
{
    partial class IssueBrowserDialog : Form
    {
        private List<Issue> _issuesAffected = new List<Issue>();
        private IReadOnlyCollection<Issue> issues;
        private Parameters parameters;
        private Release latest_release = null;

        public IssueBrowserDialog(Parameters parameters)
        {
            InitializeComponent();

            this.parameters = parameters;

            Text = string.Format(Text, parameters.Repository);
        }

        private async void StartIssuesRequest()
        {
            BtnReload.Enabled = false;
            workStatus.Visible = true;
            statusLabel.Text = "Downloading\x2026";

            var github = new GitHubClient(new ProductHeaderValue("TurtleHub"));
            Logger.LogMessageWithData("Making Github request for issues");

#if DEBUG
            // The logging normally takes care of this but ifdef'ing this out keeps it from doing an unneeded rate limit check
            var ratelimit = await github.Miscellaneous.GetRateLimits();
            Logger.LogMessage(string.Format("\tRate limit: {0}/{1}", ratelimit.Resources.Core.Remaining.ToString(), ratelimit.Resources.Core.Limit.ToString()));
#endif

            issues = await github.Issue.GetAllForRepository(parameters.Username, parameters.Repository);

            listView1.Items.Clear();
            Logger.LogMessage("\tGot " + issues.Count().ToString() + " issues");

            foreach(var issue in issues)
            {
                // Skip pull requests
                if (issue.PullRequest != null) continue;

                ListViewItem lvi = new ListViewItem();
                lvi.Text = issue.Number.ToString();
                lvi.SubItems.Add(issue.Title);
                lvi.SubItems.Add(issue.User.Login);
                if (issue.Assignee != null) lvi.SubItems.Add(issue.Assignee.Login);
                else lvi.SubItems.Add("");
                lvi.Tag = issue;
                listView1.Items.Add(lvi);
            }
#if DEBUG
            ratelimit = await github.Miscellaneous.GetRateLimits();
            Logger.LogMessage(string.Format("\tRate limit: {0}/{1}", ratelimit.Resources.Core.Remaining.ToString(), ratelimit.Resources.Core.Limit.ToString()));
#endif
            BtnReload.Enabled = true;
            workStatus.Visible = false;
            statusLabel.Text = "Ready";

            CheckForUpdate(github);
        }

        private async void CheckForUpdate(GitHubClient github)
        {
            // Only check if we haven't checked before
            if (latest_release != null) return;

            // Let's check to see if there is an update for TurtleHub
            Logger.LogMessageWithData("Checking for new TurtleHub release");
            var releases = await github.Release.GetAll("dail8859", "TurtleHub");
            var latest = await github.Release.Get("dail8859", "TurtleHub", releases[0].Id);
            Logger.LogMessage("\tFound " + latest.TagName);

            var thatVersion = Version.Parse(latest.TagName.Substring(1)); // remove the v from e.g. v0.1.1
            var thisVersion = typeof(Plugin).Assembly.GetName().Version;

            Logger.LogMessage("\tThis " + thisVersion.ToString());
            Logger.LogMessage("\tThat " + thatVersion.ToString());
            if (thatVersion > thisVersion)
            {
                updateNotifyIcon.BalloonTipText = string.Format(updateNotifyIcon.BalloonTipText, latest.TagName);
                updateNotifyIcon.Visible = true;
                updateNotifyIcon.ShowBalloonTip(15 * 1000);
            }

            latest_release = latest;
            //var response = await client.Connection.Get<object>(new Uri(latestAsset[0].Url), new Dictionary<string, string>(), "application/octet-stream");
        }

        public IReadOnlyCollection<Issue> IssuesFixed
        {
            get { return _issuesAffected; }
        }

        private void MyIssuesForm_Load(object sender, EventArgs e)
        {
            StartIssuesRequest();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                Issue issueItem = lvi.Tag as Issue;
                if (issueItem != null && lvi.Checked)
                    _issuesAffected.Add(issueItem);
            }
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            Logger.LogMessage("Reload issues");
            BtnShowGithub.Enabled = false;
            StartIssuesRequest();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            BtnShowGithub.Enabled = e.IsSelected;
        }

        private void BtnShowGithub_Click(object sender, EventArgs e)
        {
            var issue = listView1.SelectedItems[0].Tag as Issue;
            Logger.LogMessageWithData("Opening " + issue.HtmlUrl.AbsoluteUri);
            Process.Start(issue.HtmlUrl.AbsoluteUri);
        }

        private void updateNotifyIcon_Click(object sender, EventArgs e)
        {
            var thatVersion = Version.Parse(latest_release.TagName.Substring(1)); // remove the v from e.g. v0.1.1
            var thisVersion = typeof(Plugin).Assembly.GetName().Version;

            var message = new StringBuilder()
                    .AppendLine("There is a new version of TurtleHub available. Would you like to update now?")
                    .AppendLine()
                    .Append("Your version: ").Append(thisVersion).AppendLine()
                    .Append("New version: ").Append(thatVersion).AppendLine()
                    .ToString();

            var reply = MessageBox.Show(this, message,
                "Update Notice", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (reply == DialogResult.Cancel)
                return;

            if (reply == DialogResult.Yes)
            {
                Process.Start(latest_release.HtmlUrl);
                Close();
            }

            updateNotifyIcon.Visible = false;
        }
    }
}
