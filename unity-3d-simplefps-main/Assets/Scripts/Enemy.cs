using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetPlayer;
    public string tagObject;
    Animator animator;
    NavMeshAgent agent;
    public int health = 5;
    private Transform tBase
    {
        get { return transform.GetChild(0); }
    }
    private Transform tWeapon
    {
        get { return tBase.GetChild(0); }
    }
    private Transform tOrigin
    {
        get { return tWeapon.GetChild(0); }
    }
    public int chaseZ1;
    public int chaseZ2;
    public int chaseX1;
    public int chaseX2;
    public int damage = 10;
    public int Time = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPlayer.position.x <= chaseX1 && targetPlayer.position.x >= chaseX2 && targetPlayer.position.z <= chaseZ1 && targetPlayer.position.z >= chaseZ2){
            animator.SetBool("Moving", true);
            agent.isStopped = false;
            agent.SetDestination(targetPlayer.position);
        }else{
            agent.isStopped = true;
            animator.SetBool("Moving", false);
            animator.SetBool("Attack", false);
        }
    }   
    private void TakeDamage()
    {
        health--;
        if (health <= 0)
        {        

            enabled = false;
            animator.SetTrigger("Death");
            agent.isStopped = true;
            StartCoroutine(clearObject());
        }
    }
    IEnumerator clearObject(){
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
     private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            animator.SetBool("Attack", true);
            other.collider.SendMessage("TakeDamage", damage);
        }
    }
    private void OnCollisionStay(Collision other) 
    {
        if (other.collider.tag == "Player")
        {
            Time += 1;
            animator.SetBool("Attack", true);
            if(Time >= 1000)
            {
                other.collider.SendMessage("TakeDamage", damage);
                Time = 0;
            }
        }
    }
}
