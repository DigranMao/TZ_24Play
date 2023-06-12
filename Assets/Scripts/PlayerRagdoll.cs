using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    private Rigidbody[] rb_Ragdoll;
    private Joint[] joints;

    void Start()
    {
        rb_Ragdoll = GetComponentsInChildren<Rigidbody>();
        joints = GetComponentsInChildren<Joint>();
        
        foreach(Rigidbody rb in rb_Ragdoll)
            rb.isKinematic = true;
        
        foreach(Joint joint in joints)
            joint.gameObject.SetActive(false);

    }

    public void PlayRagdoll()
    {
        foreach(Rigidbody rb in rb_Ragdoll)
                rb.isKinematic = false;

        foreach(Joint joint in joints)
                joint.gameObject.SetActive(true);
    }
}
