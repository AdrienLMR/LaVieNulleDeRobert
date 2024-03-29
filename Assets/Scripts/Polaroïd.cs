using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Polaroïd : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Animator animator = default;

    [Header("Value")]
    [SerializeField] private float waitTime = 2f;

    private static Polaroïd Instance;

    private void Start()
    {
        Instance = this;
        StartCoroutine(_Start());
    }

    private IEnumerator _Start()
    {
        animator.Play("Card1");
        yield return null;
    }

    public static void AnimationEnded()
    {
        Instance.StartCoroutine(Instance.Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);

        LevelManager.Instance.NextLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}