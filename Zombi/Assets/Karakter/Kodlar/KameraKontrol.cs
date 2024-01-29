using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform hedef;
    public Vector3 hedefMesafe;

    [SerializeField]
    private float fareHassasiyeti;
    float fareX, fareY;

    Vector3 objRot;
    public Transform karakterVucut;
    KarakterKontrol karakterHp;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        karakterHp = GameObject.Find("SWAT").GetComponent<KarakterKontrol>();
    }

    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (karakterHp.YasiyorMu() == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, hedef.position + hedefMesafe, Time.deltaTime * 10);

            fareX += Input.GetAxis("Mouse X") * fareHassasiyeti;
            fareY += Input.GetAxis("Mouse Y") * fareHassasiyeti;

            if (fareY >= 18)
            {
                fareY = 18;
            }

            if (fareY <= -30)
            {
                fareY = -30;
            }
            this.transform.eulerAngles = new Vector3(fareY, fareX, 0);
            hedef.transform.eulerAngles = new Vector3(0, fareX, 0);

            Vector3 gecici = this.transform.localEulerAngles;
            gecici.z = 0;
            gecici.y = this.transform.localEulerAngles.y;
            gecici.x = this.transform.localEulerAngles.x + 10;
            objRot = gecici;
            karakterVucut.transform.eulerAngles = objRot;
        }
    }
}
