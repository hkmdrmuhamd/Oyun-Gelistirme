using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float zombieHP = 100;
    bool zombieDied;
    Animator anim;
    GameObject targetPlayer;
    public float yaklasma;
    public float saldirmaMesafesi;
    NavMeshAgent zombieNavMesh;
    AudioSource sesKaynagi;
    public AudioClip saldirmaSesi;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        targetPlayer = GameObject.Find("SWAT");
        zombieNavMesh = this.GetComponent<NavMeshAgent>();
        sesKaynagi = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (zombieHP <= 0)
        {
            zombieDied = true;
        }
        if (zombieDied == true)
        {
            anim.SetBool("died", true);
            StartCoroutine(YokOl());

        }

        else
        {
            float mesafe = Vector3.Distance(this.transform.position, targetPlayer.transform.position);
            if (mesafe < yaklasma)
            {
                zombieNavMesh.isStopped = false;
                zombieNavMesh.SetDestination(targetPlayer.transform.position);
                anim.SetBool("walking", true);
                anim.SetBool("attacking", false);
                this.transform.LookAt(targetPlayer.transform.position);
            }
            else
            {
                zombieNavMesh.isStopped = true;
                anim.SetBool("walking", false);
                anim.SetBool("attacking", false);
            }
            if (mesafe < saldirmaMesafesi)
            {
                zombieNavMesh.isStopped = true;
                anim.SetBool("walking", false);
                anim.SetBool("attacking", true);
                this.transform.LookAt(targetPlayer.transform.position);
            }
        }
    }

    public void HasarVermeSes()
    {
        sesKaynagi.PlayOneShot(saldirmaSesi);
    }

    public void HasarVerme()
    {
        targetPlayer.GetComponent<KarakterKontrol>().HasarAl();
    }

    IEnumerator YokOl()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    public void TakeDamage()
    {
        zombieHP -= Random.Range(10, 15);
        if (zombieHP <= 0)
        {
            zombieHP = 0;
        }
    }
}
