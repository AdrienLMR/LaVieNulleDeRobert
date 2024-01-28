using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Loading : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Animator animator = default;

    private static Loading Instance;
    //[Header("Values")]

    #region Unity Methods
    private void Start()
    {
        Instance = this;

        animator.Play("Load");
    }
    #endregion

    public static void StartAnim()
    {
        Debug.Log("a");
        Instance.animator.Play("Start");
    }

    public static void Stop()
    {
        Instance.animator.Play("Unload");
    }

    public static void OnUnloaded()
    {
        LevelManager.Instance.UnloadLoading();
    }
}
