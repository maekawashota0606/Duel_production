using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneQueue = string.Empty;

    void Update()
    {
        if(InputProvider.Instance.GetFire1P(InputProvider.FireType.Cross) || InputProvider.Instance.GetFire2P(InputProvider.FireType.Cross))
        {
            SceneManager.LoadScene(nextSceneQueue);
        }
    }
}
