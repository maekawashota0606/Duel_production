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
        // InputProviderを仲介し、入力をとる
    }
}
