using System;
using System.Collections.Generic;
using System.Text;

namespace Visitare
{
    public class RewardsGetMine
    {
        public string Nickname { get; set; }
        public string Id { get; set; }
        public int Punkty { get; set; }

       public IList<string> Roles { get; set; }
    }
}
