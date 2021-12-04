using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool canMove { get; private set; }
    [SerializeField] private int minCop = 5;
    [SerializeField] private float chanceSpawnCop = 0.5f;
    [SerializeField] private GameObject policePrefab;
    [SerializeField] private Vector2 spawnFrom, spawnToo;
    [SerializeField] private Collider2D groundCollider;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length == 0 && !canMove)
            canMove = true;
    }

    public void playerMove()
    {
        GameObject[] polices = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach(GameObject police in polices)
            if(police.GetComponent<NPCBehavior>().life > 0)
                police.GetComponent<PoliceBehavior>().Shoot();
        canMove = false;
        if(polices.Length < 5 && Random.Range(0.0f,1f)<=chanceSpawnCop)
        {
            int x, y;

            x = (int)Random.Range(spawnFrom.x, spawnToo.x);
            y = (int)Random.Range(spawnFrom.y, spawnToo.y);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x,y), Vector2.up);
            if(hit.distance > 1)
                Instantiate(policePrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    
}
