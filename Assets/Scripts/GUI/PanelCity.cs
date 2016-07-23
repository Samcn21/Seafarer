using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelCity : MonoBehaviour, IPanelControl
{
    [SerializeField]    
    private GameObject panelCity;

    [SerializeField] 
    private Text CityName;

    [SerializeField]
    private Button btnAttack;

    [SerializeField]
    private GameData.TeamCountry _askerCountry;

    [SerializeField]
    private GameData.City _city;

    void Awake()
    {
        if (!panelCity)
            Debug.LogError("panelCity is not found!");
    }

    public void ShowPanel()
    {
        panelCity.GetComponent<CanvasGroup>().alpha = 1;
        panelCity.GetComponent<CanvasGroup>().interactable = true;
    }

    public void HidePanel()
    {
        panelCity.GetComponent<CanvasGroup>().alpha = 0;
        panelCity.GetComponent<CanvasGroup>().interactable = false;
    }

    public void OpenPanel(GameData.City cityName, GameData.TeamCountry askerCountry)
    {
        ShowPanel();
        CityName.text = cityName.ToString();
        _city = cityName;
        _askerCountry = askerCountry;

        foreach (GameObject city in GameManager.Instance.allCities)
        {
            //if city is under attack or under siege disable the attack button
            if (city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.UnderAttack || city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.UnderSiege)
            {
                btnAttack.enabled = false;
            }
        }

    }

    public void Attack() 
    {
        //find the selected city between all cities
        foreach (GameObject city in GameManager.Instance.allCities)
        {
            //finding the clicked city between all cities
            if (city.GetComponent<CityController>().GetCityName() == _city)
            {
                //check if city is neutral and free
                if (city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.Free && city.GetComponent<CityController>().GetCityStatus() == GameData.CityStatus.Neutral)
                {
                    //change city status to under attack in all network
                    city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.UnderAttack);
                    //TODO: change player status from exploring to attacking

                    //TODO (Singular attack):
                    //4.  HidePanel(); and open question panel
                    GUIManager.Instance.PanelQuestion.OpenPanel(_askerCountry, _city);
                    HidePanel();
                }
            }
        }

    }

    [PunRPC]
    private void CityStatus()
    { 
        //TODO:
        //before a team attack they need to update the city status on their machine too 
    }
}
