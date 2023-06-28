using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaintenanceManager : MonoBehaviour
{
    public MachineState machineState = MachineState.UnPlugged;

    [SerializeField]
    private NanoDropManager nanoDropManager;
    [SerializeField]
    private PipetteTestManager pipetteTestManager;
     
      

    [Space]
    public GameObject step02;
    public GameObject step03;
    public GameObject step04;
    public GameObject step05;
    public GameObject step06;
    public GameObject step07;

    private void Start()
    { 
    }
        

    public void Step02()
    {
        step02.SetActive(false);
        step03.SetActive(true);
    }


    public void Step03()
    {
        step03.SetActive(false);
        nanoDropManager.OpenRotator();
    }

    public void Step04()
    {
        step04.SetActive(false);
        pipetteTestManager.StartMaintainance();
    }

    public void Step05()
    {
        step05.SetActive(false);
        step06.SetActive(true);
    }

    public void Step06()
    {
        step06.SetActive(false);
        nanoDropManager.CloseRotator();
        pipetteTestManager.cachePipette.SetActive(false);
    }

    public void Step07()
    {
        Reload();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}


[SerializeField]
public enum MachineState
{
    Plugged,
    UnPlugged
}

