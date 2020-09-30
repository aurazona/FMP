using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //UNUSED
    public GameObject player;
    public bool camPerspective;

    // Start is called before the first frame update
    void Start()
    {
        camPerspective = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (camPerspective == false)
        {
         transform.position = player.transform.position + new Vector3(0, 0, -9);
        }
        if (camPerspective == true)
        {
            transform.position = player.transform.position + new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.M) && (camPerspective == false))
        {
            camPerspective = true;
        }
        if (Input.GetKeyDown(KeyCode.N) && (camPerspective == true))
        {
            camPerspective = false;
        }
    }
}
