using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public void OnClickPlay()
    {
        LevelManager.Instance.NextLevel();
    }
}
