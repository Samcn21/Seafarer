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
        }

        public void StateUpdate()
        {
            //Asking pin code in the beginning?
            if (GameManager.Instance.GameStatus(GameData.GameStatus.HasPinCodePanel))
            {
                GUIManager.Instance.PanelPinCode.ShowPanel();
            }
            else
            {
                StateManager.PreActiveState = GameData.GameStates.PinCode;
                StateManager.SwitchState(new ReadyState(StateManager));
            }

            if (GUIManager.Instance.PanelPinCode.canCheckPinCode)
            {
                CheckPinCode();
            }
        }

        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate() 
        { 
        
        }

        private bool IsPinCodeCorrect(string value)
        {
            if (value == GameManager.Instance.GamePinCode())
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
            if (!GameManager.Instance.GameStatus(GameData.GameStatus.ConnectingAdminPanel))
            {
                if (IsPinCodeCorrect(GUIManager.Instance.PanelPinCode.inputPinCode.text.ToString()))
                {
                    //when the pin code is correct the pincode panel must be hidden
                    GUIManager.Instance.PanelPinCode.HidePanel();

                    StateManager.PreActiveState = GameData.GameStates.PinCode;
                    StateManager.SwitchState(new ReadyState(StateManager));
                }
                else
                {
                    GUIManager.Instance.PanelPinCode.EnterPinCodeAgain();
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