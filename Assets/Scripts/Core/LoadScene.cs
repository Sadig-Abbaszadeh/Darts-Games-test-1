using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    int index;

    private void Start()
    {
        SceneManager.LoadScene(index);
    }
}
