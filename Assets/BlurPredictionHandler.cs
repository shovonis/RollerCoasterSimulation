using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkAPI;
public class BlurPredictionHandler : MonoBehaviour
{
    [Header("Values")]
    [Range(0f,10f)]

    public float prediction = 0f;

    public float minSigma = .3f;
    public float maxSigma = 4f;
    public DynamicGaussianBlur dynamicGaussianBlur;
    public NetWorkManager networkManager;
    void Start(){
        //StartCoroutine(PredictionUpdate());
    }
    void Update(){
        dynamicGaussianBlur.sigma = Mathf.Lerp(minSigma,maxSigma,prediction/10f);
    }

    float changeRate = .15f;
    IEnumerator PredictionUpdate(){
        while(true){
            prediction = Mathf.Lerp(prediction,networkManager.PredictedCyberSickness,changeRate);
            yield return null;
        }
    }
}
