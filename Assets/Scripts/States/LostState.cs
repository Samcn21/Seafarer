using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;
namespace Assets.Scripts.States
{
    public class LostState : IStateBase
    {
        private StateManager StateManager;
        public LostState(StateManager managerRef)
        {
            StateManager = managerRef;
            Debug.Log("Constructing Lost State");
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