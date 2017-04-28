using System;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Interfaces;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Commands
{
    public abstract class Command
    {
        ~Command(){}
        public virtual void Execute(IPlayerComponent component){}
        public virtual void Execute(GameActor actor){}
        public virtual void Execute(){}

    }
}

