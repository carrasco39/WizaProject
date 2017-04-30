using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using BeheaderTavern.Scripts.Interfaces;
using BeheaderTavern.Scripts.Commands;
using BeheaderTavern.Scripts.Core;

namespace BeheaderTavern.Scripts.Mobiles.Player
{
    public class PlayerActor : GameActor
    {

        public enum EState : byte
        {
            STATE_IDLE,
            STATE_MOVING,
            STATE_DASHING,
            STATE_CASTING,
        }



        public NavMeshAgent agent;
        public Animator animator;
        public EState state;




        public PlayerTarget playerTarget;
        public PlayerDash playerDash;
        public PlayerMovement playerMovement;
        public PlayerCastSpell playerCastSpell;
        public PlayerSpellSelector playerSpellSelector;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            if(PhotonNetwork.offlineMode)
            {
                GameManager.instance.PlayerActor = this;
            }

            if (!PhotonNetwork.offlineMode && !photonView.isMine)
            {
                this.enabled = false;
            
            }
            else
            {
                agent.speed = actorProps.MaxSpeed;

                playerDash.Start(this);
                playerMovement.Start(this);
                playerCastSpell.Start(this);
                playerTarget.Start(this);
                playerSpellSelector.Start(this);
            }
        }


        private void Update()
        {
            playerDash.Update();
            playerMovement.Update();
            playerCastSpell.Update();
            playerTarget.Update();
            playerSpellSelector.Update();


        }
    }
}

