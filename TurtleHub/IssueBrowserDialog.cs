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

namespace TurtleHub
{
    partial class IssueBrowserDialog : Form
    {
        private List<IssueItem> issues = new List<IssueItem>();
        private readonly List<IssueItem> _issuesAffected = new List<IssueItem>();
        private readonly string repo;
        private string etag = ""; // For conditional requests

        public IssueBrowserDialog(string parameters)
        {
            InitializeComponent();

            repo = parameters;

            StartIssuesRequest();
        }

        public void StartIssuesRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create("https://api.github.com/repos/" + repo + "/issues");

            Logger.LogMessage("Sending Http request for list of issues for " + repo);

            webRequest.UserAgent = "TurtleHub"; // per GitHub's documentation
            if (etag.Length > 0)
            {
                Logger.LogMessage("\tUsing etag: " + etag);
                webRequest.Headers.Add(HttpRequestHeader.IfNoneMatch, etag);
            }

            webRequest.BeginGetResponse(new AsyncCallback(FinishIssueRequest), webRequest);
        }

        public void FinishIssueRequest(IAsyncResult result)
        {
            Logger.LogMessage("\tReceived Http response for list of issues");

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)((HttpWebRequest)result.AsyncState).EndGetResponse(result);

                etag = webResponse.GetResponseHeader("Etag");

                Logger.LogMessage("\t\tReceived response " + webResponse.StatusCode.ToString());
                Logger.LogMessage("\t\tHeader contained etag: " + etag);

                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    var obj = (SimpleJson.JsonArray)SimpleJson.SimpleJson.DeserializeObject(new StreamReader(webResponse.GetResponseStream(), true).ReadToEnd());

                    Logger.LogMessage("\t\tReceived " + obj.Count.ToString() + " issues");

                    issues.Clear();
                    listView1.Items.Clear();
                    foreach (SimpleJson.JsonObject item in obj)
                    {
                        var num = (long)item["number"];
                        var desc = (string)item["title"];

                        issues.Add(new IssueItem((int)num, desc));
                    }

                    MyIssuesForm_Load(null, null);
                }
                else
                {
                    Logger.LogMessage("\t\tUnexpected status code");
                }

                webResponse.Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse webResponse = (HttpWebResponse) wex.Response;

                Logger.LogMessage("\t\tWebException: Received response " + webResponse.StatusCode.ToString());

                if (webResponse.StatusCode == HttpStatusCode.NotModified)
                    Logger.LogMessage("\t\tCache hit");
                else
                {
                    Logger.LogMessage(wex.ToString());
                    MessageBox.Show(wex.ToString());
                    throw;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.ToString());
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public IEnumerable<IssueItem> IssuesFixed
        {
            get { return _issuesAffected; }
        }

        private void MyIssuesForm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Clear();
            listView1.Columns.Add("");
            listView1.Columns.Add("#");
            listView1.Columns.Add("Summary");

            foreach(IssueItem issueItem in issues)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add(issueItem.Number.ToString());
                lvi.SubItems.Add(issueItem.Summary);
                lvi.Tag = issueItem;

                listView1.Items.Add(lvi);
            }

            listView1.Columns[0].Width = -1;
            listView1.Columns[1].Width = -1;
            listView1.Columns[2].Width = -1;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                IssueItem issueItem = lvi.Tag as IssueItem;
                if (issueItem != null && lvi.Checked)
                    _issuesAffected.Add(issueItem);
            }
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            Logger.LogMessage("Reload issues");
            StartIssuesRequest();
        }
    }
}
