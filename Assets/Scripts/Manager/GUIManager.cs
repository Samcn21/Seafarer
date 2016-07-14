using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public GameObject panelPinCode;
    public InputField inputPinCode;
    public Text pinCodeMsg;
    public Button btnPinCodeEnter;
    public bool canCheckPinCode { get; private set; }

    void Awake()
    {
        if (!panelPinCode) 
            Debug.LogError("PanelPinCode is not found!");
        
        if (!inputPinCode) 
            Debug.LogError("InputFieldPinCode is not found!");
        
        if (!pinCodeMsg) 
            Debug.LogError("TextPinCodeMessage is not found!");

        if (!btnPinCodeEnter)
            Debug.LogError("ButtonPinCode is not found!");

    }


    public void ReadInputPinCode()
    {
        pinCodeMsg.text = "";

        if (inputPinCode.text.Length == 0)
        {
            pinCodeMsg.text = "Please enter the pin code";
        }
        else if (inputPinCode.text.Length < 4)
        {
            pinCodeMsg.text = "It is a 4-digit pin code";
        }
        else
        {
            canCheckPinCode = true;
            inputPinCode.interactable = false;
            btnPinCodeEnter.interactable = false;
            pinCodeMsg.text = "Please wait...";
        }
    }

    //if the pin code is incorrect!
    public void EnterPinCodeAgain() 
    {
        pinCodeMsg.text = "The Pin Code is incorrect, Please enter again";
        canCheckPinCode = false;
        inputPinCode.interactable = true;
        btnPinCodeEnter.interactable = true;
    }

    public void ShowPinCodePanel() 
    {
        panelPinCode.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void HidePinCodePanel()
    {
        panelPinCode.GetComponent<CanvasGroup>().alpha = 0;
    }
}
