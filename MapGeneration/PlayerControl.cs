using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float playerSpeed = 1;

    private Rigidbody playerRigid;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxis("Vertical");
        float yMove = Input.GetAxis("Horizontal");

        Vector3 newPos = new Vector3(playerSpeed * xMove + gameObject.transform.position.x, gameObject.transform.position.y, playerSpeed * yMove + gameObject.transform.position.z);

        playerRigid.MovePosition(newPos);
    }
}
