using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelReady : MonoBehaviour, IPanelControl
{
    public GameObject panelReady;
    public Text readyMsg;
    public Text countdownTimer;

    [SerializeField]
    private float _waitForSeconds = 3;

    [SerializeField]
    private bool _canCountdownStart = false;

    [SerializeField]
    private GameObject[] _allFlags;
    void Awake()
    {
        if (!panelReady)
            Debug.LogError("panelReady is not found!");
    }

    void Update() 
    {
        if (_canCountdownStart)
        {
            _waitForSeconds -= Time.deltaTime;
        }
    }

    public void ShowPanel()
    {
        panelReady.GetComponent<CanvasGroup>().alpha = 1;
        panelReady.GetComponent<CanvasGroup>().interactable = true;
    }

    public void HidePanel()
    {
        panelReady.GetComponent<CanvasGroup>().alpha = 0;
        panelReady.GetComponent<CanvasGroup>().interactable = false;
    }

    public void ShowPanelChosenCountry(GameData.TeamCountry team)
    {
        ShowPanel();
        _allFlags = GameObject.FindGameObjectsWithTag("ReadyCountryFlag");

        foreach (GameObject flag in _allFlags)
        {
            if (!flag.name.Contains(team.ToString()))
                flag.GetComponent<Image>().enabled = false;
        }

    }

    public bool CanPlayNow()
    {
        if (NetworkManager.Instance.IsMinimumTeamsValid())
        {
            //TODO:
            //if useing GPS need to warm up and wait for x seconds on all devices
            //or recieve ready signal from all devices
            readyMsg.text = "";
            countdownTimer.text = "Start in " + (int)_waitForSeconds + " Seconds";

            _canCountdownStart = true;

            if (_waitForSeconds <= 0)
            {
                _canCountdownStart = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            readyMsg.text = "Waiting for other teams to join...";
            return false;
        }
    }
}
