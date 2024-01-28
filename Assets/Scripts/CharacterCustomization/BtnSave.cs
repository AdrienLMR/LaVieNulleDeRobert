using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSave : MonoBehaviour
{
    [SerializeField] private float timeToGoToNextScene = 10f;
    [SerializeField] private Transform poseToAddAnim = default;
    [SerializeField] private GameObject animToAdd = default;
    [SerializeField] private GameObject objectToDeactivate = default;
    [SerializeField] private Button btn;

    private GameObject anim = default;

    private float counterToGoToNextScene;
    private bool startCounter = false;

    private Action DoAction;

    private void Start()
    {
        DoAction = DoActionCheckNextScene;

        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        startCounter = true;
        anim = Instantiate(animToAdd, poseToAddAnim.position, Quaternion.identity);
        objectToDeactivate.SetActive(false);
    }

    private void Update()
    {
        DoAction();
    }

    private void DoActionVoid() { }

    private void DoActionCheckNextScene()
    {
        if (startCounter)
        {
            counterToGoToNextScene += Time.deltaTime;

            if (counterToGoToNextScene >= timeToGoToNextScene)
            {
                DoAction = DoActionVoid;
                Destroy(anim);
                LevelManager.Instance.NextLevel();
            }
        }
    }
}
