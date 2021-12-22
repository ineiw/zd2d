using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{
    public GameObject enemy2;
    public int limit = 20;
    int firstLimit = 20;
    List<GameObject> enemy = new List<GameObject>();
    public GameObject player = null;
    public GameObject sword = null;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(sword,new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0),Quaternion.Euler(new Vector3(0,0,Random.Range(-1f,1f))));
        for(int i=0;i<firstLimit;i++)
            enemy.Add(Instantiate(enemy2));
        // Debug.Log(enemy2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            reStart();
        }
    }
    void reStart(){
        int cnt=0;
        foreach (var item in enemy)
        {
            cnt++;
            item.SetActive(true);
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.transform.position = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            item.GetComponent<SpriteRenderer>().sortingLayerName="Player";
            item.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
            item.gameObject.GetComponent<Enemy>().moveSpeed = 0f;
            if(cnt>limit)
                item.SetActive(false);
        }
        player.SetActive(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = Vector2.zero;
        player.GetComponent<SpriteRenderer>().sortingLayerName="Player";
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
    }
}
