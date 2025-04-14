using UnityEngine;
using System.Collections;

public class mincheolDance : MonoBehaviour
{
    public Animator animator;

    public bool dancing = false;
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GOGO();
    }

    void GOGO()
    {
        if(Input.GetKeyDown(KeyCode.F) && !dancing) 
        {
            animator.SetTrigger("Dance");
            dancing = true;
            animator.SetBool("Dancing", true);
        } else if(Input.GetKeyDown(KeyCode.F) && dancing)
        {
            dancing = false;
            animator.SetBool("Dancing", false);
        }
    }

}
