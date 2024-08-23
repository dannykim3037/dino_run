using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public float jumpForce = 6.0f;   // 점프할 때 적용할 힘
    private Rigidbody2D rb;           // Rigidbody2D 컴포넌트 참조
    private bool isGrounded = true;   // Player가 땅에 있는지 확인하기 위한 변수
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 컴포넌트 가져오기
    }

    public void Jump()
    {
        if (isGrounded)  // 땅에 있을 때만 점프 가능
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;  // 점프 후에는 땅에 있지 않음
            animator.SetBool("isjump", true);
       
        }
    }

    // 땅에 닿으면 isGrounded를 true로 설정
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isjump", false);
        }
    }
   
}
