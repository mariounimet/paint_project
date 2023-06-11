using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }
}
