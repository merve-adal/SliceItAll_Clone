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

    /*OnObjectSliced adl� bir Action<int> t�r�nde event olu�turulur. Bu event, nesnenin kesildi�i zaman �a�r�lacak ve kesildi�i zaman kazan�lacak puan� i�erecektir.

    _score de�i�keni, nesnenin kesildi�i zaman kazan�lacak puan� belirtir.

    _particle de�i�keni, nesne kesildi�i zaman oynat�lacak patlama efektini belirtir.

    _isSliced de�i�keni, nesnenin kesilip kesilmedi�ini belirtir.

    _rigidbodies de�i�keni, nesnenin alt nesnelerinin Rigidbody bile�enlerini i�erir.

    _scoreTMP de�i�keni, nesnenin alt�ndaki TextMeshProUGUI bile�enini i�erir.

    Awake() fonksiyonu, bile�enlerin referanslar�n� al�r.

    OnEnable() fonksiyonu, nesne etkinle�tirildi�inde �al���r ve puan textinin ayarlar�n� yapar.

    OnSharpEdgeHit() fonksiyonu, oyuncunun keskin b��akla nesneye �arpt��� zaman �al���r. E�er nesne daha �nce kesilmi�se, fonksiyon hi�bir i�lem yapmadan ��kar. E�er nesne daha �nce kesilmemi�se, nesneyi keser, puan textini g�r�n�r yapar ve OnObjectSliced event'�n� tetikler.

    OnKnifesBackHit() fonksiyonu, oyuncunun b��a��n arkas�na �arpt��� zaman �al���r. E�er nesne daha �nce kesilmi�se, fonksiyon hi�bir i�lem yapmadan ��kar. E�er nesne daha �nce kesilmemi�se, oyuncuyu geriye do�ru z�plat�r.

    Slice() fonksiyonu, nesneyi keser ve patlama efektini oynat�r. Bu fonksiyon, _rigidbodies de�i�kenindeki t�m Rigidbody bile�enleri i�in isKinematic �zelli�ini kapat�r ve yukar� do�ru bir kuvvet uygular.*/

}