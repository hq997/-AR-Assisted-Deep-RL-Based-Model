using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour
{
    public RotateState rotateState = RotateState.CLosed;


    public Transform rotator;
    [SerializeField]
    private GameObject rotatorIsOn;
    public GameObject openBtn;
    public GameObject closeBtn;

    //[SerializeField]
    //private Animator instructionsPanel;

    [Space]
    [SerializeField] private List<GameObject> popUps = new List<GameObject>();

    public void OpenRotator()
    {
        StopAllCoroutines();
        StartCoroutine(RotaterCoroutine(Quaternion.Euler(new Vector3(0, 0, -90)), 1));

        rotateState = RotateState.Openned;
        rotatorIsOn.SetActive(true);

        openBtn.SetActive(false);
        closeBtn.SetActive(true);
    }

    public void CloseRotator()
    {
        StopAllCoroutines();
        StartCoroutine(RotaterCoroutine(Quaternion.Euler(Vector3.zero), 1));

        rotateState = RotateState.CLosed;
        rotatorIsOn.SetActive(false);

        openBtn.SetActive(true);
        closeBtn.SetActive(false);
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

    //public void OpenInstructionPanel()
    //{
    //    instructionsPanel.Play("Open");
    //}


    //public void CloseInstructionPanel()
    //{
    //    instructionsPanel.Play("Close");
    //}


    public void OpenPopUp(GameObject popUp)
    {
        for (int i = 0; i < popUps.Count; i++)
        {
            popUps[i].SetActive(false);
        }

        popUp.SetActive(true);
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
