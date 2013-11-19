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

namespace TurtleHub
{
    internal class IssueItem
    {
        private readonly int _issueNumber = -1;
        private readonly string _issueSummary = "";
        private readonly string _openedBy = "";
        private readonly string _assignedTo = "";

        public IssueItem(SimpleJson.JsonObject obj)
        {
            _issueNumber = (int)(long)obj["number"];
            _issueSummary = (string)obj["title"];

            var user = (SimpleJson.JsonObject)obj["user"];
            _openedBy = (string)user["login"];

            var assignee = (SimpleJson.JsonObject)obj["assignee"];
            if (assignee != null)
                _assignedTo = (string)assignee["login"];
        }

        public int Number
        {
            get { return _issueNumber; }
        }

        public string Summary
        {
            get { return _issueSummary; }
        }

        public string OpenedBy
        {
            get { return _openedBy; }
        }

        public string AssignedTo
        {
            get { return _assignedTo; }
        }
    }
}