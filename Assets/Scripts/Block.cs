using UnityEngine;
public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _strength = 10;
    [SerializeField] private Collider _colider;
    [SerializeField] private Vector3 _dropDirection;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.AddForce(_dropDirection * _strength, ForceMode.Impulse);
        }
    }
}
