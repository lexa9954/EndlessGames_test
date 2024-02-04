using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPC_controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения персонажа
    public GameObject bulletPrefab; // Префаб снаряда
    public Transform firePoint; // Точка, откуда будут вылетать снаряды

    private Rigidbody rb; // Компонент Rigidbody для управления физикой персонажа
    private Vector3 movement; // Вектор для хранения направления движения
    private Animator animator; // Компонент Animator для управления анимациями персонажа

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody при запуске
        animator = GetComponent<Animator>(); // Получаем компонент Animator при запуске
    }

    void Update()
    {
        // Получаем ввод с клавиатуры для управления персонажем
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Создаем вектор направления движения
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Если есть направление движения, поворачиваем персонажа в эту сторону
        if (movement != Vector3.zero)
        {
            transform.LookAt(transform.position + movement);
        }

        // Обновляем параметры аниматора в зависимости от направления движения
        animator.SetFloat("Speed", movement.magnitude);

        // Обработка стрельбы
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot"); // Устанавливаем триггер анимации стрельбы
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Перемещаем персонажа с учетом скорости и направления
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        // Создаем снаряд в точке firePoint и с его текущим вращением
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
