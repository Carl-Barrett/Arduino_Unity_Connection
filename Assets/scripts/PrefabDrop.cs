using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PrefabDrop : MonoBehaviour
{
    public GameObject prefabToDrop; // Prefab to instantiate and drop
    public Transform dropFromObjectF; // Object to drop prefab from when 'F' is received
    public Transform dropFromObjectG; // Object to drop prefab from when 'G' is received
    public Transform dropFromObjectA; // Object to drop prefab from when 'A' is received
    public Transform dropFromObjectC; // Object to drop prefab from when 'C' is received
    public Transform dropFromObjectE; // Object to drop prefab from when 'E' is received
    private SerialPort serialPort; // Serial port object

    // Use this for initialization
    void Start()
    {
        serialPort = new SerialPort("COM3", 9600); // Change "COM3" to the appropriate serial port name for your setup
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            if (serialPort.BytesToRead > 0)
            {
                byte receivedByte = (byte)serialPort.ReadByte();
                switch ((char)receivedByte)
                {
                    case 'F':
                        InstantiateAndDropFrom(dropFromObjectF);
                        break;
                    case 'G':
                        InstantiateAndDropFrom(dropFromObjectG);
                        break;
                    case 'A':
                        InstantiateAndDropFrom(dropFromObjectA);
                        break;
                    case 'C':
                        InstantiateAndDropFrom(dropFromObjectC);
                        break;
                    case 'E':
                        InstantiateAndDropFrom(dropFromObjectE);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    private void InstantiateAndDropFrom(Transform dropFromObject)
    {
        GameObject newObject = Instantiate(prefabToDrop, dropFromObject.position, Quaternion.identity);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.down * Random.Range(-1f, 1f), ForceMode.Impulse);
        }
    }
}


