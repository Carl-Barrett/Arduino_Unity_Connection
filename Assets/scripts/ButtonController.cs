using UnityEngine;
using System.IO.Ports;

public class ButtonController : MonoBehaviour
{
    public AudioSource sound;
    SerialPort sp = new SerialPort("COM4", 9600);

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
                if (sp.ReadByte() == 1)
                {
                    Debug.Log("button is pressed");
                    sound.Play();
                }
            }
            catch (System.Exception)
            {

            }
        }
    }
}


