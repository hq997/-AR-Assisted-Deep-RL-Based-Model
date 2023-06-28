using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDissolveEffect : MonoBehaviour
{
    public float minVal;
    public float maxVal;
    public float lerpDuration = 3; 


    float timeElapsed;
    float valueToLerp;
    

    // Start is called before the first frame update
    List<Material> materials = new List<Material>();
    bool PingPong = false;
    void Start()
    {
        var renders = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            materials.AddRange(renders[i].materials);
        }

    }

    private void OnEnable()
    {
        timeElapsed = 0;
    }

    void Update()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(minVal, maxVal, timeElapsed / lerpDuration);
            SetValue(valueToLerp);
            timeElapsed += Time.deltaTime;
        }
    }

    public void SetValue(float value)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetVector("_DissolveOffest", new Vector4(0, 0, value, 0));
        }
    }
}


