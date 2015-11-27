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

using Octokit;

namespace TurtleHub
{
    public partial class OptionsDialog : Form
    {
        public Parameters Params { get { return new Parameters(TxtOwner.Text, TxtRepository.Text); } }

        public OptionsDialog(Parameters parameters)
        {
            InitializeComponent();

            TxtOwner.Text = parameters.Username;
            TxtRepository.Text = parameters.Repository;
        }

        private async void TxtRepository_Enter(object sender, EventArgs e)
        {
            String owner = TxtOwner.Text;
            var github = new GitHubClient(new ProductHeaderValue("TurtleHub"));
            var repos = await github.Repository.GetAllForUser(owner);

            var repo_list = new AutoCompleteStringCollection();
            foreach(var repo in repos)
            {
                repo_list.Add(repo.Name);
            }

            TxtRepository.AutoCompleteCustomSource = repo_list;
        }
    }
}
