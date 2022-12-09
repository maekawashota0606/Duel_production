using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private GameObject _spiner;

    public void MyInit(GameObject spiner)
    {
        _spiner = spiner;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // UŒ‚ƒqƒbƒg
        if (collision.gameObject.CompareTag("Spiner"))
        {
            if (collision.gameObject != _spiner)
            {
                collision.gameObject.GetComponent<Spiner>().DecelerationHit();
            }
        }
        //UŒ‚‚Ì‘ŠE
        else if (collision.gameObject.CompareTag("Attack"))
        {
            _spiner.GetComponent<Spiner>().DecelerationOffset();
        }
    }
}
