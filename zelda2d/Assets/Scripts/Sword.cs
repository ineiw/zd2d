using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Collider2D col;
    private void Start() {
        col = transform.GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("SWORD")){
            Vector2 eToP = other.transform.parent.GetComponent<SwordEnemy>().enemyToPlayer;
            // Debug.Log(eToP);
            other.transform.parent.GetComponent<SwordEnemy>().imok=0;
            // other.transform.parent.GetComponent<Rigidbody2D>().AddForce(eToP.normalized*40f);
            other.transform.parent.GetComponent<SwordEnemy>().bangBing(transform.GetComponent<Rigidbody2D>().angularVelocity);
            // foreach (ContactPoint2D contact in other.contacts) {
            //     other.transform.parent.GetComponent<SwordEnemy>().bangBing(contact.relativeVelocity);
            // }

            
        }
    }
}
