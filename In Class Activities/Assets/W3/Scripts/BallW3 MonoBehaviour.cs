using TMPro;
using UnityEngine;

public class BallW3 : MonoBehaviour
{
    public SpriteRenderer ballRenderer; // 供CatW3访问颜色，需保持public
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speedMultiplier = 1.0f;
    [SerializeField] private float _speedThreshold = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // STEP 1：碰撞后速度乘以乘数
        _rigidbody.linearVelocity *= _speedMultiplier;

        // STEP 9：颜色变化（取消注释即可生效）
        //ballRenderer.color *= GetColorMultiplier(Mathf.Abs(_rigidbody.linearVelocity.x), Mathf.Abs(_rigidbody.linearVelocity.y));
    }

    // STEP 8：颜色乘数方法（按原要求实现）
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