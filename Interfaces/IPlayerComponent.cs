using System;
using UnityEngine;
using UnityEngine.AI;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Interfaces
{
    public interface IPlayerComponent
    {
        void Update();
        void Start(PlayerActor actor);
    }
}

