using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

    Slider slider;
	// Use this for initialization
	void Start () {
        slider = GameObject.Find("SliderTest").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickTest()
    {
        StartCoroutine(TimeCountTest());
    }

    IEnumerator TimeCountTest()
    {
        float timeCount = 10;
        slider.value = timeCount;
        while (true)
        {
            timeCount -= Time.deltaTime;
            slider.value = timeCount;
            yield return null;
        }
    }
}
