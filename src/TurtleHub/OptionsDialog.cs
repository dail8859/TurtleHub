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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TurtleHub
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog(string parameters)
        {
            InitializeComponent();

            var ownerrepo = parameters.Split('/');
            if (ownerrepo.Length == 2)
            {
                TxtOwner.Text = ownerrepo[0];
                TxtRepository.Text = ownerrepo[1];
            }
        }

        private void TxtRepository_Enter(object sender, EventArgs e)
        {
            StartRepoListRequest();
        }

        private void StartRepoListRequest()
        {
            String owner = TxtOwner.Text;

            if (string.IsNullOrEmpty(owner))
                return;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/users/" + owner + "/repos");

            Logger.LogMessage("Sending Http request for list of repos owned by " + owner);

            webRequest.UserAgent = "TurtleHub"; // per GitHub's documentation

            webRequest.BeginGetResponse(new AsyncCallback(FinishRepoListRequest), webRequest);
        }

        private void FinishRepoListRequest(IAsyncResult result)
        {
            Logger.LogMessage("\tReceived Http response for list of repositories");

            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)((HttpWebRequest)result.AsyncState).EndGetResponse(result);
                Logger.LogMessage("\t\tReceived response " + webResponse.StatusCode.ToString());

                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    var root = (SimpleJson.JsonArray)SimpleJson.SimpleJson.DeserializeObject(new StreamReader(webResponse.GetResponseStream(), true).ReadToEnd());

                    Logger.LogMessage("\t\tReceived " + root.Count.ToString() + " repositories");

                    var repos = new AutoCompleteStringCollection();
                    
                    foreach (SimpleJson.JsonObject item in root)
                        repos.Add(item["name"].ToString());

                    // Since this is a different thread, we have to call the BeginInvoke method
                    TxtRepository.BeginInvoke((Action)delegate
                    {
                        TxtRepository.AutoCompleteCustomSource = repos;
                    });
                }
                else
                {
                    Logger.LogMessage("\t\tUnexpected status code");
                }

                webResponse.Close();
            }
            catch (WebException wex)
            {
                HttpWebResponse webResponse = (HttpWebResponse)wex.Response;

                Logger.LogMessage("\t\tWebException: Received response " + webResponse.StatusCode.ToString());

                if (webResponse.StatusCode == HttpStatusCode.NotModified)
                {
                    // Should this ever happen?
                    Logger.LogMessage("\t\tCache hit");
                }
                else if (webResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    // This is ok for now
                }
                else
                {
                    Logger.LogMessage(wex.ToString());
                    MessageBox.Show(wex.ToString(), "TurtleHub Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.ToString());
                MessageBox.Show(ex.ToString(), "TurtleHub Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
