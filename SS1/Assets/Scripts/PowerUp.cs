using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{   [SerializeField]
 private float _speed=3.0f;
 [SerializeField]
 private int powerupid;
 [SerializeField]
 private AudioClip _audioclip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <=-5)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Player player =  other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioclip,transform.position);
            if(player != null)
                {
                if(powerupid==0)
                {
                    player.TSactive();
                }
                else if(powerupid==1)
                {
                    player.SpeedActive();
                }
                else if(powerupid==2)
                {
                    player.ShieldActive();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
