using UnityEngine;
using UnityEngine.AI;
using BeheaderTavern.Scripts.Interfaces;
using RAIN.Core;

namespace BeheaderTavern.Scripts.Mobiles.Player
{
    [System.Serializable]
    public class PlayerMovement : IPlayerComponent
    {
        private PlayerActor _actor;


        public void Start(PlayerActor actor)
        {
            _actor = actor;
        }

        #region IPlayerComponent implementation
        public void Update()
        {
            _actor.animator.SetFloat("Speed", _actor.agent.velocity.magnitude);


        }
        #endregion

        public void DoMove()
        {
            NavMeshMove();

        }

        public void NavMeshMove()
        {
            if (_actor.state == PlayerActor.EState.STATE_CASTING)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                _actor.agent.SetDestination(hit.point);
            }
        }

    }
}

