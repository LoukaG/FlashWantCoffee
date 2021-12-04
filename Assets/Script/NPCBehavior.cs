using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public int life { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        life = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        life--;
        if (life <= 0)
            Destroy(this.gameObject);
    }
}
