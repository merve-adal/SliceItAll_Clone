using System.Collections;
using UnityEngine;
using TMPro;

public class CurrencyController : MonoBehaviour
{
    // Toplam para ve kazanýlan para için TextMeshProUGUI bileþenlerine referans
    [SerializeField] private TextMeshProUGUI _totalCurrencyTMP;
    [SerializeField] private TextMeshProUGUI _earnedCurrencyTMP;
    
    private int _totalCurrency; // Toplam para
    private int _earnedCurrencyOnThisLevel;// Bu seviyede kazanýlan para miktarý

    private void OnEnable()
    {
        // Oyun durumu deðiþtiðinde, Sliceable'dan gelen para miktarý bilgisini güncelle
        GameManager.OnStateChanged += CheckGameState;
        Sliceable.OnObjectSliced += UpdateCurrency;

        // UI'yý güncelle
        UpdateUI();
    }

    private void OnDisable()
    {
        // Oyun durumu deðiþtiðinde, Sliceable'dan gelen para miktarý bilgisini güncelleme aboneliðini iptal et
        GameManager.OnStateChanged -= CheckGameState;
        Sliceable.OnObjectSliced -= UpdateCurrency;
    }

    public void ApplyBonus(int multiplier)
    {
        // Bonus'u belirtilen çarpana göre uygula
        StartCoroutine(Bonus(multiplier));
    }

    private IEnumerator Bonus(int multiplier)
    {
        yield return new WaitForSeconds(2f);

        _earnedCurrencyOnThisLevel *= multiplier; // Bu seviyede kazanýlan parayý çarpana göre artýr
        _earnedCurrencyTMP.text = $"+ {_earnedCurrencyOnThisLevel}";  // Kazanýlan para miktarýný UI'ya yaz


        yield return new WaitForSeconds(0.5f);

        _totalCurrency += _earnedCurrencyOnThisLevel; // Toplam parayý güncelle
        _totalCurrencyTMP.text = $"$ {_totalCurrency}"; // Toplam para miktarýný UI'ya yaz
    }

    private void CheckGameState()
    {
        GameState state = GameManager.Instance.CharacterState;

        if (state == GameState.Start)
        {
            _earnedCurrencyOnThisLevel = 0; //yeni seviyede kazanýlan para miktarýný sýfýrla
        }
        else if (state == GameState.Win)
        {
            _earnedCurrencyTMP.text = $"+ {_earnedCurrencyOnThisLevel}"; // Kazanýlan para miktarýný UI'ya yaz
        }
    }

    private void UpdateCurrency(int score)
    {
        _totalCurrency += score;  // Toplam para miktarýný güncelle
        _earnedCurrencyOnThisLevel += score; // Bu seviyede kazanýlan para miktarýný güncelle

        UpdateUI(); //UI'yý güncelle
    }

    private void UpdateUI()
    {
        _totalCurrencyTMP.text = $"$ {_totalCurrency}";  // Toplam para miktarýný UI'ya yaz
    }
}