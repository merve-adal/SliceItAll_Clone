using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Level prefab'lar�n� depolamak i�in dizi kullan�l�r
    [SerializeField] private GameObject[] _levelPrefabs;

    // �u anki level ve aktif level prefab'� s�n�f de�i�kenleri olarak tan�mlan�r
    private int _currentLevel;
    private GameObject _activeLevelPrefab;

    // �u anki level'� get metodu arac�l���yla d��ar�ya a�ar
    public int CurrentLevel => _currentLevel;

    private void Awake()
    {
        // LevelManager �rne�i olu�turuldu�unda LoadLevel() metodu �a��r�l�r
        LoadLevel();
    }

    // Bir sonraki level'a ge�mek i�in kullan�lan metot
    public void NextLevel()
    {
        // �u anki level'� bir art�r ve yeni level'� y�kle
        _currentLevel++;
        LoadLevel();
    }

    // Level'� yeniden y�klemek i�in kullan�lan metot
    public void Restart()
    {
        LoadLevel();
    }

    // Level y�kleme i�lemleri i�in kullan�lan metot
    private void LoadLevel()
    {
        // E�er bir �nceki level y�kl�yse, onu yok et
        if (_activeLevelPrefab != null)
        {
            Destroy(_activeLevelPrefab);
        }

        // levelToLoad de�i�keni, _currentLevel de�i�keninin ge�erli aral�kta kalmas�n� sa�lar
        int levelToLoad = Mathf.Clamp(_currentLevel, 0, _levelPrefabs.Length - 1);

        // levelToLoad'daki prefab'� aktif hale getirerek yeni bir �rne�i olu�tur
        _activeLevelPrefab = Instantiate(_levelPrefabs[levelToLoad]);
        _activeLevelPrefab.gameObject.SetActive(true);
    }
}