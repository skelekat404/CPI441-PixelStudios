using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goober_AI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool should_rotate;
    public float check_radius;
    public float speed;
    public float attack_range;
    public LayerMask what_is_player;
    public Vector3 direction;

    private Transform target;
    private Rigidbody2D rigid_body;
    private Vector2 move;
    private bool is_in_attack_range;
    private bool is_in_chase_range;
    private Animator ani;


    

    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

    }

   
    // Update is called once per frame
    void Update()
    {
        ani.SetBool("isRunning", is_in_chase_range);

        is_in_chase_range = Physics2D.OverlapCircle(transform.position, check_radius, what_is_player);
        is_in_attack_range = Physics2D.OverlapCircle(transform.position, attack_range, what_is_player);

        direction = target.position - transform.position;
        float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        move = direction;
        if(should_rotate)
        {
            ani.SetFloat("X", direction.x);
            ani.SetFloat("Y", direction.y);
        }
       
    }

    private void FixedUpdate()
    {
        if(is_in_chase_range && !is_in_attack_range)
        {
            MoveEnemy(move);
        }
        if(is_in_attack_range)
        {
            rigid_body.velocity = Vector2.zero;
        }
    }


    private void MoveEnemy(Vector2 direction)
    {
        rigid_body.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }


}
