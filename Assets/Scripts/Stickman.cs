using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{  
    [SerializeField] private GameObject cubeText;
    [SerializeField] private Transform positionText;

    private Animator anim;
    private PlayerRagdoll ragdoll;
    private GameObject losePanel;

    public bool loseGame = false;

    void Awake()
    {
        losePanel = GameObject.Find("LosePanel");
        anim = GetComponent<Animator>();
        ragdoll = GetComponentInChildren<PlayerRagdoll>();
    }

    void Start()
    {
        losePanel.SetActive(false);
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
