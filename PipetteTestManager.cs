using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipetteTestManager : MonoBehaviour
{
    public MaintenanceManager maintenanceManager;

    public MoveToGoalAgent goalAgent;
    public GameObject pipettePrefab;
     
    public Transform bestPosition;

    public GameObject resultPanel;
    public GameObject showResultTxt;
    public GameObject hideResultTxt; 

    public TMPro.TextMeshProUGUI rotationResult;
    public TMPro.TextMeshProUGUI positionResult;
    public TMPro.TextMeshProUGUI accurcyResult;


    public GameObject cachePipette;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMaintainance()
    { 
        goalAgent.gameObject.SetActive(true);

        if(cachePipette != null)
            Destroy(cachePipette);

        goalAgent.enabled = true; 
    }

    internal void OnSuccessfull()
    {
        goalAgent.gameObject.SetActive(false);
        cachePipette = Instantiate(pipettePrefab, transform);

        cachePipette.transform.localPosition = goalAgent.transform.localPosition;
        cachePipette.transform.localRotation = goalAgent.transform.localRotation;
         

        maintenanceManager.step05.SetActive(true);

        GenerateResults(cachePipette.transform);

    }

    public void OnClickShowResultBtn()
    {
        if (resultPanel.activeInHierarchy)
        {
            resultPanel.SetActive(false);

            showResultTxt.SetActive(true);
            hideResultTxt.SetActive(false);
        }
        else
        {
            resultPanel.SetActive(true);

            showResultTxt.SetActive(false);
            hideResultTxt.SetActive(true);
        }
    }

    private void GenerateResults(Transform pipette)
    {

        float pipetteRotation = 90 - Mathf.Abs(pipette.localRotation.eulerAngles.z);
        float pipettePosition = Mathf.Abs(bestPosition.position.z - pipette.position.z);

        rotationResult.text = pipetteRotation + " ";
        positionResult.text = pipettePosition + " ";
        accurcyResult.text = "## %";

    }
}
