using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Forth")]
    [SerializeField] private Vector3 _jumpForthForce;  // zýplama kuvveti ileri yönde
    [SerializeField] private Vector3 _spinForthTorque; // dönüþ torku ileri yönde
    [Header("Jump Back")]
    [SerializeField] private Vector3 _jumpBackForce;   // zýplama kuvveti geri yönde
    [SerializeField] private Vector3 _spinBackTorque;  // dönüþ torku geri yönde

    private Rigidbody _rigidbody; // oyun nesnesinin fizik motoru

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // oyun nesnesinin fizik motorunu al
    }

    private void OnEnable()
    {
        InputController.OnTap += OnTapHandler; // giriþ kontrolcüsünde "OnTap" olayýný dinle
    }

    private void OnDisable()
    {
        InputController.OnTap -= OnTapHandler; // giriþ kontrolcüsünde "OnTap" olayýný dinlemeyi býrak
    }

    private void Start()
    {
        GameManager.Instance.SetGameState(GameState.Start); // oyun durumunu baþlat
    }

    private void FixedUpdate()
    {
        _rigidbody.inertiaTensorRotation = Quaternion.identity; // tenzör rotasyonunu sýfýrla
    }

    public void Stuck()
    {
        _rigidbody.isKinematic = true; // oyun nesnesinin fizik motorunu kapat
    }

    public void JumpBack()
    {
        Jump(-1); // zýpla (-1) geri yönde
        Spin(-1); // dön (-1) geri yönde
    }

    private void OnTapHandler()
    {
        GameManager.Instance.SetGameState(GameState.InGame); // oyun durumunu "InGame" yap

        _rigidbody.isKinematic = false; // oyun nesnesinin fizik motorunu aç
        Jump(); // zýpla (1) ileri yönde
        Spin(); // dön (1) ileri yönde
    }

    private void Jump(int direction = 1)
    {
        Vector3 jumpForce = direction == 1 ? _jumpForthForce : _jumpBackForce; // zýplama kuvveti

        _rigidbody.velocity = Vector3.zero; // oyun nesnesinin hýzýný sýfýrla
        _rigidbody.AddForce(jumpForce, ForceMode.Impulse); // zýplama kuvvetini uygula
    }

    private void Spin(int direction = 1)
    {
        Vector3 spinTorque = direction == 1 ? _spinForthTorque : _spinBackTorque; // dönüþ torku

        _rigidbody.angularVelocity = Vector3.zero; // oyun nesnesinin açýsal hýzýný sýfýrla
        _rigidbody.AddTorque(spinTorque, ForceMode.Acceleration); // dönüþ torkunu uygula
    }
}
