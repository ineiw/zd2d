using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float freezeTime = 1f;
    public float imok = freezeTime+1;
    public float hitPower = 50f;
    float moveSpeed = 0f;
    Rigidbody2D rb;
    public Transform player;
    Player playerScript;
    public Vector2 enemyToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<Player>();
    }
    void Update() {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(imok>freezeTime)
            move();
        if(imok<=freezeTime){
            imok+=Time.deltaTime;
            maChal();
        }
            
    }
    void maChal(){
        rb.velocity = new Vector2(rb.velocity.x*0.9f,rb.velocity.y*0.9f);
    }
    void move(){
        moveSpeed+=0.01f;
        moveSpeed = Mathf.Clamp(moveSpeed,0,0.3f);
        enemyToPlayer = new Vector2(
            rb.transform.position.x-player.position.x,
            rb.transform.position.y-player.position.y
        );
        // Debug.Log(enemyToPlayer.normalized);
        rb.MovePosition(rb.position-enemyToPlayer.normalized * moveSpeed *Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("SWORD")){
            rb.AddForce(enemyToPlayer.normalized  * playerScript.powerUp*0.1f);
            imok = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("SWORD")){
            rb.AddForce(enemyToPlayer.normalized  * playerScript.powerUp*0.1f);
            imok = 0;
        }
    }
}
