using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject Gospel;
    [SerializeField] GameObject finalGospel;
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject JESUSUI;
    [SerializeField] CinemachineVirtualCamera MainVCAM;

    public void LoadGospel()
    {
        Gospel.SetActive(true);
        startUI.SetActive(false);
        JESUSUI.SetActive(false);
    }

    public void LoadGospelFinal()
    {
        finalGospel.SetActive(true);
        JESUSUI.SetActive(false);
    }

    public void ReturnFromGospelFinal()
    {
        finalGospel.SetActive(false);
        JESUSUI.SetActive(true);
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        MainVCAM.Priority = 100;
    }

    public void ExitGospel()
    {
        Gospel.SetActive(false);
        startUI.SetActive(true);
    }

    private void onUIButtonClick()
    {

    }
}
