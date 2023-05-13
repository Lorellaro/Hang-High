using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class GameEndHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] List<GameObject> allObjsToDisableAtEnd;
    [SerializeField] GameEnder gameEnder1;
    [SerializeField] GameEnder gameEnder2;
    [SerializeField] GameObject GospelButtonUI;
    [SerializeField] TypeWriterUI typeWriterUI;
    [SerializeField] Transform endObjCamFocus;
    [SerializeField] float cameraFinalOrthSize;
    [SerializeField] float cameraZoneOutSpeed;
    [SerializeField] float timeBtwText;
    [SerializeField] string text1;
    [SerializeField] string text2;
    [SerializeField] string text3;
    [SerializeField] string text4;


    float currentCamOrthSize;

    public bool hasGameEnded;
    public bool zoomCameraOut;

    private void Start()
    {
        currentCamOrthSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void OnEnable()
    {
        gameEnder1.endGame += EndGame;
        gameEnder2.endGame += EndGame;
    }

    private void OnDisable()
    {
        gameEnder1.endGame -= EndGame;
        gameEnder2.endGame -= EndGame;
    }

    public void EndGame()
    {
        if (hasGameEnded) { return; }
        hasGameEnded = true;
        
        for(int i = 0; i < allObjsToDisableAtEnd.Count; i++)
        {
            allObjsToDisableAtEnd[i].SetActive(false);
        }
        //zoom camera out 
        virtualCamera.m_Follow = endObjCamFocus;
        virtualCamera.m_LookAt = endObjCamFocus;
        zoomCameraOut = true;
        StartCoroutine(ShowEndText());
    }

    private IEnumerator ShowEndText()
    {
        yield return new WaitForSeconds(timeBtwText / 2f);
        StartCoroutine(typeWriterUI.TypeWriterAndSetString(text1));
        yield return new WaitForSeconds(timeBtwText);
        StartCoroutine(typeWriterUI.TypeWriterAndSetString(text2));
        yield return new WaitForSeconds(timeBtwText);
        StartCoroutine(typeWriterUI.TypeWriterAndSetString(text3));
        yield return new WaitForSeconds(timeBtwText);
        StartCoroutine(typeWriterUI.TypeWriterAndSetString(text4));
        yield return new WaitForSeconds(timeBtwText + 6.25f);
        GospelButtonUI.SetActive(true);
    }

    private void Update()
    {
        if (!zoomCameraOut || currentCamOrthSize > cameraFinalOrthSize) { return; }
        currentCamOrthSize += cameraZoneOutSpeed * Time.deltaTime;
        virtualCamera.m_Lens.OrthographicSize = currentCamOrthSize;
    }
}
