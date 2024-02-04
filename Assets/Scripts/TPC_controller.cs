using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPC_controller : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ����������� ���������
    public GameObject bulletPrefab; // ������ �������
    public Transform firePoint; // �����, ������ ����� �������� �������

    private Rigidbody rb; // ��������� Rigidbody ��� ���������� ������� ���������
    private Vector3 movement; // ������ ��� �������� ����������� ��������
    private Animator animator; // ��������� Animator ��� ���������� ���������� ���������

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // �������� ��������� Rigidbody ��� �������
        animator = GetComponent<Animator>(); // �������� ��������� Animator ��� �������
    }

    void Update()
    {
        // �������� ���� � ���������� ��� ���������� ����������
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // ������� ������ ����������� ��������
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // ���� ���� ����������� ��������, ������������ ��������� � ��� �������
        if (movement != Vector3.zero)
        {
            transform.LookAt(transform.position + movement);
        }

        // ��������� ��������� ��������� � ����������� �� ����������� ��������
        animator.SetFloat("Speed", movement.magnitude);

        // ��������� ��������
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot"); // ������������� ������� �������� ��������
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // ���������� ��������� � ������ �������� � �����������
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        // ������� ������ � ����� firePoint � � ��� ������� ���������
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
