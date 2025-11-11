using UnityEngine;

public class MuskratW7 : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce = 5.0f;
    private bool _orbitMode;
    private Transform _sphereTransform;

    // ------------------------------------------------------------------------
    private void Update()
    {
        if (_orbitMode)
        {
            MoveOrbitMode();
        }
        else
        {
            MoveNormal();
        }
        Jump();
    }

    // ------------------------------------------------------------------------
    private void MoveOrbitMode()
    {
        // STEP 3 -------------------------------------------------------------
        float leftright = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        // 前后移动：绕球体“横向”滚动（用本地 right 轴，转世界空间）
        Vector3 rightAxis = transform.TransformDirection(Vector3.right);
        transform.RotateAround(
            _sphereTransform.position,
            rightAxis,
            forward * _rotationSpeed * Time.deltaTime
        );

        // 左右移动：绕球体“纵向”走圈（用本地 up 轴，转世界空间）
        Vector3 upAxis = transform.TransformDirection(Vector3.up);
        transform.RotateAround(
            _sphereTransform.position,
            upAxis,
            -leftright * _rotationSpeed * Time.deltaTime  // 负号让左转为顺时针
        );
        // STEP 3 -------------------------------------------------------------

        // STEP 5 -------------------------------------------------------------
        bool isMoving = Mathf.Abs(forward) > 0.1f || Mathf.Abs(leftright) > 0.1f;
        _animator.SetBool("flying", false);                    // 在泡泡上不飞
        _animator.SetBool("running", isMoving);                // 有输入就跑
        // STEP 5 -------------------------------------------------------------
    }

    // ------------------------------------------------------------------------
    private void MoveNormal()
    {
        // STEP 1 -------------------------------------------------------------
        float leftright = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, leftright * _rotationSpeed * Time.deltaTime);
        // STEP 1 -------------------------------------------------------------

        // STEP 2 -------------------------------------------------------------
        float movement = Input.GetAxis("Vertical");
        transform.position += transform.forward * movement * _moveSpeed * Time.deltaTime;
        // STEP 2 -------------------------------------------------------------

        // STEP 4 -------------------------------------------------------------
        float speed = _rigidbody.linearVelocity.magnitude;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f); // 简单地面检测
        _animator.SetBool("running", speed > 0.1f && isGrounded);
        _animator.SetBool("flying", speed > 0.1f && !isGrounded);
        // STEP 4 -------------------------------------------------------------
    }

    // ------------------------------------------------------------------------
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            if (_sphereTransform != null)
            {
                Destroy(_sphereTransform.gameObject);
                _sphereTransform = null;
            }
            _orbitMode = false;
        }
    }

    // ------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            _orbitMode = true;
            _rigidbody.isKinematic = true;
            _sphereTransform = collision.transform;
            ContactPoint contact = collision.GetContact(0);
            Vector3 tangent = Vector3.Cross(Vector3.right, contact.normal);
            transform.SetPositionAndRotation(
                contact.point,
                Quaternion.LookRotation(tangent, contact.normal)
            );
        }
    }
}