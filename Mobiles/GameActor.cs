using System;
using UnityEngine;
using BeheaderTavern.Scripts.Interfaces;
using System.Collections.Generic;

namespace BeheaderTavern.Scripts.Mobiles
{
    public enum EGameActorType
    {
        TYPE_PLAYER,
        TYPE_AI,
        TYPE_AI_FOLLOWER,
        TYPE_NPC
    }

    [Serializable] 
    public class ActorProperties
    {
        [SerializeField]
        private EGameActorType m_type;
        [SerializeField]
        private float m_health;
        [SerializeField]
        private float m_maxHealth;
        [SerializeField]
        private float m_stamina;
        [SerializeField]
        private float m_maxStamina;
        [SerializeField]
        private int m_experience;
        [SerializeField]
        private int m_level;
// TODO: Remove set property
        [SerializeField]
        private float m_maxSpeed;

        public EGameActorType Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        public float Health
        {
            get
            {
                return m_health;
            }
            set
            {
                m_health = value;
            }
        }

        public float MaxHealth
        {
            get
            {
                return m_maxHealth;
            }
            set
            {
                m_maxHealth = value;
            }
        }

        public float Stamina
        {
            get
            {
                return m_stamina;
            }
            set
            {
                m_stamina = value;
            }
        }

        public int Experience
        {
            get
            {
                return m_experience;
            }
            set
            {
                m_experience = value;
            }
        }

        public int Level
        {
            get
            {
                return m_level;
            }
            set
            {
                m_level = value;
            }
        }

        public float MaxSpeed
        {
            get
            {
                return m_maxSpeed;
            }
            set
            {
                m_maxSpeed = value;
            }
        }
    }

    public class GameActor : MonoBehaviour
    {
        [SerializeField]
        public ActorProperties actorProps;

        public bool CanSee(GameActor actor)
        {
            
            Ray ray = new Ray(this.transform.position, actor.transform.position);
            RaycastHit hit;
            if (Physics.Linecast(this.transform.position, actor.transform.position, out hit))
            {
                if (hit.transform.tag == "Obstacle")
                    return false;
            }
                
            return true;
        }

        public bool InRange(GameActor actor, float range)
        {
            return Vector3.Distance(this.transform.position, actor.transform.position) > range ? false : true;
        }

        public bool InRange(float range, EGameActorType type)
        {
            var colliders = Physics.OverlapSphere(this.transform.transform.position, range);
            for (int i = 0; i < colliders.Length; i++)
            {
                var gameActor = colliders[i].GetComponent<GameActor>();
                if (gameActor != null && gameActor.actorProps.Type == type)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the less distant actor in range.
        /// </summary>
        /// <returns>The actor in range.</returns>
        /// <param name="range">Range.</param>
        /// <param name="type">Type.</param>
        public GameActor GetActorInRange(float range, EGameActorType type)
        {
            GameActor gameActor = null; 
            var distance = Mathf.Infinity;
            var colliders = Physics.OverlapSphere(this.transform.position, range);
            for (int i = 0; i < colliders.Length; i++)
            {
                var mActor = colliders[i].GetComponent<GameActor>();
                if (mActor != null && mActor.actorProps.Type == type)
                {
                    var dist = Vector3.Distance(this.transform.position, mActor.transform.position);
                    if (dist < distance)
                    {
                        distance = dist;
                        gameActor = mActor;
                    }
                }
            }
            return gameActor;
        }

        /// <summary>
        /// Gets the actors in range.
        /// </summary>
        /// <returns>The actors in range.</returns>
        /// <param name="range">Range.</param>
        /// <param name="type">Type.</param>
        public GameActor[] GetActorsInRange(float range, EGameActorType type)
        {
            var colliders = Physics.OverlapSphere(this.transform.position, range);
            List<GameActor> actorList = new List<GameActor>();
            for (int i = 0; i < colliders.Length; i++)
            {
                var gActor = colliders[i].GetComponent<GameActor>();
                if(gActor != null)
                {
                    actorList.Add(gActor);
                }
            }

            return actorList.ToArray();
        }
    }
}