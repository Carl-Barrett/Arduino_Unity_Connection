using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class HumidityController : MonoBehaviour
{
    public GameObject prefabToSpawn; // the prefab to spawn when humidity is above 50
    public int poolSize = 10; // the number of prefabs to preallocate in the object pool
    public float spawnRate = 1f; // the initial rate at which prefabs will fall
    public float maxSpawnRate = 10f; // the maximum rate at which prefabs will fall
    public string serialPortName = "COM3"; // the name of the serial port that the Arduino is connected to
    public int baudRate = 9600; // the baud rate of the serial port

    private SerialPort serialPort;
    private ObjectPool pool;
    private float lastSpawnTime;
    private float humidity;

    void Start()
    {
        // initialize the serial port
        serialPort = new SerialPort(serialPortName, baudRate);
        serialPort.Open();

        // create an object pool for the prefabs
        pool = new ObjectPool(prefabToSpawn, poolSize);

        // set the last spawn time to the current time
        lastSpawnTime = Time.time;

        // start the coroutine that spawns prefabs
        StartCoroutine(SpawnPrefabsCoroutine());
    }

    void OnDestroy()
    {
        // close the serial port when the script is destroyed
        serialPort.Close();
    }

    void Update()
    {
        // read the humidity data from the serial port
        if (serialPort.BytesToRead > 0)
        {
            string data = serialPort.ReadLine();
            string[] values = data.Split(',');
            humidity = float.Parse(values[0].Substring(2));
        }
    }

    IEnumerator SpawnPrefabsCoroutine()
    {
        while (true)
        {
            // check if the humidity is above 50
            if (humidity > 50)
            {
                // calculate the spawn rate based on the humidity
                float newSpawnRate = Mathf.Clamp(spawnRate + humidity / 10f, spawnRate, maxSpawnRate);

                // check if it's time to spawn a new prefab
                if (Time.time - lastSpawnTime > 1f / newSpawnRate)
                {
                    // get a prefab from the object pool
                    GameObject prefab = pool.Get();

                    // set the prefab's position and rotation
                    Vector3 position = transform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                    prefab.transform.position = position;
                    prefab.transform.rotation = Quaternion.identity;

                    // activate the prefab
                    prefab.SetActive(true);

                    // update the last spawn time
                    lastSpawnTime = Time.time;

                    // update the spawn rate
                    spawnRate = newSpawnRate;
                }
            }

            yield return null;
        }
    }
}

public class ObjectPool
{
    private GameObject prefab;
    private int poolSize;
    private GameObject[] pool;
    private int nextAvailableIndex;

    public ObjectPool(GameObject prefab, int poolSize)
    {
        this.prefab = prefab;
        this.poolSize = poolSize;
        pool = new GameObject[poolSize];
        nextAvailableIndex = 0;

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = GameObject.Instantiate(prefab);
            pool[i].SetActive(false);
        }
    }

    public GameObject Get()
    {
        GameObject obj = pool[nextAvailableIndex];
        nextAvailableIndex = (nextAvailableIndex + 1) % poolSize;
        return obj;
    }
}










