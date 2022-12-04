using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Test : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // çUåÇÉqÉbÉg
        if (collision.gameObject.CompareTag("Koma"))
        {
            if (gameObject.name == "Attack1(Clone)" && collision.gameObject.name == "Koma2")
            {
                collision.gameObject.GetComponent<Koma>().DecelerationHit();
            }
            else if (gameObject.name == "Attack2(Clone)" && collision.gameObject.name == "Koma1")
            {
                collision.gameObject.GetComponent<Koma>().DecelerationHit();
            }
        }
        //çUåÇÇÃëäéE
        else if (collision.gameObject.CompareTag("Attack"))
        {
            if (gameObject.name == "Attack1(Clone)")
            {
                GameObject.Find("Koma1").GetComponent<Koma>().DecelerationOffset();
                GameObject.Find("Koma2").GetComponent<Koma>().DecelerationOffset();
            }
        }
    }
}
