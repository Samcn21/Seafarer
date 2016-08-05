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
    public PanelAllianceInvitation PanelAllianceInvitation;
    public PanelInfo PanelInfo;
    public PanelSiege PanelSiege;
    public PanelHUD PanelHUD;


    //Controller variables
    [SerializeField]
    private bool _showAllPanel = false; //Demo

    [SerializeField]
    private float _waitForSeconds = 0.2f;

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
        _waitForSeconds = 0.1f;
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
    public bool IsAnyPanelOpen()
    {
        foreach (GameObject panel in _allPanels)
        {
            if (panel.GetComponent<CanvasGroup>().alpha == 1 && panel.name != "PanelConnectionStatus" && panel.name != "PanelHUD")
            {
                StartCoroutine(WaitForInteraction(_waitForSeconds));
                return true;
            }
        }
        StartCoroutine(WaitForInteraction(_waitForSeconds));
        return false;
    }

    IEnumerator WaitForInteraction(float value)
    {
        yield return new WaitForSeconds(value);
    }

    public void SetWaitForSeconds(float value) 
    {
        _waitForSeconds = value;
    }
}
