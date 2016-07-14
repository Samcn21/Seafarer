using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public class PinCodeState : IStateBase
    {
        private StateManager StateManager;

        public PinCodeState(StateManager managerRef)
        {
            StateManager = managerRef;
            StateManager.CurrentActiveState = GameData.GameStates.PinCode;
            GameManager.Instance.GUIManager.ShowPinCodePanel();
        }

        public void StateUpdate()
        {

        }

        public void StateShowGUI()
        {
            if (GameManager.Instance.GUIManager.canCheckPinCode)
            {
                CheckPinCode();
            }
        }

        public void StateFixedUpdate() 
        { 
        
        }

        private bool IsPinCodeCorrect(string value)
        {
            if (value == GameManager.Instance.pinCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckPinCode() 
        {
            if (!GameManager.Instance.connectingAdminPanel)
            {
                if (IsPinCodeCorrect(GameManager.Instance.GUIManager.inputPinCode.text.ToString()))
                {
                    //when the pin code is correct the pincode panel must be hidden
                    GameManager.Instance.GUIManager.HidePinCodePanel();

                    StateManager.PreActiveState = GameData.GameStates.PinCode;
                    StateManager.SwitchState(new ReadyState(StateManager));
                }
                else
                {
                    GameManager.Instance.GUIManager.EnterPinCodeAgain();
                }
            }
            else
            {
                //TODO
                //connect to the admin panel and get the JSON file and check the pin code
            }
        }
    }
}