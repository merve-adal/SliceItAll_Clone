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

    /*bir FinishGround nesnesi ile etkileþime geçen býçaðýn nasýl davranacaðýný belirler. Eðer býçak keskin bir kenara çarptýðýnda OnSharpEdgeHit() fonksiyonu çaðrýlýr ve GameManager'ýn durumunu "Kazanma" olarak deðiþtirir. Ayrýca, CurrencyController'a bonus uygulayarak kazanýlan para miktarýný artýrýr. Eðer býçak sýrtý ile çarparsa OnKnifesBackHit() fonksiyonu çaðrýlýr ve býçaðý geri atar.

_bonusMultiplier, bonus uygulandýðýnda kazanýlacak para miktarýný belirleyen çarpan deðiþkenidir.
_isKnifeStuck, býçaðýn FinishGround'a yapýþýp yapýþmadýðýný belirleyen bir boolean deðiþkendir.
_multiplierTMP, FinishGround nesnesinin altýnda bulunan TextMeshPro nesnesidir ve bonus çarpanýnýn deðerini görüntüler.
_currencyController, CurrencyController nesnesine eriþmek için kullanýlýr.
Kod, IKnifeHit arabirimini de uygular ve bu nedenle OnSharpEdgeHit() ve OnKnifesBackHit() fonksiyonlarý bu arabirimi kullanýr.
    */
}