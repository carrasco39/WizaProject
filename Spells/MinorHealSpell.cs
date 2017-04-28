using System;
using UnityEngine;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Enums;

namespace BeheaderTavern.Scripts.Spells
{
    public class MinorHealSpell : SpellBase
    {
        public override SpellProperties SpellProps { get; set;}

        public override SpellObject SpellObject{ get; set; }
       

        public MinorHealSpell()
        {
            SpellProps = new SpellProperties
                {
                    Damage = 5,
                    SpellDuration = 3,
                    CoolDownDuration = 5f,
                    StaminaCost = 10f, 
                    Type = ESpellType.TYPE_SELF,
                    CastTime = 0.5f,
                    CoolDownTimer = 5f,
                    Icon = Resources.Load<Sprite>("MinorHeal"),
                };
                    

            SpellObject = Resources.Load<SpellObject>("FX_Fireball");
        }

        public override void Cast(GameActor caster, Vector3 target)
        {
            Debug.Log("Heal");
        }

        public override void Cast(GameActor caster)
        {
            Debug.Log("Heal");
        }
    }
}

