using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; // Singleton instance
    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        // Membuat Player menjadi Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Mengambil informasi dari PlayerMovement dan Animator
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("Engine/EngineEffect").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Memanggil method Move dari PlayerMovement
        playerMovement.Move();
    }

    private void LateUpdate()
    {
        // Mengatur nilai Bool dari parameter IsMoving pada Animator
        bool isMoving = playerMovement.IsMoving();
        animator.SetBool("IsMoving", isMoving);
        Debug.Log("Animator IsMoving set to: " + isMoving); // Log untuk memeriksa nilai IsMoving
    }
}
