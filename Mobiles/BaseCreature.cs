using System;
using BeheaderTavern.Scripts.Interfaces;
using BeheaderTavern.Scripts.Mobiles.Brains;
using UnityEngine.AI;
using UnityEngine;
using BeheaderTavern.Scripts.Spells;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Mobiles
{
    [RequireComponent(typeof(BaseBrain))]
    public class BaseCreature : GameActor
    {
        public NavMeshAgent agent;
        public BaseBrain brain;
        public Animator animator;

        void Start()
        {
            animator = this.GetComponent<Animator>();
        }

        void Update()
        {
            this.animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }
}

