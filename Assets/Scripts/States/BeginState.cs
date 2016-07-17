using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public class BeginState : IStateBase
    {
        private StateManager StateManager;
        public BeginState(StateManager managerRef)
        {
            StateManager = managerRef;
            StateManager.CurrentActiveState = GameData.GameStates.Begin;
        }

        public void StateUpdate()
        {
            StateManager.PreActiveState = GameData.GameStates.Begin;
            StateManager.SwitchState(new PinCodeState(StateManager));

                    //TODO:
                    // I should make a class that checks internet connection and returns true/false
                    // the internet connection check must check the connection every 20 seconds
                    // there must be another class to check GPS every 30-60 seconds
                    //StateManager.SwitchState(new EnterPinCode(StateManager));
        }

        public void StateShowGUI() 
        {
        
        }
        public void StateFixedUpdate() 
        { 
        
        }

    }
}