using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SwitchGravity : MonoBehaviour
{
    // The name of the serial port to use
    public string portName = "COM3";
    // The baud rate for the serial communication
    public int baudRate = 4800;
    // The gravitational pull when the switch is inactive
    public float gravityDown = -9.81f;
    // The gravitational pull when the switch is active
    public float gravityUp = 9.81f;

    // The serial port object
    private SerialPort serialPort;

    // Cached vector objects for setting the gravitational pull
    private Vector3 gravityUpVector;
    private Vector3 gravityDownVector;

    void Start()
    {
        // Initialize the serial port object
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();

        // Create the cached vector objects for setting the gravitational pull
        gravityUpVector = new Vector3(0, gravityUp, 0);
        gravityDownVector = new Vector3(0, gravityDown, 0);

        // Start reading the serial port on a separate thread
        StartCoroutine(ReadSerialPort());
    }

    private IEnumerator ReadSerialPort()
    {
        while (true)
        {
            string switchState = serialPort.ReadLine();
            int switchValue;
            if (int.TryParse(switchState, out switchValue))
            {
                if (switchValue == 1)
                {
                    Physics.gravity = gravityUpVector;
                }
                else
                {
                    Physics.gravity = gravityDownVector;
                }
            }
            yield return null;
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port when the application is quit
        serialPort.Close();
    }
}


