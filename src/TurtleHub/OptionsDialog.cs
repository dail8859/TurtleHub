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
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace TurtleHub
{
    public partial class OptionsDialog : Form
    {
        public Parameters Params
        {
            get
            {
                Parameters p = new Parameters();
                p.Owner = TxtOwner.Text;
                p.Repository = TxtRepository.Text;
                p.Keyword = CmbKeyword.Text;
                p.RefFullRepo = CheckRefFullRepo.Checked;
                p.ShowPrsByDefault = CheckShowPrs.Checked;
                p.APIToken = TxtAPIToken.Text;
                return p;
            }
        }

        public OptionsDialog(Parameters parameters)
        {
            InitializeComponent();

            int idx = 0;
            CmbTracker.SelectedIndex = idx;

            TxtOwner.Text = parameters.Owner;
            TxtRepository.Text = parameters.Repository;
            CheckRefFullRepo.Checked = parameters.RefFullRepo;
            CheckShowPrs.Checked = parameters.ShowPrsByDefault;
            TxtAPIToken.Text = parameters.APIToken;

            idx = CmbKeyword.FindString(parameters.Keyword);
            CmbKeyword.SelectedIndex = idx != -1 ? idx : 0;
        }

        private void PreviewMessage()
        {
            TxtPreview.Text = this.Params.CreateReferenceMessage(new List<int> { 42 });
        }

        private async void TxtRepository_Enter(object sender, EventArgs e)
        {
            String owner = TxtOwner.Text;
            if (owner.Length == 0) return;

            try
            {
                var tracker = IssueTrackerFactory.CreateIssueTracker(Params);
                if (tracker != null)
                {
                    var repos = await tracker.GetAllRepositories();
                    var repo_list = new AutoCompleteStringCollection();
                    repo_list.AddRange(repos.ToArray());

                    TxtRepository.AutoCompleteCustomSource = repo_list;
                }
            }
            catch
            {
                // Let this silently fail, the user just won't have an autocomplete list
                // This could be due to no network connection, invalid username, etc
            }
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
        }

        private void TxtOwner_TextChanged(object sender, EventArgs e)
        {
            PreviewMessage();
        }

        private void TxtRepository_TextChanged(object sender, EventArgs e)
        {
            PreviewMessage();
        }

        private void CheckRefFullRepo_Click(object sender, EventArgs e)
        {
            PreviewMessage();
        }

        private void CmbKeyword_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreviewMessage();
        }
    }
}
