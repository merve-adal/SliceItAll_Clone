using UnityEngine;
using TMPro;

public class FinishGround : MonoBehaviour, IKnifeHit
{
    [SerializeField] private int _bonusMultiplier = 1;
    
    private bool _isKnifeStuck;

    private TextMeshProUGUI _multiplierTMP;
    private CurrencyController _currencyController;

    private void Awake()
    {
        _multiplierTMP = GetComponentInChildren<TextMeshProUGUI>();
        _currencyController = FindObjectOfType<CurrencyController>();
    }

    private void Start()
    {
        if (_multiplierTMP != null)
        {
            _multiplierTMP.text = $"X{_bonusMultiplier}";
        }
    }

    public void OnSharpEdgeHit(PlayerController playerController)
    {
        if (_isKnifeStuck || GameManager.Instance.CharacterState == GameState.Win) return;
        _isKnifeStuck = true;
        
        playerController.Stuck();
        
        GameManager.Instance.SetGameState(GameState.Win);
        _currencyController.ApplyBonus(_bonusMultiplier);
    }

    public void OnKnifesBackHit(PlayerController playerController)
    {
        if (_isKnifeStuck) return;
        
        playerController.JumpBack();
    }

    /*bir FinishGround nesnesi ile etkile�ime ge�en b��a��n nas�l davranaca��n� belirler. E�er b��ak keskin bir kenara �arpt���nda OnSharpEdgeHit() fonksiyonu �a�r�l�r ve GameManager'�n durumunu "Kazanma" olarak de�i�tirir. Ayr�ca, CurrencyController'a bonus uygulayarak kazan�lan para miktar�n� art�r�r. E�er b��ak s�rt� ile �arparsa OnKnifesBackHit() fonksiyonu �a�r�l�r ve b��a�� geri atar.

_bonusMultiplier, bonus uyguland���nda kazan�lacak para miktar�n� belirleyen �arpan de�i�kenidir.
_isKnifeStuck, b��a��n FinishGround'a yap���p yap��mad���n� belirleyen bir boolean de�i�kendir.
_multiplierTMP, FinishGround nesnesinin alt�nda bulunan TextMeshPro nesnesidir ve bonus �arpan�n�n de�erini g�r�nt�ler.
_currencyController, CurrencyController nesnesine eri�mek i�in kullan�l�r.
Kod, IKnifeHit arabirimini de uygular ve bu nedenle OnSharpEdgeHit() ve OnKnifesBackHit() fonksiyonlar� bu arabirimi kullan�r.
    */
}