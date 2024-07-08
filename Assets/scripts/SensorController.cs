using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SensorController : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public float minDelay = 0.5f;
    public float maxDelay = 3f;
    public float maxRate = 10f;
    public float minHumidity = 50f;
    public float minTemp = 20f;
    public float maxTemp = 25f;
    public GameObject uiPanel;

    private SerialPort stream;
    private float delay = 1f;
    private float rate = 0f;
    private float lastSpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        stream = new SerialPort("COM3", 9600);
        stream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (stream.IsOpen)
        {
            string line = stream.ReadLine();
            string[] values = line.Split(',');

            if (values.Length == 2)
            {
                if (values[0].StartsWith("H:"))
                {
                    float humidity = 0f;
                    if (float.TryParse(values[0].Substring(2), out humidity))
                    {
                        if (humidity > minHumidity)
                        {
                            rate = Mathf.Clamp(humidity - minHumidity, 0f, maxRate);
                            delay = Mathf.Lerp(maxDelay, minDelay, rate / maxRate);
                            if (Time.time > lastSpawnTime + delay)
                            {
                                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                                lastSpawnTime = Time.time;
                            }
                        }
                        else
                        {
                            rate = 0f;
                        }
                    }
                }

                if (values[1].StartsWith("T:"))
                {
                    float temp = 0f;
                    if (float.TryParse(values[1].Substring(2), out temp))
                    {
                        if (temp > maxTemp)
                        {
                            uiPanel.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                        }
                        else if (temp < minTemp)
                        {
                            uiPanel.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
                        }
                        else
                        {
                            uiPanel.GetComponent<UnityEngine.UI.Image>().color = Color.clear;
                        }
                    }
                }
            }
        }
    }
}

