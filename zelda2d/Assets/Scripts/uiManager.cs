using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{
    public GameObject [] enemy = new GameObject[6];
    public GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            reStart();
        }
    }
    void reStart(){
        foreach (var item in enemy)
        {
            item.SetActive(true);
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.transform.position = new Vector2(Random.RandomRange(-1f,1f),Random.RandomRange(-1f,1f));
            item.GetComponent<SpriteRenderer>().sortingLayerName="Player";
            item.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
        }
        player.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = Vector2.zero;
        player.GetComponent<SpriteRenderer>().sortingLayerName="Player";
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
    }
}
