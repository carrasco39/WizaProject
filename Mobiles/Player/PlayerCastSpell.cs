using System;
using BeheaderTavern.Scripts.Interfaces;
using UnityEngine;
using BeheaderTavern.Scripts.Spells;
using BeheaderTavern.Scripts.Enums;

namespace BeheaderTavern.Scripts.Mobiles.Player
{
    [Serializable]
    public class PlayerCastSpell : IPlayerComponent
    {
        private PlayerActor _actor;
        private float _castingTimer;
        //private float _coolDownTimer;
        private Vector3 _castPosition;

        //public SpellBase selectedSpell;
        public SpellProperties CurrSpellProps
        {
            get
            {
                return _actor.playerSpellSelector.selectedSpell.SpellProps;
            }
        }

        #region IPlayerComponent implementation

        public void Update()
        {
            _actor.animator.SetBool("isCasting", _actor.state == PlayerActor.EState.STATE_CASTING ? true : false);

            if (_castingTimer > 0 && _actor.state == PlayerActor.EState.STATE_CASTING)
            {
                _castingTimer -= Time.deltaTime;
            }
            else if (_castingTimer <= 0 && _actor.state == PlayerActor.EState.STATE_CASTING)
            {
                CastSpell();  
            }
        }

        public void Start(PlayerActor actor)
        {
            _actor = actor;
        }

        #endregion

        public void DoCast()
        {
            if (_actor.state != PlayerActor.EState.STATE_CASTING && CurrSpellProps.CoolDownTimer <= 0)
            {
                _castingTimer = CurrSpellProps.CastTime;
                _actor.state = PlayerActor.EState.STATE_CASTING;
                _castPosition = Input.mousePosition;
                if (_actor.playerTarget.target != null && CurrSpellProps.Type != ESpellType.TYPE_AREA)
                {
                    _actor.transform.LookAt(_actor.playerTarget.target.transform.position);  
                }
                else
                {
                    var hit = HitPosition(_castPosition);
                    _actor.transform.LookAt(hit.point);
                }
                StopAgentPath();

            }
        }

        public void CastSpell()
        {    
            _actor.playerSpellSelector.selectedSpell.SpellProps.CoolDownTimer = _actor.playerSpellSelector.selectedSpell.SpellProps.CoolDownDuration;
            _actor.animator.SetTrigger("Cast");
            _actor.state = PlayerActor.EState.STATE_IDLE;
            switch(CurrSpellProps.Type)
            {
                case ESpellType.TYPE_SELF:
                case ESpellType.TYPE_RANGED:
                    CastRangedSpell();
                    break;
                case ESpellType.TYPE_AREA:
                    CastAreaSpell();
                    break;
            }

        }

        private void CastAreaSpell()
        {
            var spell = _actor.playerSpellSelector.selectedSpell;
            spell.Cast(_actor, _castPosition);


        }

        private void CastRangedSpell()
        {
            var spell = _actor.playerSpellSelector.selectedSpell;
            if(_actor.playerTarget.target != null)
            {
                spell.Cast((GameActor)_actor,_actor.playerTarget.target.transform.position);

            }
            else
            {
                var hit = HitPosition(_castPosition);
                _actor.transform.LookAt(hit.point);
                spell.Cast(_actor,hit.point);

                var gameActor = hit.collider.GetComponent<GameActor>();
                if(gameActor != null && gameActor != _actor)
                {
                    _actor.playerTarget.target = gameActor;
                }
            }
        }

            
        private void StopAgentPath()
        {
            _actor.agent.ResetPath();
            _actor.agent.velocity = Vector3.zero;
        }

        private RaycastHit HitPosition(Vector3 vector)
        {
            Ray ray = Camera.main.ScreenPointToRay(vector);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                return hit;
            }

            return hit;
        }

    }
}

