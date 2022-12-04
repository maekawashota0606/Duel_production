using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Test : MonoBehaviour
{
    [SerializeField]
    private Vector3 normal = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Koma"))
        {
            collision.gameObject.GetComponent<Koma>().ReflectWall(normal);
        }
    }
}
