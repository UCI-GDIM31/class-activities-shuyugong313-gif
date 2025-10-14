using TMPro;
using UnityEngine;

public class BallW3 : MonoBehaviour
{
    public SpriteRenderer ballRenderer; // ��CatW3������ɫ���豣��public
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speedMultiplier = 1.0f;
    [SerializeField] private float _speedThreshold = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // STEP 1����ײ���ٶȳ��Գ���
        _rigidbody.linearVelocity *= _speedMultiplier;

        // STEP 9����ɫ�仯��ȡ��ע�ͼ�����Ч��
        //ballRenderer.color *= GetColorMultiplier(Mathf.Abs(_rigidbody.linearVelocity.x), Mathf.Abs(_rigidbody.linearVelocity.y));
    }

    // STEP 8����ɫ������������ԭҪ��ʵ�֣�
    private float GetColorMultiplier(float xSpeed, float ySpeed)
    {
        float averageSpeed = (Mathf.Abs(xSpeed) + Mathf.Abs(ySpeed)) / 2;
        if (averageSpeed > _speedThreshold)
        {
            return 1.5f;
        }
        else
        {
            return 1.0f;
        }
    }
}