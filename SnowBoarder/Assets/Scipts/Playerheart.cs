using UnityEngine;
using System.Collections;

public class Playerheart : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.transform.tag == "Enemy")
        {
            HeartManager.health --;
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
    

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 8,false);
    }
}
