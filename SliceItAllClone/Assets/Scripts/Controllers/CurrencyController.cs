using System.Collections;
using UnityEngine;
using TMPro;

public class CurrencyController : MonoBehaviour
{
    // Toplam para ve kazan�lan para i�in TextMeshProUGUI bile�enlerine referans
    [SerializeField] private TextMeshProUGUI _totalCurrencyTMP;
    [SerializeField] private TextMeshProUGUI _earnedCurrencyTMP;
    
    private int _totalCurrency; // Toplam para
    private int _earnedCurrencyOnThisLevel;// Bu seviyede kazan�lan para miktar�

    private void OnEnable()
    {
        // Oyun durumu de�i�ti�inde, Sliceable'dan gelen para miktar� bilgisini g�ncelle
        GameManager.OnStateChanged += CheckGameState;
        Sliceable.OnObjectSliced += UpdateCurrency;

        // UI'y� g�ncelle
        UpdateUI();
    }

    private void OnDisable()
    {
        // Oyun durumu de�i�ti�inde, Sliceable'dan gelen para miktar� bilgisini g�ncelleme aboneli�ini iptal et
        GameManager.OnStateChanged -= CheckGameState;
        Sliceable.OnObjectSliced -= UpdateCurrency;
    }

    public void ApplyBonus(int multiplier)
    {
        // Bonus'u belirtilen �arpana g�re uygula
        StartCoroutine(Bonus(multiplier));
    }

    private IEnumerator Bonus(int multiplier)
    {
        yield return new WaitForSeconds(2f);

        _earnedCurrencyOnThisLevel *= multiplier; // Bu seviyede kazan�lan paray� �arpana g�re art�r
        _earnedCurrencyTMP.text = $"+ {_earnedCurrencyOnThisLevel}";  // Kazan�lan para miktar�n� UI'ya yaz


        yield return new WaitForSeconds(0.5f);

        _totalCurrency += _earnedCurrencyOnThisLevel; // Toplam paray� g�ncelle
        _totalCurrencyTMP.text = $"$ {_totalCurrency}"; // Toplam para miktar�n� UI'ya yaz
    }

    private void CheckGameState()
    {
        GameState state = GameManager.Instance.CharacterState;

        if (state == GameState.Start)
        {
            _earnedCurrencyOnThisLevel = 0; //yeni seviyede kazan�lan para miktar�n� s�f�rla
        }
        else if (state == GameState.Win)
        {
            _earnedCurrencyTMP.text = $"+ {_earnedCurrencyOnThisLevel}"; // Kazan�lan para miktar�n� UI'ya yaz
        }
    }

    private void UpdateCurrency(int score)
    {
        _totalCurrency += score;  // Toplam para miktar�n� g�ncelle
        _earnedCurrencyOnThisLevel += score; // Bu seviyede kazan�lan para miktar�n� g�ncelle

        UpdateUI(); //UI'y� g�ncelle
    }

    private void UpdateUI()
    {
        _totalCurrencyTMP.text = $"$ {_totalCurrency}";  // Toplam para miktar�n� UI'ya yaz
    }
}