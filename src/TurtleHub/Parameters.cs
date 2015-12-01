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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleHub
{
    public class Parameters
    {
        public string Username { get; private set; }
        public string Repository { get; private set; }

        public Parameters(string parameters)
        {
            var ownerrepo = parameters.Split('/');
            if (ownerrepo.Length == 2)
            {
                this.Username = ownerrepo[0];
                this.Repository = ownerrepo[1];
            }
        }

        public Parameters(string username, string repository)
        {
            this.Username = username;
            this.Repository = repository;
        }

        public override string ToString()
        {
            // NOTE: make sure this string is formatted in a way that is acceptable to the constructor
            return Username + "/" + Repository;
        }
    }
}
