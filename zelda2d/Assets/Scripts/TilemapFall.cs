using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapFall : MonoBehaviour
{
    // float cnt=0;
    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log(cnt);
        if(other.gameObject.CompareTag("PLAYER") || other.gameObject.CompareTag("ENEMY")){
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale=8.8f;
            StartCoroutine("Fade",other.gameObject);
        }
    }
    IEnumerator Fade(GameObject Obj){
        if(gameObject.name=="fall")
            Obj.GetComponent<SpriteRenderer>().sortingLayerName="DefaultBackGround";
        // else
        //     Obj.GetComponent<SpriteRenderer>().sortingLayerName="DefaultBackGround";
        // Debug.Log(-1);
        yield return new WaitForSeconds(2f);
        // Debug.Log(1);
        Obj.SetActive(false);
        yield break;
    }
}
