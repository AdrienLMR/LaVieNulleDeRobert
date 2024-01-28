using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.VersionControl.Asset;

public class AnimFinger : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.DOShakeRotation(5f, new Vector3(0, 0, 5),1, 2f);
        
    }
}


// DOShakeRotation(float duration, float/Vector3 strength, int vibrato, float randomness, bool fadeOut, ShakeRandomnessMode randomnessMode)