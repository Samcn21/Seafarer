using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public class ReadyState : IStateBase
    {
        private StateManager StateManager;

        public ReadyState(StateManager managerRef)
        {
            StateManager = managerRef;
            StateManager.CurrentActiveState = GameData.GameStates.Ready;
        }
        public void StateUpdate()
        {

        }
        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate() 
        { 
        
        }

    }
}