using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarakterKontrol : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private float karakterHiz;

    private float hp = 100;
    bool canli;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        canli = true;
    }

    
    void Update()
    {
        if (hp <= 0)
        {
            canli = false;
            anim.SetBool("yasiyorMu", canli);
            StartCoroutine(YenidenBaslat());
        }
        if (canli == true)
        {
            Hareket();
        }
    }

    public float GetSaglik()
    {
        return hp;
    }

    public bool YasiyorMu()
    {
        return canli;
    }

    public void HasarAl()
    {
        hp -= Random.Range(5, 10);
        if (hp <= 0)
        {
            hp = 0;
        }
    }

    void Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);

        this.gameObject.transform.Translate(yatay * karakterHiz * Time.deltaTime, 0, dikey * karakterHiz * Time.deltaTime);
    }

    IEnumerator YenidenBaslat()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
}
