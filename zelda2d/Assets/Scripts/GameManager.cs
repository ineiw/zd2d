using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy2;
    public GameObject swordEnemy2;
    public int limit = 20;
    public int swordLimit = 20;
    int firstLimit = 100;
    List<GameObject> enemy = new List<GameObject>();
    List<GameObject> swordEnemy = new List<GameObject>();
    public GameObject player = null;
    public GameObject sword = null;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(sword,new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0),Quaternion.Euler(new Vector3(0,0,Random.Range(-1f,1f))));
        limit = Mathf.Clamp(limit,0,firstLimit);
        for(int i=0;i<limit;i++)
            enemy.Add(Instantiate(enemy2));
        swordLimit = Mathf.Clamp(swordLimit,0,firstLimit);
        for(int i=0;i<swordLimit;i++){
            swordEnemy.Add(Instantiate(swordEnemy2,Vector3.right,Quaternion.identity));
            swordEnemy[i].GetComponent<SwordEnemy>().sword = Instantiate(sword);
            swordEnemy[i].GetComponent<SwordEnemy>().sword.tag="SWORD";
            swordEnemy[i].GetComponent<SwordEnemy>().sword.transform.parent = swordEnemy[i].transform;
        }
        player.GetComponent<Player>().sword.transform.parent = player.transform;
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
            item.GetComponent<Enemy>().setReStart();
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.transform.position = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            item.GetComponent<SpriteRenderer>().sortingLayerName="Player";
            item.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
            item.gameObject.GetComponent<Enemy>().moveSpeed = 0f;
            if(cnt>limit)
                item.SetActive(false);
        }
        cnt=0;
        foreach (var item in swordEnemy)
        {
            cnt++;
            item.SetActive(true);
            item.GetComponent<SwordEnemy>().setReStart();
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.transform.position = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            item.GetComponent<SpriteRenderer>().sortingLayerName="Player";
            item.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
            item.gameObject.GetComponent<SwordEnemy>().moveSpeed = 0f;
            if(cnt>swordLimit)
                item.SetActive(false);
        }
        player.SetActive(true);
        player.GetComponent<Player>().setReStart();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = Vector2.zero;
        player.GetComponent<SpriteRenderer>().sortingLayerName="Player";
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale=0f;
    }
}
