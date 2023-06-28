using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoDropManager : MonoBehaviour
{ 
    public RotateState rotateState = RotateState.CLosed;

    [SerializeField]
    private MaintenanceManager maintenanceManager;

    public Transform rotator; 

    public void OpenRotator()
    {
        StopAllCoroutines();
        StartCoroutine(RotaterCoroutine(Quaternion.Euler( new Vector3(0,0,-90)),1));
        
        rotateState = RotateState.Openned; 

        maintenanceManager.step04.SetActive(true);

    }

    public void CloseRotator()
    {
        StopAllCoroutines();    
        StartCoroutine(RotaterCoroutine(Quaternion.Euler(Vector3.zero), 1));
        
        rotateState = RotateState.CLosed; 

        maintenanceManager.step07.SetActive(true);

    }


    IEnumerator RotaterCoroutine(Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = rotator.transform.localRotation; 
        while (time < duration)
        {
            rotator.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, time / duration); 
            time += Time.deltaTime;
            yield return null;
        }

        rotator.transform.localRotation = targetRotation;
    }

}

[SerializeField]
public enum RotateState
{
    Openned,
    CLosed
}

