using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class APrefabDrop : MonoBehaviour
{
    public GameObject prefabToDrop; // Prefab to instantiate and drop
    public Transform dropFromObject; // Object to drop prefab from
    public float dropForce = 1.0f; // Force to apply to dropped prefab
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
            if (serialPort.ReadByte() == 'A') // Listen for 'A' character from serial connection
            {
                GameObject newObject = Instantiate(prefabToDrop, dropFromObject.position, Quaternion.identity);
                Rigidbody rb = newObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
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
}

