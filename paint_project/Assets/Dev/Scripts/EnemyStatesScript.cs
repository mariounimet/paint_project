using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesScript : MonoBehaviour
{
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void pause()
    {
        paused = true;
    }

    public void resume()
    {
        paused = false;
    }
}
