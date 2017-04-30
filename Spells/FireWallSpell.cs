using System;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Enums;
using UnityEngine;

namespace BeheaderTavern.Scripts.Spells
{
    public class FireWallSpell : SpellBase
    {
        public override SpellProperties SpellProps{ get; set; }

        public override SpellObject SpellObject { get; set; }

        public FireWallSpell()
        {
            SpellObject = Resources.Load<SpellObject>("FX_FireWall");
            SpellProps = new SpellProperties
            {
                Damage = 15,
                SpellDuration = 20,
                CoolDownDuration = 15f,
                StaminaCost = 20f, 
                CastTime = 0.5f,
                Type = ESpellType.TYPE_AREA,
                CoolDownTimer = 15f,
                Icon = Resources.Load<Sprite>("FireWall"),
            };

            GameObjectPool.Initialize(SpellObject.gameObject);
                    
        }


        public override void Cast(GameActor caster, UnityEngine.Vector3 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(target);
            RaycastHit hit;

            LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask.value))
            {
                //hit.point.Set(hit.point.x, 1, hit.point.z);
                SpellObject spellObj = null;
                if (PhotonNetwork.offlineMode)
                {
                    spellObj = GameObjectPool.Spawn(SpellObject.gameObject).GetComponent<SpellObject>();
                }
                else
                {
                    spellObj = PhotonNetwork.Instantiate(SpellObject.gameObject.name, Vector3.zero, Quaternion.identity, 0).GetComponent<SpellObject>();
                }   
                if (!spellObj.photonView.isMine)
                    spellObj.photonView.TransferOwnership(caster.photonView.ownerId);
                spellObj.transform.position = hit.point;
                spellObj.transform.localScale = Vector3.one;
                spellObj.SetProps(this);
                spellObj.transform.LookAt(caster.transform.position);
            }
        }

        public override void Cast(GameActor caster)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                SpellObject spellObj = null;
                if (PhotonNetwork.offlineMode)
                {
                    spellObj = GameObjectPool.Spawn(SpellObject.gameObject).GetComponent<SpellObject>();
                }
                else
                {
                  spellObj = PhotonNetwork.Instantiate(SpellObject.gameObject.name, Vector3.zero, Quaternion.identity, 0).GetComponent<SpellObject>();
                }    
                if (!spellObj.photonView.isMine)
                    spellObj.photonView.TransferOwnership(caster.photonView.ownerId);
                spellObj.transform.position = hit.point;
                spellObj.SetProps(this);
                spellObj.transform.LookAt(caster.transform.position);
            }
        }

        public override void OnTriggerEnter(Collider col)
        {
            
        }
            
    }
}

