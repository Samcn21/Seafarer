using UnityEngine;
using System.Collections;

public class DiceController : MonoBehaviour
{

    [SerializeField]
    private GameData.DiceType _cityDiceType;
    [SerializeField]
    private GameData.DiceType _playerDiceType;

    private int _cityDiceNumber = 20;
    private int _playerDiceNumber = 10;

    void Start()
    {
        switch (_cityDiceType)
        {
            case GameData.DiceType.Coin:
                _cityDiceNumber = 2;
                break;
            case GameData.DiceType.SixSided:
                _cityDiceNumber = 6;
                break;
            case GameData.DiceType.TenSided:
                _cityDiceNumber = 10;
                break;
            case GameData.DiceType.TweleveSided:
                _cityDiceNumber = 12;
                break;
            case GameData.DiceType.TwentySided:
                _cityDiceNumber = 20;
                break;
        }

        switch (_playerDiceType)
        {
            case GameData.DiceType.Coin:
                _playerDiceNumber = 2;
                break;
            case GameData.DiceType.SixSided:
                _playerDiceNumber = 6;
                break;
            case GameData.DiceType.TenSided:
                _playerDiceNumber = 10;
                break;
            case GameData.DiceType.TweleveSided:
                _playerDiceNumber = 12;
                break;
            case GameData.DiceType.TwentySided:
                _playerDiceNumber = 20;
                break;
        }

    }

    public int GetPlayerDiceNumber()
    {
        return Random.Range(1, _playerDiceNumber + 1);
    }

    public int GetCityDiceNumber()
    {
        return Random.Range(1, _cityDiceNumber + 1);
    }
}
