using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeheaderTavern.Scripts.Mobiles;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {

        private RtsCamera _rtsCam;
        private InputHandler _inputHandler;
        private GameActor _playerActor;

        public GameActor PlayerActor
        { 
            get
            {
                return _playerActor;
            }
            set
            {
                _playerActor = value;
                if(_rtsCam == null)
                {
                    _rtsCam = Camera.main.GetComponent<RtsCamera>();
                }
                _rtsCam.Follow(_playerActor.transform);
            }
        }
        #region SINGLETON

        private static GameManager instance_;
        private static object _lock = new object();

        public static GameManager instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance_ == null)
                    {
                        instance_ = FindObjectOfType<GameManager>();
                        if (FindObjectsOfType<GameManager>().Length > 1)
                        {
                            Debug.LogError("There is more than one GameManagers in Scene");
                            return instance_;
                        }

                        if (instance_ == null)
                        {
                            GameObject singleton = new GameObject("GameManager");
                            instance_ = singleton.AddComponent<GameManager>();
                            DontDestroyOnLoad(singleton);
                        }

                    }

                    return instance_;
                }
            }
        }

        #endregion


        #region MONOBEHAVIOR CALLBACKS

        // Use this for initialization
        private void Start()
        {
            _inputHandler = new InputHandler();
            _rtsCam = Camera.main.GetComponent<RtsCamera>();
        	
        }
	
        // Update is called once per frame
        private void Update()
        {
            if (_inputHandler != null && PlayerActor != null)
                _inputHandler.HandleInput();
        }

        #endregion

        #region public methods

        #endregion
    }
}
