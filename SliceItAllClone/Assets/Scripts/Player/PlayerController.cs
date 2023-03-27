using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Forth")]
    [SerializeField] private Vector3 _jumpForthForce;  // z�plama kuvveti ileri y�nde
    [SerializeField] private Vector3 _spinForthTorque; // d�n�� torku ileri y�nde
    [Header("Jump Back")]
    [SerializeField] private Vector3 _jumpBackForce;   // z�plama kuvveti geri y�nde
    [SerializeField] private Vector3 _spinBackTorque;  // d�n�� torku geri y�nde

    private Rigidbody _rigidbody; // oyun nesnesinin fizik motoru

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // oyun nesnesinin fizik motorunu al
    }

    private void OnEnable()
    {
        InputController.OnTap += OnTapHandler; // giri� kontrolc�s�nde "OnTap" olay�n� dinle
    }

    private void OnDisable()
    {
        InputController.OnTap -= OnTapHandler; // giri� kontrolc�s�nde "OnTap" olay�n� dinlemeyi b�rak
    }

    private void Start()
    {
        GameManager.Instance.SetGameState(GameState.Start); // oyun durumunu ba�lat
    }

    private void FixedUpdate()
    {
        _rigidbody.inertiaTensorRotation = Quaternion.identity; // tenz�r rotasyonunu s�f�rla
    }

    public void Stuck()
    {
        _rigidbody.isKinematic = true; // oyun nesnesinin fizik motorunu kapat
    }

    public void JumpBack()
    {
        Jump(-1); // z�pla (-1) geri y�nde
        Spin(-1); // d�n (-1) geri y�nde
    }

    private void OnTapHandler()
    {
        GameManager.Instance.SetGameState(GameState.InGame); // oyun durumunu "InGame" yap

        _rigidbody.isKinematic = false; // oyun nesnesinin fizik motorunu a�
        Jump(); // z�pla (1) ileri y�nde
        Spin(); // d�n (1) ileri y�nde
    }

    private void Jump(int direction = 1)
    {
        Vector3 jumpForce = direction == 1 ? _jumpForthForce : _jumpBackForce; // z�plama kuvveti

        _rigidbody.velocity = Vector3.zero; // oyun nesnesinin h�z�n� s�f�rla
        _rigidbody.AddForce(jumpForce, ForceMode.Impulse); // z�plama kuvvetini uygula
    }

    private void Spin(int direction = 1)
    {
        Vector3 spinTorque = direction == 1 ? _spinForthTorque : _spinBackTorque; // d�n�� torku

        _rigidbody.angularVelocity = Vector3.zero; // oyun nesnesinin a��sal h�z�n� s�f�rla
        _rigidbody.AddTorque(spinTorque, ForceMode.Acceleration); // d�n�� torkunu uygula
    }
}
