using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyEarner : MonoBehaviour
{

    private int _coins;
    [SerializeField] private int _coinsMultiplier;
    [SerializeField] private int _coinsMultiplierCost;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _coinsMultiplierText;
    [SerializeField] private TMP_Text _coinsMultiplierCostText;
    [SerializeField] private GameObject _ClickFX;
    [SerializeField] private RectTransform _buttonPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            _coinsMultiplier = PlayerPrefs.GetInt("CoinsMultiplier");
            _coinsMultiplierCost = PlayerPrefs.GetInt("CoinsMultiplierCost");
            _coinsMultiplierText.text = _coinsMultiplier.ToString();
            _coinsMultiplierCostText.text = _coinsMultiplierCost.ToString();
            _coins = PlayerPrefs.GetInt("Coins");
            _coinsText.text = _coins.ToString();
        }
    }

    public void EarnMoney()
    {
        Instantiate(_ClickFX, _buttonPosition.position.normalized, Quaternion.identity);
        _coins += _coinsMultiplier;
        PlayerPrefs.SetInt("Coins", _coins);
        _coinsText.text = _coins.ToString();
    }

    public void UpgradeClickMultiplier()
    {
        if (_coins >= _coinsMultiplierCost)
        {
            
            _coins -= _coinsMultiplierCost;
            _coinsText.text = _coins.ToString();
            _coinsMultiplier *= 2;
            _coinsMultiplierCost = (_coinsMultiplier * _coinsMultiplierCost);
            PlayerPrefs.SetInt("Coins", _coins);
            PlayerPrefs.SetInt("CoinsMultiplier", _coinsMultiplier);
            PlayerPrefs.SetInt("CoinsMultiplierCost", _coinsMultiplierCost);
            _coinsMultiplierText.text = _coinsMultiplier.ToString();
            _coinsMultiplierCostText.text = _coinsMultiplierCost.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
