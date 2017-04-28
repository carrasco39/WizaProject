using System;
using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using BeheaderTavern.Scripts.Interfaces;

namespace BeheaderTavern.Scripts.Mobiles.Player
{
    [Serializable]
    public class PlayerDash : IPlayerComponent
    {
        
        public float dashCoolDown = 15f;
        public float dashMaxSpeed = 8f;
        public float dashDuration = 5f;
        public float dashCoolDownTimer;

        private PlayerActor _actor;

        public void Start(PlayerActor actor)
        {
            _actor = actor;
        }

        public void Update()
        {
            _actor.animator.SetBool("Dash", _actor.state == PlayerActor.EState.STATE_DASHING ? true:false);

            if(dashCoolDownTimer > 0)
            {
                dashCoolDownTimer -= Time.deltaTime;
            } 
        }

        public void DoDash()
        {
            if(_actor.state != PlayerActor.EState.STATE_CASTING && dashCoolDownTimer <=0)
                _actor.StartCoroutine(StartDash());
        }

        public IEnumerator StartDash()
        {
            _actor.state = PlayerActor.EState.STATE_DASHING;
            _actor.agent.speed = dashMaxSpeed;
            var timer = 0f;
            while(timer < dashDuration && _actor.agent.velocity.magnitude > 0)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();  
            }
            StopDash();
        }

        private void StopDash()
        {
            _actor.agent.speed = _actor.actorProps.MaxSpeed;
            if(_actor.state != PlayerActor.EState.STATE_CASTING)
                _actor.state = PlayerActor.EState.STATE_IDLE;
            dashCoolDownTimer = dashCoolDown;
        }

    }
}

