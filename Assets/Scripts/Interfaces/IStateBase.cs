using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Interfaces
{
    public interface IStateBase {
        //for update
        void StateUpdate();
        //for fixed update
        void StateFixedUpdate();
        //for OnGUI
        void StateShowGUI();
    }
}