using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public float jumpForce = 6.0f;   // ������ �� ������ ��
    private Rigidbody2D rb;           // Rigidbody2D ������Ʈ ����
    private bool isGrounded = true;   // Player�� ���� �ִ��� Ȯ���ϱ� ���� ����
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D ������Ʈ ��������
    }

    public void Jump()
    {
        if (isGrounded)  // ���� ���� ���� ���� ����
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;  // ���� �Ŀ��� ���� ���� ����
            animator.SetBool("isjump", true);
       
        }
    }

    // ���� ������ isGrounded�� true�� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isjump", false);
        }
    }
   
}
