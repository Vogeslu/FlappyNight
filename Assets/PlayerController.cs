using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D controllerBody;
    public Camera mainCamera;
    public float upwardsVelocity = 4.0f;
    public float speed = 2f;
    
    public GameObject busszaw;
    public float lastCreation = 4f;
    public float distance = 5f;

    public float[] busszawPositions = {
        3.85f,
        2.64f,
        1.44f,
        0.19f,
        -1.05f,
        -2.33f,
        -3.61f
    };

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++) {
            lastCreation += distance;
            generateRandomBuzzsawWall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Game.alive) {
            Vector3 velocity = controllerBody.velocity;

            if(Input.GetKey("space")) {
                velocity.y = upwardsVelocity;
            }

            controllerBody.velocity = velocity;

            float y = controllerBody.velocity.y;
            float rotation = 0;

            if(y > 4) y = 4;
            if(y < -4) y = -4;

            rotation = -(180 - (180 / 8 * (y+4)));

            if(rotation > 0) rotation = 0;
            if(rotation < -180) rotation = -180;

            transform.eulerAngles = new Vector3(0,0,rotation);

            transform.position += Vector3.right * Time.deltaTime * speed;
        
            if((transform.position.x + (distance * 5)) > lastCreation + distance) {
                for(int i = 0; i < 5; i++) {
                    lastCreation += distance;
                    generateRandomBuzzsawWall();
                }
            }
        }
    }

    void generateRandomBuzzsawWall() {
        int skip = Random.Range(0,busszawPositions.Length - 2);

        for(int i = 0; i < busszawPositions.Length; i++) {
            if(skip != i && skip +1 != i)
                Instantiate(busszaw, new Vector3(lastCreation,busszawPositions[i],0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Game.alive = false;
        controllerBody.bodyType = RigidbodyType2D.Static;
    }
}
