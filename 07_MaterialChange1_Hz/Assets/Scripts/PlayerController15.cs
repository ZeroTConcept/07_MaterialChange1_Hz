using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController15 : MonoBehaviour
{
    int isOnGround = 0;

    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float zLimit = 4.0f;
    float xLimit = 4.0f;
    float speed = 10.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //Move Player (GameObject) according to user interactions
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playerMtrs[3].color;
        }
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }
        PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = 0;

            playerRdr.material.color = playerMtrs[0].color;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround < 1)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isOnGround++;

            playerRdr.material.color = playerMtrs[1].color;
        }
    }
}
