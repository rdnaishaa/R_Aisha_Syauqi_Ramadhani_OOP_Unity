using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level;

    void Start()
    {
      
    }

    void Update()
    {
        // Logika dasar untuk Enemy, bisa ditambahkan di sini jika diperlukan
    }

    // Fungsi untuk mengaktifkan Enemy setelah waktu tertentu
    protected IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
    }
}
