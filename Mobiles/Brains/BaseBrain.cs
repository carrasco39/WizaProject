using UnityEngine;
using System.Collections;
using UnityEngine.AI;


namespace BeheaderTavern.Scripts.Mobiles.Brains
{
    public enum EAI_State
    {
        STATE_WANDER,
        STATE_CHASE,
        STATE_PATROL,
        STATE_COMBAT,
        STATE_ATTACK,
        STATE_GUARD,
        STATE_FLEE,
    }

    public enum EAI_Behavior
    {
        BEHAVIOR_AGRESSIVE,
        BEHAVIOR_PASSIVE,
    }

    [System.Serializable]
    public class BaseBrain : MonoBehaviour
    {
        public EAI_State state;
        protected BaseCreature m_creature;
        protected GameActor m_target;
        [SerializeField]
        protected EAI_Behavior behavior;
        [SerializeField]
        protected float canSeeDistance;
        [SerializeField]
        protected float targetDistance;

        public virtual void Start()
        {
            m_creature = GetComponent<BaseCreature>();
            StartCoroutine(Think());
        }

        public virtual void Update()
        {
            
        }
            

        public virtual IEnumerator Think()
        {
            if (!HasLife())
            {
                Die();
                yield return null;
            }
                
            switch(state)
            {
                case EAI_State.STATE_WANDER:
                    DoWander();
                    yield return new WaitForSeconds(.5f);
                    break;
                case EAI_State.STATE_COMBAT:
                    DoCombat();
                    break;
                case EAI_State.STATE_CHASE:
                    DoChase();
                    break;
            }

            yield return new WaitForSeconds(1f/60f);
            yield return Think();
        }

        public virtual void DoChase()
        {
            if(m_target != null && m_creature.InRange(m_target,canSeeDistance))
            {
                var heading = m_target.transform.position - this.transform.position;
                var direction = heading / heading.magnitude;
                m_creature.agent.SetDestination(transform.position + direction * targetDistance);
                state = EAI_State.STATE_COMBAT;

            }
            else
            {
                state = EAI_State.STATE_WANDER;
            }
        }

        public virtual void DoWander()
        {
            if (m_creature.agent.velocity.magnitude <= 0)
            {
                var direction = new Vector3(m_creature.transform.position.x + 1 * Random.Range(-10f, 10f),
                                m_creature.transform.position.y, m_creature.transform.position.z + 1 * Random.Range(-10f, 10f));
                m_creature.agent.SetDestination(direction);
            }

            if (behavior == EAI_Behavior.BEHAVIOR_AGRESSIVE && state == EAI_State.STATE_WANDER)
            {
                var actor = m_creature.GetActorInRange(10, EGameActorType.TYPE_PLAYER);
                if ( actor != null && m_creature.CanSee(actor))
                {
                    m_target = actor;
                    state = EAI_State.STATE_COMBAT;
                }
            }

        }

        public virtual void DoCombat()
        {
            if(m_target != null && m_creature.CanSee(m_target))
            {
                if(!m_creature.InRange(m_target,targetDistance) && state == EAI_State.STATE_COMBAT)
                {
                    state = EAI_State.STATE_CHASE;
                }
            }
            else
            {
                state = EAI_State.STATE_WANDER;
            }
                
        }

        public virtual void Die()
        {
            GameObject.Destroy(m_creature.gameObject);
        }

        public bool HasLife()
        {
            if (m_creature.actorProps.Health > 0)
                return true;

            return false;
        }
    }
}

