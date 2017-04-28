using System;
using UnityEngine;
using BeheaderTavern.Scripts.Interfaces;
using System.Linq;

namespace BeheaderTavern.Scripts.Mobiles.Player
{
    [Serializable]
    public class PlayerTarget : IPlayerComponent
    {
        private PlayerActor _actor;

        public GameActor target;
        public GameObject targetMarker;
        private bool _hasMarker;

        #region IPlayerComponent implementation
        public void Update()
        {
            if(target != null)
            {
                if(!_hasMarker)
                    AddTargetMarker(target);

                if(!target.InRange(_actor,15f) || !target.CanSee(_actor))
                {
                    RemoveTargetMarker(target);
                    target = null;
                }
            }

           
        }
        public void Start(PlayerActor actor)
        {
            _actor = actor;
            GameObjectPool.Initialize(targetMarker);
        }
        #endregion

        public void LockTarget()
        {
            if(target != null)
            {
                RemoveTargetMarker(target);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 10000))
            {
                var gameActor = hit.collider.GetComponent<GameActor>();
                if (gameActor != null && gameActor != _actor)
                {
                    target = gameActor;

                }
                else
                {
                    if (target != null)
                    {
                        target = null;
                    }
                }
            }
        }

        private void AddTargetMarker(GameActor target)
        {
            targetMarker = GameObjectPool.Spawn(targetMarker);//GameObject.Instantiate(targetMarker, target.transform);
            targetMarker.transform.SetParent(target.transform);
            targetMarker.transform.localPosition = Vector3.up * 0.01f;
            _hasMarker = true;
        }

        private void RemoveTargetMarker(GameActor target)
        {
            var go = target.GetComponentsInChildren<ParticleSystem>();
            for(int i = 0; i < go.Length; i++)
            {
                if(go[i].name.Contains("TargetMarker"))
                {
                    GameObjectPool.Recycle(go[i].gameObject);
                    //GameObject.Destroy(go[i].gameObject);
                }
            }
            _hasMarker = false;
        }


        GameActor GetClosestEnemy(GameActor[] enemies)
        {
            GameActor tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = _actor.transform.position;
            foreach (GameActor t in enemies)
            {
                if (t.name != _actor.name)
                {  
                    float dist = Vector3.Distance(t.transform.position, currentPos);
                    if (dist < minDist)
                    {
                        tMin = t;
                        minDist = dist;
                    }
                }
            }
            return tMin;
        }
       
    }
}

