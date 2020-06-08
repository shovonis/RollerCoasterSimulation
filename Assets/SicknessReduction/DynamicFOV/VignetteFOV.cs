using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkAPI;
[ExecuteInEditMode]
public class VignetteFOV : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Values")]
    [Range(0f,10f)]
    public float prediction = 1f;
    public float minScale = .03f;
    public float maxScale = .06f;

    [Header("Helpers")]
    public GameObject sprite;
    public NetWorkManager networkManager;
    // Update is called once per frame
    public void Start(){
        StartCoroutine(PredictionUpdate());
    }
    void Update()
    {
        float scale = Mathf.Lerp(minScale,maxScale,1f - (prediction/10f) );
        sprite.transform.localScale = new Vector3(scale,scale);
    }

    public float PredictionBuffer(float p){
        return p;
    }
    float changeRate = .15f;
    IEnumerator PredictionUpdate(){
        while(true){
            prediction = Mathf.Lerp(prediction,networkManager.PredictedCyberSickness,changeRate);
            yield return null;
        }
    }
}
