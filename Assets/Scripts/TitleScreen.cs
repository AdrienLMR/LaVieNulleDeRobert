using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private string nextLevel = string.Empty;

    public void OnClickPlay()
    {
        //LevelManager.Instance.LoadNextLevel(nextLevel);
    }
}
