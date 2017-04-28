using UnityEngine;
using BeheaderTavern.Scripts.Spells;
using System.Collections;


namespace BeheaderTavern.Scripts.Mobiles.Brains
{
    public class MageBrain : BaseBrain
    {
        float _castCoolDown;
        SpellBase _spell;

        public override void Start()
        {
            state = EAI_State.STATE_WANDER;
            _spell = new FireballSpell();
            base.Start();

        }
            
        public override void DoCombat()
        {
            base.DoCombat();
            m_creature.transform.LookAt(m_target.transform);
            if (state != EAI_State.STATE_ATTACK && _castCoolDown <= 0)
            {
                StartCoroutine(StartCast());
            }
            m_creature.animator.SetBool("isCasting", state == EAI_State.STATE_ATTACK ? true : false);
        }

        public IEnumerator StartCast()
        {
            state = EAI_State.STATE_ATTACK;
            m_creature.agent.velocity = Vector3.zero;
            m_creature.agent.ResetPath();
            yield return new WaitForSeconds(_spell.SpellProps.CastTime);
            _spell.Cast(m_creature, m_target.transform.position);
            _castCoolDown = _spell.SpellProps.CoolDownDuration;
            m_creature.animator.SetTrigger("Cast");
            state = EAI_State.STATE_COMBAT;
        }


        public void Update()
        {
            if(_castCoolDown > 0)
            {
                _castCoolDown -= Time.deltaTime;
            }
        }
    }
}

