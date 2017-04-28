using System;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Mobiles.Player;
using BeheaderTavern.Scripts.Interfaces;

namespace BeheaderTavern.Scripts.Commands
{
    public class DashCommand : Command
    {
        public DashCommand()
        {
        }
            
        #region implemented abstract members of Command
        public override void Execute(IPlayerComponent component)
        {
            (component as PlayerDash).DoDash();
        }
        #endregion
    }
}

