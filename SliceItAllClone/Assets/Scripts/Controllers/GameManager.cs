using System;
using UnityEngine;

// GameManager sýnýfý, diðer kodlardan önce çalýþtýrýlýr
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static event Action OnStateChanged;

    // Singleton tasarým kalýbý uygulanýr: GameManager örneði tek bir örnek olarak kullanýlýr
    public static GameManager Instance { get; private set; }

    // Karakterin mevcut durumunu temsil eden bir oyun durumu nesnesi
    public GameState CharacterState { get; private set; }

    private void Awake()
    {
        // GameManager örneði zaten varsa, bu yeni örneði yok et
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // GameManager örneði yoksa, bu örneði kullan
            Instance = this;
        }
    }

    // Karakterin mevcut durumunu ayarlamak için kullanýlan bir metot
    public void SetGameState(GameState state)
    {
        // Yeni durum, mevcut durumla aynýysa iþlem yapma
        if (state == CharacterState) return;
        // Yeni durumu mevcut durum olarak ayarla
        CharacterState = state;
        // Oyun durumu deðiþtiðinde, OnStateChanged event'ini çaðýr
        OnStateChanged?.Invoke();
    }
}