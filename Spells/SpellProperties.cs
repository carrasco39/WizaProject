using UnityEngine;
using System.Collections;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Enums;

namespace BeheaderTavern.Scripts.Spells
{
    public class SpellProperties
    {
        [SerializeField]
        private float m_damage;
        [SerializeField]
        private float m_coolDownDuration;
        [SerializeField]
        private float m_castTime;
        [SerializeField]
        private float m_spellDuration;
        [SerializeField]
        private float m_staminaCost;
        [SerializeField]
        private float m_currentCoolDownTimer;
        [SerializeField]
        private ESpellType m_type;
        [SerializeField]
        private Sprite m_icon;

        
        public float Damage
        {
            get
            {
                return m_damage;
            }
            set
            {
                m_damage = value;
            }
        }
        
        public float CoolDownDuration
        {
            get
            {
                return m_coolDownDuration;
            }
            set
            {
                m_coolDownDuration = value;
            }
        }

        public float CoolDownTimer
        {
            get
            {
                return m_currentCoolDownTimer;
            }
            set
            {
                m_currentCoolDownTimer = value;
            }
        }
        
        public float CastTime
        {
            get
            {
                return m_castTime;
            }
            set
            {
                m_castTime = value;
            }
        }
        
        public float SpellDuration
        {
            get
            {
                return m_spellDuration;
            }
            set
            {
                m_spellDuration = value;
            }
        }
        
        public float StaminaCost
        {
            get
            {
                return m_staminaCost;
            }
            set
            {
                m_staminaCost = value;
            }
        }
        
        public ESpellType Type
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

        public Sprite Icon
        {
            get
            {
                return m_icon;
            }
            set
            {
                m_icon = value;
            }
        }
    }
    
}
