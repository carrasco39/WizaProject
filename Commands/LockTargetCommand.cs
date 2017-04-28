using System;
using BeheaderTavern.Scripts.Interfaces;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Commands
{
    public class LockTargetCommand : Command
    {
        #region implemented abstract members of Command

        public override void Execute(IPlayerComponent component)
        {
            (component as PlayerTarget).LockTarget();
        }

        #endregion
    }
}

