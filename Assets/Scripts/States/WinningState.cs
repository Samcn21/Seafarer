using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public class WinningState : IStateBase
    {

        private StateManager StateManager;
        public WinningState(StateManager managerRef)
        {
            StateManager = managerRef;
            Debug.Log("Constructing Winning State");
        }

        public void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StateManager.SwitchState(new BeginState(StateManager));
            }
        }

        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate()
        {

        }
    }
}