using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Vector3 normal = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spiner"))
        {
            collision.gameObject.GetComponent<Spiner>().ReflectWall(normal);
        }
    }
}
