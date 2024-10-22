using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    Gyroscope gyro;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = gyro.attitude;
        if(Input.GetMouseButtonDown(0))
        {
            text.text = Random.Range(0, 100).ToString();
        }
    }
}
