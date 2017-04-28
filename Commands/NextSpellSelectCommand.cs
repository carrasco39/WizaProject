using System;
using BeheaderTavern.Scripts.Interfaces;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Commands
{

    public class NextSpellSelectCommand : Command
    {
        public NextSpellSelectCommand()
        {
        }

        public override void Execute(IPlayerComponent component)
        {
            (component as PlayerSpellSelector).Next();
        }
    }
}
