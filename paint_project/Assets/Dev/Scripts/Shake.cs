using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator canAnim;

    public void CamShake()
    {
        canAnim.SetTrigger("Shake1");
    }
}
