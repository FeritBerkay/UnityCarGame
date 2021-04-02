using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float Jump_speed;
    Rigidbody2D rigid;
    public float Fast;
    int puan;
    public bool karakterYerde;
    public Text puantext;
    public Text TimeText;
    public Text Uyarılar;
    Vector3 Fark;
    public GameObject Kamera;
    Vector3 Toplam;
    float time;
    int time2;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Fark = Kamera.transform.position - transform.position;
        Uyarılar.text = "You have 20 second ";
    }

    void Update()
    {
        TimeText.text = "time :" + time2;
        puantext.text = "Puan :" + puan.ToString();
        Toplam = transform.position + Fark;
        Kamera.transform.position = new Vector3(Toplam.x, Toplam.y, Kamera.transform.position.z);
        car_move();
        if (Input.GetKeyDown(KeyCode.Space) && karakterYerde == true)
        {
            car_jump();
        }
        car_Time();
        if (time2 == 1)
        {
            Uyarılar.gameObject.SetActive(false);
        }
        else if (time2 == 20)
        {
            Uyarılar.gameObject.SetActive(true);
            Uyarılar.text = "You died!!";
        }
        else if (time2 == 21)
        {
            SceneManager.LoadScene(0);
        }
    }
    void car_move()
    {
        horizontal = Input.GetAxis("Horizontal");
        rigid.AddForce(new Vector3(horizontal * Fast, 0, 0));
    }
    void car_jump()
    {
        rigid.AddForce(Vector2.up * Jump_speed * 100);
        karakterYerde = false;
    }
    void car_Time()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            time2++;
            time = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            puan++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {

            Uyarılar.gameObject.SetActive(true);
            Uyarılar.text = "Bitti Bitti";
            SceneManager.LoadScene(0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floot")
        {
            karakterYerde = true;
        }

    }
}

