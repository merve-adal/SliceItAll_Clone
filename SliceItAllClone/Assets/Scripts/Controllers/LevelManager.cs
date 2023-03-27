using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Level prefab'larýný depolamak için dizi kullanýlýr
    [SerializeField] private GameObject[] _levelPrefabs;

    // Þu anki level ve aktif level prefab'ý sýnýf deðiþkenleri olarak tanýmlanýr
    private int _currentLevel;
    private GameObject _activeLevelPrefab;

    // Þu anki level'ý get metodu aracýlýðýyla dýþarýya açar
    public int CurrentLevel => _currentLevel;

    private void Awake()
    {
        // LevelManager örneði oluþturulduðunda LoadLevel() metodu çaðýrýlýr
        LoadLevel();
    }

    // Bir sonraki level'a geçmek için kullanýlan metot
    public void NextLevel()
    {
        // Þu anki level'ý bir artýr ve yeni level'ý yükle
        _currentLevel++;
        LoadLevel();
    }

    // Level'ý yeniden yüklemek için kullanýlan metot
    public void Restart()
    {
        LoadLevel();
    }

    // Level yükleme iþlemleri için kullanýlan metot
    private void LoadLevel()
    {
        // Eðer bir önceki level yüklüyse, onu yok et
        if (_activeLevelPrefab != null)
        {
            Destroy(_activeLevelPrefab);
        }

        // levelToLoad deðiþkeni, _currentLevel deðiþkeninin geçerli aralýkta kalmasýný saðlar
        int levelToLoad = Mathf.Clamp(_currentLevel, 0, _levelPrefabs.Length - 1);

        // levelToLoad'daki prefab'ý aktif hale getirerek yeni bir örneði oluþtur
        _activeLevelPrefab = Instantiate(_levelPrefabs[levelToLoad]);
        _activeLevelPrefab.gameObject.SetActive(true);
    }
}