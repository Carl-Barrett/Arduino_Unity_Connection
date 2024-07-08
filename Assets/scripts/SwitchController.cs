using UnityEngine;
using System.IO.Ports;

public class SwitchController : MonoBehaviour
{
    public GameObject prefabToDropOn;
    public GameObject prefabToDropOff;
    public Transform dropPoint;
    SerialPort sp = new SerialPort("COM3", 9600);

    private void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    private void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                if (sp.ReadByte() == '0')
                {
                    Debug.Log("switch is on");
                    Instantiate(prefabToDropOn, dropPoint.position, dropPoint.rotation);
                }
                else
                {
                    Debug.Log("switch is off");
                    Instantiate(prefabToDropOff, dropPoint.position, dropPoint.rotation);
                }
            }
            catch (System.Exception)
            {

            }
        }

    }
}



