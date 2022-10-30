using System;
using System.Collections.Generic;

namespace Jumper.Game
{
    public class Parachute
    {
        private List<string> parachuteLines = new List<string>();

        public Parachute()
        {
            this.parachuteLines.Add("  ___");
            this.parachuteLines.Add(@" /___\");
            this.parachuteLines.Add(@" \   /");
            this.parachuteLines.Add(@"  \ /");
        }
        public List<string> GetParachute()
        {
            return this.parachuteLines;
        }
        public void LoseLine()
        {
            parachuteLines.RemoveAt(0);
        }
        public bool StillExists()
        {
            if (parachuteLines.Count > 0)
                return true;
            else
                return false;
        }
    }
}