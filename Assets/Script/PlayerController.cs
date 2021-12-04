using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionAsset input;
    [SerializeField] private Vector2 mapSize;
    [SerializeField] private float speed;
    [SerializeField] private GameManager manager;
    [SerializeField] Text scoreText;
    private Rigidbody2D rb;
    private NPCBehavior npc;
    private Vector3 touchPoint;
    private bool hasMove;
    private int score = 0;
    [SerializeField] private int life = 1;

    void Start()
    {
        hasMove = false;
        input.FindAction("Up").Enable();
        input.FindAction("Left").Enable();
        input.FindAction("Down").Enable();
        input.FindAction("Right").Enable();

        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!manager.canMove)
            return;
        Vector2 direction = new Vector2(0,0);


        if (input.FindAction("Up").triggered)
        {
            direction = Vector2.up;
        }

        if (input.FindAction("Down").triggered)
        {
            direction = -Vector2.up;
        }

        if (input.FindAction("Left").triggered)
        {
            direction = Vector2.left;
        }

        if (input.FindAction("Right").triggered)
        {
            direction = -Vector2.left;
        }

        if (direction.x != 0 || direction.y != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            if(hit.collider.gameObject.CompareTag("Ennemy") || hit.collider.gameObject.CompareTag("NPC"))
            {
                NPCBehavior npc = hit.collider.gameObject.GetComponent<NPCBehavior>();
                npc.Damage();
                this.transform.position = new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y);
                score++;
                scoreText.text = "Score: " + score + " ";
            }else
                this.transform.position = new Vector2(hit.point.x,hit.point.y);
            manager.playerMove();
        }

            


    }

    public void Damage()
    {
        life--;
        if(life == 0)
        {
            input.FindAction("Up").Disable();
            input.FindAction("Left").Disable();
            input.FindAction("Down").Disable();
            input.FindAction("Right").Disable();
            SceneManager.LoadScene("Game");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Ennemy"))
        {
            npc = collision.gameObject.GetComponent<NPCBehavior>();
            touchPoint = this.transform.position;
        }
    }

}
