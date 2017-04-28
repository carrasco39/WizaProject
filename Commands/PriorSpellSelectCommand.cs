using System;
using BeheaderTavern.Scripts.Interfaces;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Commands
{
    public class PriorSpellSelectCommand : Command
    {
        public PriorSpellSelectCommand()
        {
        }


        public override void Execute(IPlayerComponent component)
        {
            (component as PlayerSpellSelector).Prior();
        }
    }

}

