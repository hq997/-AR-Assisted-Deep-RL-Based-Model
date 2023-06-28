using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDesignManager : MonoBehaviour
{
    public GameObject UI_Info;
    private Vector3 UI_StartPos;

    private void Awake()
    {
        UI_StartPos = UI_Info.transform.localPosition;
    }

    public void ActivateUI(bool flag)
    { 
        if (flag)
        {
            StartCoroutine(ActivateUICoroutine(new Vector3(UI_StartPos.x, UI_StartPos.y, 
                UI_StartPos.z + 0.05f), Vector3.one, 0.25f));
        }
        else
        {
            StartCoroutine(ActivateUICoroutine(UI_StartPos, Vector3.zero, 0.25f));
        }
    }

    IEnumerator ActivateUICoroutine(Vector3 targetPosition, Vector3 targetScale, float duration)
    {
        float time = 0;
        Vector3 startPosition = UI_Info.transform.localPosition;
        Vector3 startScale = UI_Info.transform.localScale;
        while (time < duration)
        {
            UI_Info.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            UI_Info.transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration); 
            time += Time.deltaTime;
            yield return null;
        }
        UI_Info.transform.localPosition = targetPosition;
        UI_Info.transform.localScale = targetScale;
    }     

    public void OnClickCloseUI()
    {
        ActivateUI(false); 
    }
}
