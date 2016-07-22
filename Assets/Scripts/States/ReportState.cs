using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public class ReportState : IStateBase
    {
        private StateManager StateManager;
        public ReportState(StateManager managerRef)
        {
            StateManager = managerRef;
        }
        public void StateUpdate()
        {

            //    //StateManager.SwitchState(new PlayState(StateManager));

        }
        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate() 
        { 
        
        }

    }
}