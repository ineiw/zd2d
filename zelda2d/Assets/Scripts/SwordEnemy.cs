using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Enemy
{
    public GameObject sword;
    float deadLineDis = 0.3f;
    Vector2 swordMoveDir;
    void Update() {
        
    }
    void FixedUpdate()
    {
        if(imok>freezeTime && deadLineDis <Vector3.Distance(rb.transform.position,player.transform.position))
            move();
        else if(imok<=freezeTime){
            imok+=Time.deltaTime;
            maChal();
        }
        else if(imok>freezeTime && deadLineDis>Vector3.Distance(rb.transform.position,player.transform.position))
            moveBack();
        // else
        //     rb.velocity = Vector2.zero;
        if(triggerFlag>0 && triggerFlag<2){
            boxCol.isTrigger = false;
            hittedOk();
        }

        sword.transform.position = new Vector2(
            transform.position.x - enemyToPlayer.normalized.x*0.15f,
            transform.position.y - enemyToPlayer.normalized.y*0.15f
        );

        sword.transform.eulerAngles =  new Vector3(0,0,Mathf.Atan2(enemyToPlayer.y,enemyToPlayer.x) * Mathf.Rad2Deg+90f);
        // Debug.Log(Vector3.Distance(rb.transform.position,player.transform.position));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("UNSWORD")){
            if(sword!=null)
                sword.tag="UNSWORD";
            sword = other.gameObject;
            sword.tag="SWORD";
            // sword.transform.position = Vector3.zero;
            // sword.transform.eulerAngles = Vector3.zero;
        }
    }
}
