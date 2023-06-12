using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject firstCube, cubePrefab;
    [SerializeField] private Transform playerTransform;   
    [SerializeField] private ParticleSystem cubeStackEffect;
    [SerializeField] private float cubeSpacing = 1f;

    private Animator camera;
    private Stickman stickman;
    private List<GameObject> cubes = new List<GameObject>();
    private bool collisionObstacle = false;

    private void Start()
    {
        stickman = GameObject.Find("Stickman").GetComponent<Stickman>();
        camera = GameObject.Find("Main Camera").GetComponent<Animator>();
        cubes.Add(firstCube);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubePickup"))
        {
            stickman.Jump();
            cubeStackEffect.Play();
            CreateCube(Vector3.zero);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Obstacle" && !collisionObstacle)
        {
            camera.SetTrigger("Shake");
            int obstacleChildCount = other.transform.childCount;

            if (obstacleChildCount <= cubes.Count)
            {
                List<GameObject> cubesToKeep = new List<GameObject>();

                for (int i = cubes.Count - 1; i >= cubes.Count - obstacleChildCount; i--)
                {
                    GameObject cubeToRemove = cubes[i];
                    cubeToRemove.transform.parent = null;
                    Destroy(cubeToRemove, 5f);
                }

                for (int i = 0; i < cubes.Count - obstacleChildCount; i++)
                    cubesToKeep.Add(cubes[i]);

                cubes = cubesToKeep;
            }
            else playerTransform.gameObject.GetComponent<Stickman>().DeathStickman();

            Invoke("RemoveObstacleCollision", 1);
            collisionObstacle = true;
        }
    }

    void RemoveObstacleCollision()
    {
        collisionObstacle = false;
    }
    
    private void CreateCube(Vector3 localPosition)
    {
        float characterHeight = playerTransform.localPosition.y + cubeSpacing + 0.5f;
        playerTransform.localPosition = new Vector3(playerTransform.localPosition.x, characterHeight, playerTransform.localPosition.z);

        for (int i = cubes.Count - 1; i >= 0; i--)
        {
            GameObject cube = cubes[i];
            float cubeHeight = cube.transform.localPosition.y + cubeSpacing;
            cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, cubeHeight, cube.transform.localPosition.z);
        }

        GameObject newCube = Instantiate(cubePrefab, transform);
        newCube.transform.localPosition = localPosition;
        cubes.Add(newCube);
        newCube.tag = "Untagged";
    }
}