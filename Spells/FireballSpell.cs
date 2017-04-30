using System;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Enums;
using BeheaderTavern.Scripts.Interfaces;
using System.Collections;

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
            if (!PhotonNetwork.offlineMode)
            {
                _spellObject = PhotonNetwork.Instantiate(SpellObject.name, 
                    Vector3.zero, Quaternion.identity, 0)
                    .GetComponent<SpellObject>();
            }
            else
            {
                _spellObject = GameObjectPool.Spawn(SpellObject.gameObject).GetComponent<SpellObject>();
            }
            _spellObject.SetProps(this);
            if (!_spellObject.photonView.isMine)
                _spellObject.photonView.TransferOwnership(_caster.photonView.ownerId);
            _spellObject.transform.position = caster.transform.position + caster.transform.forward * 1.5f;
            _spellObject.transform.localScale = Vector3.one;
            _spellObject.transform.LookAt(target);
            _spellObject.StartCoroutine(MoveSpell());

        }

        public override void Cast(GameActor caster)
        {
            
        }

        #endregion

        private IEnumerator MoveSpell()
        {
            _spellObject.transform.position += _spellObject.transform.forward * 40f * Time.deltaTime;
            yield return new WaitForSeconds(1f / 60f);
            yield return MoveSpell();
        }

        public override void OnTriggerEnter(Collider col)
        {
            if ((col.GetComponent<GameActor>() && col.gameObject != _caster.gameObject) || col.tag == "Obstacle")
            {
                _spellObject.StopAllCoroutines();
                _spellObject.collided = true;
            }
        }
    }
}

