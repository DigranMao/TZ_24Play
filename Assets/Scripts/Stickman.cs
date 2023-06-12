using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{  
    [SerializeField] private GameObject losePanel, cubeText;
    [SerializeField] private Transform positionText;

    private Animator anim;
    private PlayerRagdoll ragdoll;

    public bool loseGame = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        ragdoll = GetComponentInChildren<PlayerRagdoll>();
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
        GameObject text = Instantiate(cubeText, positionText.position, cubeText.transform.rotation);
        Destroy(text, 3f);
    }

    void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.tag == "CubeObstacle")
            DeathStickman();
    }

    public void DeathStickman()
    {
        loseGame = true;
        anim.enabled = false;
        ragdoll.PlayRagdoll();
        losePanel.SetActive(true);
    }
}
