using System;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Enums;
using BeheaderTavern.Scripts.Interfaces;

namespace BeheaderTavern.Scripts.Spells
{
    using UnityEngine;

    public class FireballSpell : SpellBase
    {

        private SpellObject _spellObject;
        private GameActor _caster;
        public FireballSpell()
        {
            SpellProps = new SpellProperties
            {
                Damage = 5,
                SpellDuration = 3,
                CoolDownDuration = 5f,
                StaminaCost = 10f, 
                CastTime = 0.5f,
                CoolDownTimer = 5f,
                Type = ESpellType.TYPE_RANGED,
                Icon = Resources.Load<Sprite>("Fireball"),
                
            };
                    
                    
            SpellObject = Resources.Load<SpellObject>("FX_Fireball");
            GameObjectPool.Initialize(SpellObject.gameObject);
        }



        ~FireballSpell()
        {
        }

        #region implemented abstract members of SpellBase

        public override SpellProperties SpellProps{ get; set; }

        public override SpellObject SpellObject{ get ; set; }


        public override void Cast(GameActor caster, Vector3 target)
        {
            _caster = caster;
            var spellObj = GameObjectPool.Spawn(SpellObject.gameObject).GetComponent<SpellObject>();//GameObject.Instantiate<SpellObject>(SpellObject, caster.transform.position, Quaternion.identity);
            spellObj.transform.localScale = Vector3.one * 0.2f;
            spellObj.transform.position = caster.transform.position;
            spellObj.SetProps(this);
            spellObj.transform.LookAt(target);
            spellObj.rb.AddForce(spellObj.transform.forward * 1000f);
            _spellObject = spellObj;
        }

        public override void Cast(GameActor caster)
        {
        }

        #endregion

        public override void OnTriggerEnter(Collider col)
        {
            if((col.GetComponent<GameActor>() && col.gameObject != _caster.gameObject ) || col.tag == "Obstacle")
            {
                _spellObject.collided = true;
            }
        }
    }
}

