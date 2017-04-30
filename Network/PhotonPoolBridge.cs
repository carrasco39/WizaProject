using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeheaderTavern.Scripts.Network
{
    
    public class PhotonPoolBridge : MonoBehaviour, IPunPrefabPool
    {
        void Start()
        {
            PhotonNetwork.PrefabPool = this;
        }
        #region IPunPrefabPool implementation
        public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
        {
            
            GameObject go = GameObjectPool.Spawn(Resources.Load<GameObject>(prefabId));
            go.transform.position = position;
            go.transform.rotation = rotation;

            return go;
        }
        public void Destroy(GameObject gameObject)
        {
            GameObjectPool.Recycle(gameObject);
        }
        #endregion
    }

}