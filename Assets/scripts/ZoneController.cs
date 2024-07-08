using UnityEngine;
using System.IO.Ports;

public class ZoneController : MonoBehaviour
{
    public string portName = "COM3"; // the name of the serial port to use (change this to match your Arduino)
    public int baudRate = 9600; // the baud rate to use for the serial communication
    public GameObject zone1; // the game object representing zone 1
    public GameObject zone2; // the game object representing zone 2

    private SerialPort serialPort;

    // Start is called before the first frame update
    void Start()
    {
        // initialize the serial port
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player is in zone 1
        if (zone1.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            // send an "R" command to the Arduino to turn on the red LED
            serialPort.Write("R");
        }

        // check if the player is in zone 2
        else if (zone2.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            // send a "G" command to the Arduino to turn on the green LED
            serialPort.Write("G");
        }

        // if the player is not in any zone, turn off the LED
        else
        {
            // send an "O" command to the Arduino to turn off the LED
            serialPort.Write("O");
        }
    }

    // called when the script is destroyed
    void OnDestroy()
    {
        // close the serial port when the script is destroyed
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}

