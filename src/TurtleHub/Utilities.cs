// This file is part of TurtleHub.
// 
// Copyright (C)2017 Justin Dailey <dail8859@yahoo.com>
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
using System.IO;
using System.Linq;

namespace TurtleHub
{
    class Utilities
    {
        public static string GetStoredAPIToken(string AbsoluteUri)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataFolder, "TurtleHub", "tokens.txt");

            Logger.LogMessage("\t" + filePath);

            if (File.Exists(filePath))
            {
                Logger.LogMessage("\tFound token file");

                var tokenfile = File.ReadAllLines(filePath);
                Dictionary<string, string> dictionary = new List<string>(tokenfile).ToDictionary(s => s.Split('=')[0], s => s.Split('=')[1]);

                if (dictionary.ContainsKey(AbsoluteUri))
                {
                    Logger.LogMessage("\tFound token for " + AbsoluteUri);
                    return dictionary[AbsoluteUri];
                }
            }

            return null;
        }
    }
}
