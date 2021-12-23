using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    Rigidbody2D rb;
    public Animator anim ;
    public GameObject sword;
    Vector2 movePos;
    Vector2 mousePos;
    Vector2 swordToMouse;
    float moveSpeed = 0.5f;
    public Enemy enemy;
    Rigidbody2D swordRb;
    public float powerUp = 0f;
    float dashPower = 100f;
    float a;
    float b;
    float dashTime = 0;
    int maxHealth=100;
    public int curHealth=100;
    public float freezeTime = 1.2f;
    float imok = 0f;
    bool ok = true;
    public SpriteRenderer playerRender;
    // Start is called before the first frame update
    void Start()
    {   
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerRender = transform.GetComponent<SpriteRenderer>();
        healthBar.setMaxHealth(maxHealth);
        // enemy = GameObject.FindObjectOfType<Enemy>();
    }

    void Update() {
        if(curHealth<=0)
            die();
        mousePos = Input.mousePosition;
        mousePos = new Vector2(
            mousePos.x-Screen.width/2,
            mousePos.y-Screen.height/2
        );
        if(ok)
            inputToMove();
        else{
            movePos.x = 0;
            movePos.y = 0; 

            anim.SetBool("run",(movePos.x!=0 || movePos.y!=0));
        }
            
        // Debug.Log(mousePos);
    }
    void die(){
        StartCoroutine("Fade",gameObject);
    }
    void inputToMove(){
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.y = Input.GetAxisRaw("Vertical"); 

        anim.SetBool("run",(movePos.x!=0 || movePos.y!=0));

        
        float _jump = Input.GetAxisRaw("Jump");
        if(_jump>0 && dashTime>0.5f){
            // for(int i=0;i<=1;i++)
            rb.AddForce(movePos * dashPower);
            // rb.velocity =(movePos * dashPower);
            dashTime=0f;
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(imok>freezeTime)
            ok=true;
        else{
            ok=false;
            imok+=Time.deltaTime;
            maChal(0.1f);
        }
        if(dashTime>0.5f)
            rb.MovePosition(rb.position + movePos.normalized * moveSpeed *Time.deltaTime);
        else{
            maChal();
            dashTime+=Time.deltaTime;
        }
        float swordToMouseDis = Vector2.Distance(transform.position,mousePos);
        sword.transform.position = new Vector2(
            transform.position.x + mousePos.x*(0.1f/swordToMouseDis),
            transform.position.y + mousePos.y*(0.1f/swordToMouseDis)
        );
        
        swordToMouse = new Vector2(
            sword.transform.position.x-mousePos.x,
            sword.transform.position.y-mousePos.y
        );
        // Debug.Log(swordToMouse);
        sword.transform.eulerAngles =  new Vector3(0,0,Mathf.Atan2(swordToMouse.y,swordToMouse.x) * Mathf.Rad2Deg+90f);
        a = swordRb.rotation;
        powerUp = Mathf.Abs((b-a)/Time.deltaTime)*10;
        Debug.Log(a);
        b = swordRb.rotation;
    }
    public void setReStart(){
        sword.tag="SWORD";
        curHealth=maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    void maChal(float h=0.9f){
        rb.velocity = new Vector2(rb.velocity.x*h,rb.velocity.y*h);
    }
    void TakeHit(int damage){
        curHealth-=damage;
        healthBar.setHealth(curHealth);
    }
    void hittedOk(){
        transform.GetComponent<SpriteRenderer>().color=Color.white;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("UNSWORD")){
            if(sword!=null)
                sword.tag="UNSWORD";
            sword = other.gameObject;
            swordRb =  sword.gameObject.GetComponent<Rigidbody2D>();
            sword.tag="SWORD";
            // sword.transform.position = Vector3.zero;
            // sword.transform.eulerAngles = Vector3.zero;
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("ENEMY"))
        {
            playerRender.color=Color.red;
            TakeHit(5);
            StartCoroutine("BeWhite");
            imok=0;
        }
    }
    IEnumerator BeWhite(){
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
        playerRender.color = Color.white;
        yield break;
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
        sword.tag="UNSWORD";
        Obj.SetActive(false);
        yield break;
    }
}
