using System;
using UnityEngine;
using TMPro;

public class Sliceable : MonoBehaviour, IKnifeHit
{
    public static event Action<int> OnObjectSliced;
    
    [SerializeField] private int _score;
    [SerializeField] private ParticleSystem _particle;
    
    private bool _isSliced;
    private Rigidbody[] _rigidbodies;
    private TextMeshProUGUI _scoreTMP;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _scoreTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _scoreTMP.text = $"+{_score}";
        _scoreTMP.gameObject.SetActive(false);
    }

    public void OnSharpEdgeHit(PlayerController playerController)
    {
        if (_isSliced) return;
        _isSliced = true;
        
        Slice();
        _scoreTMP.gameObject.SetActive(true);
        OnObjectSliced?.Invoke(_score);
    }

    public void OnKnifesBackHit(PlayerController playerController)
    {
        if (_isSliced) return;
        
        playerController.JumpBack();
    }

    private void Slice()
    {
        foreach (var part in _rigidbodies)
        {
            part.isKinematic = false;
            part.AddForce(part.transform.localPosition.y * 5000f * Vector3.up);
        }
        
        _particle.Play();
    }

    /*OnObjectSliced adlý bir Action<int> türünde event oluþturulur. Bu event, nesnenin kesildiði zaman çaðrýlacak ve kesildiði zaman kazanýlacak puaný içerecektir.

    _score deðiþkeni, nesnenin kesildiði zaman kazanýlacak puaný belirtir.

    _particle deðiþkeni, nesne kesildiði zaman oynatýlacak patlama efektini belirtir.

    _isSliced deðiþkeni, nesnenin kesilip kesilmediðini belirtir.

    _rigidbodies deðiþkeni, nesnenin alt nesnelerinin Rigidbody bileþenlerini içerir.

    _scoreTMP deðiþkeni, nesnenin altýndaki TextMeshProUGUI bileþenini içerir.

    Awake() fonksiyonu, bileþenlerin referanslarýný alýr.

    OnEnable() fonksiyonu, nesne etkinleþtirildiðinde çalýþýr ve puan textinin ayarlarýný yapar.

    OnSharpEdgeHit() fonksiyonu, oyuncunun keskin býçakla nesneye çarptýðý zaman çalýþýr. Eðer nesne daha önce kesilmiþse, fonksiyon hiçbir iþlem yapmadan çýkar. Eðer nesne daha önce kesilmemiþse, nesneyi keser, puan textini görünür yapar ve OnObjectSliced event'ýný tetikler.

    OnKnifesBackHit() fonksiyonu, oyuncunun býçaðýn arkasýna çarptýðý zaman çalýþýr. Eðer nesne daha önce kesilmiþse, fonksiyon hiçbir iþlem yapmadan çýkar. Eðer nesne daha önce kesilmemiþse, oyuncuyu geriye doðru zýplatýr.

    Slice() fonksiyonu, nesneyi keser ve patlama efektini oynatýr. Bu fonksiyon, _rigidbodies deðiþkenindeki tüm Rigidbody bileþenleri için isKinematic özelliðini kapatýr ve yukarý doðru bir kuvvet uygular.*/

}