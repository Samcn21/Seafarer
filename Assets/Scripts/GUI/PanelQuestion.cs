using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelQuestion : MonoBehaviour, IPanelControl
{
    [SerializeField]
    private GameObject panelQuestion;

    [SerializeField]
    private Text question;

    [SerializeField]
    private Text choiceA;

    [SerializeField]
    private Text choiceB;

    [SerializeField]
    private Text choiceC;

    [SerializeField]
    private Button btnChoiceA;

    [SerializeField]
    private Button btnChoiceB;

    [SerializeField]
    private Button btnChoiceC;

    void Awake()
    {
        if (!panelQuestion)
            Debug.LogError("panelQuestion is not found!");
    }

    public void ShowPanel()
    {
        panelQuestion.GetComponent<CanvasGroup>().alpha = 1;
        panelQuestion.GetComponent<CanvasGroup>().interactable = true;
        GameManager.Instance.SetPlayerInteract(false);
    }

    public void HidePanel()
    {
        panelQuestion.GetComponent<CanvasGroup>().alpha = 0;
        panelQuestion.GetComponent<CanvasGroup>().interactable = false;
        GameManager.Instance.SetPlayerInteract(true);
    }


}
