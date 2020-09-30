using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static PlayerMovement;

#pragma warning disable CS0618 // Type or member is obsolete
public class PlayerFire : NetworkBehaviour
#pragma warning restore CS0618 // Type or member is obsolete
{
    public GameObject Projectile;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CmdFireCheck();
    }

#pragma warning disable CS0618 // Type or member is obsolete
    [Command]
#pragma warning restore CS0618 // Type or member is obsolete
    void CmdFireCheck()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject Spawned = Instantiate(Projectile, new Vector2(0, 22), transform.rotation);
            NetworkServer.SpawnWithClientAuthority(Spawned, player);
        }

    }
}
