using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtisSistemi : MonoBehaviour
{
    Camera kamera;
    public LayerMask zombieLayer;
    KarakterKontrol hpKontrol;
    Animator anim;
    public ParticleSystem muzzleFlash;
    private float sarjor = 30;
    private float cephane = 120;
    private float sarjorKapasitesi = 30;
    AudioSource sesKaynagi;
    public AudioClip atesSes;
    public AudioClip reloadSes;

    void Start()
    {
        kamera = Camera.main;
        hpKontrol = this.GetComponent<KarakterKontrol>();
        anim = this.gameObject.GetComponent<Animator>();
        sesKaynagi = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hpKontrol.YasiyorMu() == true)
        {
            if (Input.GetMouseButton(0))
            {
                if (sarjor > 0)
                {
                    anim.SetBool("atesEt", true);
                }
                if (sarjor <= 0)
                {
                    anim.SetBool("atesEt", false);
                }
                if (sarjor <= 0 && cephane > 0)
                {
                    anim.SetBool("reloading", true);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("atesEt", false);
            }
        }
    }

    public void SarjorDegistirmeSes()
    {
        sesKaynagi.PlayOneShot(reloadSes);
        sesKaynagi.volume = 0.6f;
    }

    public void Reloading()
    {
        sesKaynagi.volume = 1f;
        cephane -= sarjorKapasitesi - sarjor;
        sarjor = sarjorKapasitesi;
        anim.SetBool("reloading", false);
    }

    public void AtesEtme()
    {
        if (sarjor > 0)
        {
            muzzleFlash.Play();
            sesKaynagi.PlayOneShot(atesSes);
            Ray ray = kamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, zombieLayer))
            {
                hit.collider.gameObject.GetComponent<Zombie>().TakeDamage();
            }
            sarjor--;
        }
    } 

    public float GetSarjor()
    {
        return sarjor;
    }

    public float GetCephane()
    {
        return cephane;
    }
}
