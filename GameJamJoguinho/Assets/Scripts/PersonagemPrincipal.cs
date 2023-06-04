using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemPrincipal : MonoBehaviour
{
    private float velocidade = 5f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Cima
        if (vertical > 0f) {
            animator.SetInteger("andandoVertical", 1);
        }

        //Baixo
        if (vertical < 0f)
        {
            animator.SetInteger("andandoVertical", -1);
        }

        //Direita
        if (horizontal > 0f)
        {
            animator.SetInteger("AndandoHorizontal", 1);
        }

        //Esquerda
        if (horizontal < 0f)
        {
            animator.SetInteger("AndandoHorizontal", -1);
        }

        //Idle Vertical
        if (vertical == 0f)
        {
            animator.SetInteger("andandoVertical", 0);
        }

        //Idle Horizontal
        if (horizontal == 0f)
        {
            animator.SetInteger("AndandoHorizontal", 0);
        }

        transform.Translate(new Vector3(horizontal, vertical, 0f) * velocidade * Time.deltaTime);
    }
}
