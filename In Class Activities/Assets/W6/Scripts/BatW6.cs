using UnityEngine;
using UnityEngine.UI;

// Write the BatW6 class here.
public class BatW6 : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _playerTransform;

    public  void EnableChase(Transform player)
    {    enabled = true;
        _playerTransform = player;
    }
    public void DisableChase()
    {
        enabled = false;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _playerTransform.position,
            _speed * Time.deltaTime
            );
    }
}
