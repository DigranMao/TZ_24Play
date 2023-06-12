using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    [SerializeField] private Transform player;    
    [SerializeField] private float speed;
    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - player.position;
    }
    
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, offset + player.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z + + player.position.z);
    }
}
