using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim ;
    protected const float freezeTime = 0.5f;
    public float imok = freezeTime+1;
    public float hitPower = 50f;
    public float moveSpeed = 0f;
    protected Rigidbody2D rb;
    protected Transform player;
    Player playerScript;
    public Vector2 enemyToPlayer;
    float maxPowerUp = 1000f;
    protected BoxCollider2D boxCol = null;
    protected int triggerFlag = 0;
    public HealthBar healthBar;
    int maxHealth=100;
    public int curHealth=100;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        playerScript = player.GetComponent<Player>();
        boxCol = transform.GetComponent<BoxCollider2D>(); 
        healthBar.setMaxHealth(maxHealth);
    }
    void Update() {
        anim.SetBool("run",imok>freezeTime);
        if(curHealth<=0){
            StartCoroutine("Fade",gameObject);
        }
    }
    public void setReStart(){
        curHealth=maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(imok>freezeTime)
            move();
        else if(imok<=freezeTime){
            imok+=Time.deltaTime;
            maChal();
        }
        if(triggerFlag>0 && triggerFlag<2){
            boxCol.isTrigger = false;
            hittedOk();
        }
            
        // Debug.Log(player.transform.position);
    }
    void TakeHit(int damage){
        curHealth-=damage;
        healthBar.setHealth(curHealth);
    }
    protected void hittedOk(){
        transform.GetComponent<SpriteRenderer>().color=Color.white;
    }

    protected void maChal(){
        rb.velocity = new Vector2(rb.velocity.x*0.9f,rb.velocity.y*0.9f);
    }

    protected void move(){
        triggerFlag +=1;
        moveSpeed+=0.01f;
        moveSpeed = Mathf.Clamp(moveSpeed,0,0.3f);
        enemyToPlayer = new Vector2(
            rb.transform.position.x-player.position.x,
            rb.transform.position.y-player.position.y
        );
        // Debug.Log(enemyToPlayer.normalized);
        rb.MovePosition(rb.position-enemyToPlayer.normalized * moveSpeed *Time.deltaTime);
    }
    protected void moveBack(){
        triggerFlag +=1;
        moveSpeed+=0.01f;
        moveSpeed = Mathf.Clamp(moveSpeed,0,0.3f);
        enemyToPlayer = new Vector2(
            rb.transform.position.x-player.position.x,
            rb.transform.position.y-player.position.y
        );
        // Debug.Log(enemyToPlayer.normalized);
        rb.MovePosition(rb.position+enemyToPlayer.normalized * moveSpeed *Time.deltaTime);
    }

    IEnumerator Fade(GameObject Obj){
        // if(gameObject.name=="fall")
            // Obj.GetComponent<SpriteRenderer>().sortingLayerName="DefaultBackGround";
        // else
        //     Obj.GetComponent<SpriteRenderer>().sortingLayerName="DefaultBackGround";
        // Debug.Log(-1);
        yield return new WaitForSeconds(0.5f);
        // for(int i=0;i<10000;i++){
        //     int j = 1;
        // }
        // Debug.Log(1);
        Obj.SetActive(false);
        yield break;
    }
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("SWORD")&& imok>0.1f){
    //         rb.AddForce(enemyToPlayer.normalized  * Mathf.Clamp(playerScript.powerUp,0,maxPowerUp)*0.1f);
    //         imok = 0;
    //     }
    // }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("SWORD") && imok>freezeTime && !other.transform.parent.CompareTag("ENEMY")){
            rb.AddForce(enemyToPlayer.normalized  * Mathf.Clamp(playerScript.powerUp,0,maxPowerUp)*0.1f);
            imok = 0;
            moveSpeed = 0f;
            boxCol.isTrigger = true;
            triggerFlag=0;
            transform.GetComponent<SpriteRenderer>().color=Color.green;
            TakeHit(20);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("PLAYER")&& imok>freezeTime){
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(-enemyToPlayer.normalized *400f);
        }
    }
}
