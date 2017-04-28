using UnityEngine;
using System.Collections;
using BeheaderTavern.Scripts.Mobiles;

namespace BeheaderTavern.Scripts.Spells
{
    
    [System.Serializable]
    public abstract class SpellBase
    {
        public abstract SpellProperties SpellProps { get; set; }
        public abstract SpellObject SpellObject { get; set; }

        public abstract void Cast(GameActor caster, Vector3 target);

        public abstract void Cast(GameActor caster);
        public virtual void OnTriggerEnter(Collider col){}
        public virtual void OnTriggerStay(Collider col){}
        public virtual void OnTriggerExit(Collider col){}


    }
}

