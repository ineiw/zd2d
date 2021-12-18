using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim ;
    public GameObject sword;
    Vector2 movePos;
    Vector2 mousePos;
    Vector2 swordToMouse;
    float moveSpeed = 1f;
    public Enemy enemy;
    Rigidbody2D swordRb;
    public float powerUp = 0f;
    float a;
    float b;
    // Start is called before the first frame update
    void Start()
    {   
        rb = gameObject.GetComponent<Rigidbody2D>();
        swordRb =  sword.gameObject.GetComponent<Rigidbody2D>();
        // enemy = GameObject.FindObjectOfType<Enemy>();
    }

    void Update() {
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.y = Input.GetAxisRaw("Vertical"); 

        anim.SetBool("run",(movePos.x!=0 || movePos.y!=0));

        mousePos = Input.mousePosition;
        mousePos = new Vector2(
            mousePos.x-Screen.width/2,
            mousePos.y-Screen.height/2
        );
        // Debug.Log(mousePos);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movePos.normalized * moveSpeed *Time.deltaTime);
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
        powerUp = Mathf.Abs((b-a)/Time.deltaTime);
        b = swordRb.rotation;
    }

    
    
}
