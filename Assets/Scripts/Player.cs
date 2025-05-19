using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider touchSlider;

    private Cube mainCube;

    private bool isPointerDown;
    private Vector3 cubePos;


    private void Start()
    {
        SpawnCube();

        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerUpEvent += OnPointerUp;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            mainCube.transform.position = Vector3.Lerp(
                mainCube.transform.position,
                cubePos,
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerUp()
    {
        isPointerDown = false;
        // Push the Cube
        mainCube.CubeRigidbody.AddForce(
            Vector3.forward * pushForce,
            ForceMode.Impulse);
        // spawn a new cube after 0.5 seconds
        Invoke("SpawnNewCube",0.5f);
    }

    private void SpawnNewCube(){
        mainCube.IsMainCube = false;
        SpawnCube();
    }
    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.IsMainCube = true;
        cubePos = mainCube.transform.position;
    }
    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
    }


}
