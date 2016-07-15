using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    private static GUIManager instance = null;
    public static GUIManager Instance
    {
        get
        {
            return instance;
        }
    }

    //Script Refrences
    public PanelPinCode PanelPinCode;

    //Controller variables
    [SerializeField]
    private bool showAllPanel = false; //Demo

    [SerializeField]
    private GameObject[] allPanels;

    public void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);

        if (!PanelPinCode)
            Debug.LogError("PanelPinCode not found");
    }

    void Start()
    {
        allPanels = GameObject.FindGameObjectsWithTag("Panel");
    }


    public void ShowAllPanels()
    {
        foreach (GameObject panel in allPanels)
        {
            panel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void HideAllPanels()
    {
        foreach (GameObject panel in allPanels)
        {
            panel.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
