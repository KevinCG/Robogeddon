using UnityEngine;
using System.Collections;

public class animationUnity : MonoBehaviour
{

    private Animator animator;
    private float horMov;
    private float verMov;
	void Start ()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("walk", 1f);
    }
	
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward * 300, Color.green);
        Debug.DrawRay(transform.position, transform.right * 300, Color.red);

        if (Input.GetKeyDown("1"))
        {
            animator.Play("WAIT00", -1, 0f);
        }
        else if(Input.GetKeyDown("2"))
        {
            animator.Play("DAMAGED00", -1, 0f);
        }

        Vector3 agentVelocity = GetComponent<unityChanBot>().getNavAgentVelocity();

        horMov = agentVelocity.x;
        verMov = agentVelocity.z;

        animator.SetFloat("horMov", horMov);
        animator.SetFloat("verMov", verMov);
       // animator.SetBool("hit", false);
    }

    public void playDamagedAnimation()
    {
        if (GetComponent<unityChanBot>().getMyHealth() > 0)
        {
            Debug.Log("hello");
            animator.SetFloat("damage",1f);
           // animator.SetBool("hit", true);

            StartCoroutine(setBoolFalse());
        }
        else
        {
            animator.SetBool("dead", true);
        }
    }

    IEnumerator setBoolFalse()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("hit", false);
        animator.SetFloat("damage", 0f);
    }
}
