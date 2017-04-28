using System;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Mobiles.Player;
using BeheaderTavern.Scripts.Interfaces;

namespace BeheaderTavern.Scripts.Commands
{
    public class CastSpellCommand : Command
    {
        public CastSpellCommand()
        {
        }
            

        public override void Execute(IPlayerComponent component)
        {
            (component as PlayerCastSpell).DoCast();
        }


    }
}

