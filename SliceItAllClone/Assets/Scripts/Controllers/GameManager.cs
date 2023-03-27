using System;
using UnityEngine;

// GameManager s�n�f�, di�er kodlardan �nce �al��t�r�l�r
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static event Action OnStateChanged;

    // Singleton tasar�m kal�b� uygulan�r: GameManager �rne�i tek bir �rnek olarak kullan�l�r
    public static GameManager Instance { get; private set; }

    // Karakterin mevcut durumunu temsil eden bir oyun durumu nesnesi
    public GameState CharacterState { get; private set; }

    private void Awake()
    {
        // GameManager �rne�i zaten varsa, bu yeni �rne�i yok et
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // GameManager �rne�i yoksa, bu �rne�i kullan
            Instance = this;
        }
    }

    // Karakterin mevcut durumunu ayarlamak i�in kullan�lan bir metot
    public void SetGameState(GameState state)
    {
        // Yeni durum, mevcut durumla ayn�ysa i�lem yapma
        if (state == CharacterState) return;
        // Yeni durumu mevcut durum olarak ayarla
        CharacterState = state;
        // Oyun durumu de�i�ti�inde, OnStateChanged event'ini �a��r
        OnStateChanged?.Invoke();
    }
}