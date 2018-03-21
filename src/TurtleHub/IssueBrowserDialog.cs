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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using BrightIdeasSoftware;
using System.Threading.Tasks;

namespace TurtleHub
{
    partial class IssueBrowserDialog : Form
    {
        private Parameters parameters;
        private IIssueTracker tracker;
        private TypedObjectListView<TurtleIssue> issuelistview;

        public IssueBrowserDialog(Parameters parameters)
        {
            Logger.LogMessageWithData("IssueBrowserDialog()");

            InitializeComponent();

            this.parameters = parameters;

            checkBoxShowPrs.Checked = parameters.ShowPrsByDefault;

            Text = string.Format(Text, parameters.Repository);

            // Wrap the objectlistview and set the aspects appropriately
            issuelistview = new TypedObjectListView<TurtleIssue>(this.objectListView1);
            issuelistview.GetColumn(0).AspectGetter = delegate(TurtleIssue x) { return x.Number; };
            issuelistview.GetColumn(1).AspectGetter = delegate(TurtleIssue x) { return x.Title; };
            issuelistview.GetColumn(2).AspectGetter = delegate(TurtleIssue x) { return x.Creator; };
            issuelistview.GetColumn(3).AspectGetter = delegate(TurtleIssue x) { return x.Assignee; };

            // Start the tracker magic
            tracker = IssueTrackerFactory.CreateIssueTracker(parameters);
        }

        private async Task MakeIssuesRequest()
        {
            Logger.LogMessageWithData("MakeIssuesRequest()");
            TxtSearch.Text = "";
            BtnReload.Enabled = false;
            workStatus.Visible = true;
            statusLabel.Text = "Downloading\x2026";

            try
            {
                var issues = await tracker.GetAllIssuesOnRepository();
                objectListView1.AddObjects(issues.ToArray());
                objectListView1.UseFiltering = true;
                objectListView1.FullRowSelect = true; // appearantly this is important to do
                ShowIssues();

            }
            finally
            {
                BtnReload.Enabled = true;
                workStatus.Visible = false;
                statusLabel.Text = "Ready";
            }
        }

        public IList<TurtleIssue> IssuesFixed { get { return issuelistview.CheckedObjects; } }

        private void ShowIssues()
        {
            // Create a new filter based on the searchbox
            var tmfilter = TextMatchFilter.Contains(objectListView1, TxtSearch.Text);

            ModelFilter prfilter;
            if (checkBoxShowPrs.Checked == true)
            {
                // Keep everything
                prfilter = new ModelFilter(delegate (object x) { return true; });
            }
            else
            {
                // Filter out pull requests
                prfilter = new ModelFilter(delegate (object x) { return !((TurtleIssue)x).IsPullRequest; });
            }
            var combfilter = new CompositeAllFilter(new List<IModelFilter> { tmfilter, prfilter });

            objectListView1.ModelFilter = combfilter;
            objectListView1.DefaultRenderer = new HighlightTextRenderer(tmfilter);
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            //Logger.LogMessage("Reload issues");
            //BtnShowGithub.Enabled = false;
            //MakeIssuesRequest();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowIssues();
        }

        private void BtnShowGithub_Click(object sender, EventArgs e)
        {
            var issue = issuelistview.SelectedObject;
            Logger.LogMessageWithData("Opening " + issue.HtmlUrl);
            Process.Start(issue.HtmlUrl);
        }

        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {
            BtnShowGithub.Enabled = objectListView1.SelectedObject != null;
        }

        private void ShowErrorMessage(string error)
        {
            TxtSearch.Text = "";
            TxtSearch.Enabled = false;
            BtnReload.Enabled = false;
            workStatus.Visible = false;
            statusLabel.ForeColor = Color.Red;
            statusLabel.Text = "Error: " + error;

            MessageBox.Show(error, "TurtleHub", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void IssueBrowserDialog_Load(object sender, EventArgs e)
        {
            try
            {
                await MakeIssuesRequest();
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
                ShowErrorMessage(ex.Message);
            }
        }

        private void checkBoxShowPrs_CheckedChanged(object sender, EventArgs e)
        {
            ShowIssues();
        }
    }
}
