using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    // Singleton Class
    public static CubeSpawner Instance;

    Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; //4096
    private int maxPower = 12;
    private Vector3 defaultSpwanPosition;

    private void Awake()
    {
        Instance = this;
        defaultSpwanPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        IntializeCubesQueue();
    }
    private void IntializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)

            AddCubeToQueue();
    }
    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpwanPosition, Quaternion.identity, transform)
        .GetComponent <Cube>();

        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);
    }
    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.Log("No more cubes in the queue");
            }
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;

    }

    public Cube SpawnRandom(){
        return Spawn(GenerateRandomNumber(), defaultSpwanPosition);
    }
    public void DestroyCube(Cube cube){
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);


    }
    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}
