using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Spiner _spiner = null;

    public void MyInit(Spiner spiner)
    {
        _spiner = spiner;
    }

    public void MyUpdate(float delta)
    {
        // InputProvider�𒇉�A���͂��Ƃ�
    }
}
