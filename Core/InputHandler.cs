using System;
using BeheaderTavern.Scripts.Commands;
using UnityEngine;
using BeheaderTavern.Scripts.Mobiles.Player;

namespace BeheaderTavern.Scripts.Core
{
    public class InputHandler
    {
        private float _dblClickDelay = 0.2f;
        private float _dblClickTimer;
        private bool _oneClicked;
        private bool _dblClicked;

        #region command members
        private Command _mouse0;
        private Command _mouse1;
        private Command _lShift;
        private Command _dblClickCommand;
        private Command _keyQ;
        private Command _keyE;
        #endregion

        public InputHandler()
        {
            _mouse1 = new MoveCommand();
            _mouse0 = new CastSpellCommand();
            _lShift = new DashCommand();
            _dblClickCommand = new LockTargetCommand();
            _keyQ = new NextSpellSelectCommand();
            _keyE = new PriorSpellSelectCommand();
        }

        public void HandleInput()
        {
            var player = GameManager.instance.PlayerActor as PlayerActor;

            if(_oneClicked)
            {
                _dblClickTimer += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    _dblClicked = true;
            }

            if (!_oneClicked && Input.GetKeyDown(KeyCode.Mouse0))
            {
                _oneClicked = true;

            }


            if (_dblClicked)
            {
                _dblClickCommand.Execute(player.playerTarget);
                ResetClicked();
            }

            if (_oneClicked && _dblClickDelay < _dblClickTimer && !_dblClicked)
            {
                _mouse0.Execute(player.playerCastSpell);
                ResetClicked();
            }
            if (Input.GetKey(KeyCode.LeftShift)) _lShift.Execute(player.playerDash); //Removing Dashing
            if (Input.GetKey(KeyCode.Mouse1))  _mouse1.Execute(player.playerMovement);

            if (player.state != BeheaderTavern.Scripts.Mobiles.Player.PlayerActor.EState.STATE_CASTING)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    _keyQ.Execute(player.playerSpellSelector);
                if (Input.GetKeyDown(KeyCode.E))
                    _keyE.Execute(player.playerSpellSelector);
            }
        }


        void ResetClicked()
        {
            _dblClickTimer = 0;
            _oneClicked = false;
            _dblClicked = false;
        }
    }
}

