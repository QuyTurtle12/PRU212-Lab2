using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    private bool isHurt = false;
    private float lastDamageTime = 0f; // Lưu thời gian lần trừ máu gần nhất
    private float hurtCooldown = 3f;  // Thời gian miễn thương (3 giây)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (Time.time - lastDamageTime < hurtCooldown)
            {
                return; // Nếu chưa qua 3 giây, không trừ máu
            }

            lastDamageTime = Time.time; // Cập nhật thời gian trừ máu cuối cùng
            HeartManager.health--;

            if (HeartManager.health <= 0)
            {
                PlayerManaer.isGameOver = true;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        isHurt = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        yield return new WaitForSeconds(hurtCooldown);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        isHurt = false;
    }
}
