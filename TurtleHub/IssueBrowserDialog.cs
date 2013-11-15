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

namespace TurtleHub
{
    partial class IssueBrowserDialog : Form
    {
        private readonly IEnumerable<TicketItem> _tickets;
        private readonly List<TicketItem> _ticketsAffected = new List<TicketItem>();

        public IssueBrowserDialog(IEnumerable<TicketItem> tickets)
        {
            InitializeComponent();
            _tickets = tickets;
        }

        public IEnumerable<TicketItem> TicketsFixed
        {
            get { return _ticketsAffected; }
        }

        private void MyIssuesForm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("");
            listView1.Columns.Add("#");
            listView1.Columns.Add("Summary");

            foreach(TicketItem ticketItem in _tickets)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add(ticketItem.Number.ToString());
                lvi.SubItems.Add(ticketItem.Summary);
                lvi.Tag = ticketItem;

                listView1.Items.Add(lvi);
            }

            listView1.Columns[0].Width = -1;
            listView1.Columns[1].Width = -1;
            listView1.Columns[2].Width = -1;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                TicketItem ticketItem = lvi.Tag as TicketItem;
                if (ticketItem != null && lvi.Checked)
                    _ticketsAffected.Add(ticketItem);
            }
        }
    }
}
