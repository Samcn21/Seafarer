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
            Debug.Log("Constructive Function");
        }
        public void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //StateManager.SwitchState(new PlayState(StateManager));
            }
        }

        public void StateFixedUpdate() 
        { 
        
        }

    }
}