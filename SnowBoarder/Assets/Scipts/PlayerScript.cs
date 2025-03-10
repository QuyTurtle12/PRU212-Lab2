using UnityEngine;

using System.Collections.Generic;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu player chạm vào object có tag "Boost"
        if (other.CompareTag("Boost"))
        {
         
            Destroy(other.gameObject); // Hủy boost object
           
        }
    }
}