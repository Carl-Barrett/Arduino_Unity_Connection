using UnityEngine;
using System.IO.Ports;

public class ScaleObject : MonoBehaviour
{
    public float minDistance = 10f;
    public float maxDistance = 100f;
    public float minScale = 1f;
    public float maxScale = 3f;
    public float smoothingFactor = 0.1f;

    private float currentScale = 1f;
    private SerialPort sp;

    private void Start()
    {
        sp = new SerialPort("COM3", 9600);
        sp.Open();
        sp.ReadTimeout = 1;
    }

    private void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                string serialInput = sp.ReadLine().Trim();
                if (serialInput.StartsWith("distance="))
                {
                    float distance = float.Parse(serialInput.Substring(9, serialInput.Length - 10));
                    float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
                    float targetScale = Mathf.Lerp(minScale, maxScale, normalizedDistance);
                    currentScale = Mathf.Lerp(currentScale, targetScale, smoothingFactor);
                    transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                }
            }
            catch (System.Exception)
            {
                // Ignore exceptions
            }
        }
    }
}




