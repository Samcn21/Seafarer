using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    private static GUIManager _instance = null;
    public static GUIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    //Script Refrences
    public PanelPinCode PanelPinCode;
    public PanelConnectionStatus PanelConnectionStatus;
    public PanelSelectCountry PanelSelectCountry;
    public PanelReady PanelReady;
    public PanelCity PanelCity;
    public PanelQuestion PanelQuestion;


    //Controller variables
    [SerializeField]
    private bool _showAllPanel = false; //Demo

    [SerializeField]
    private GameObject[] _allPanels;

    public void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);

        if (!PanelPinCode)
            Debug.LogError("PanelPinCode not found");
    }

    void Start()
    {
        _allPanels = GameObject.FindGameObjectsWithTag("Panel");
    }


    public void ShowAllPanels()
    {
        foreach (GameObject panel in _allPanels)
        {
            panel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void HideAllPanels()
    {
        foreach (GameObject panel in _allPanels)
        {
            panel.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
