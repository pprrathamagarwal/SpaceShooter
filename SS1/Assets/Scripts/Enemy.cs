using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed=4.0f;
    private Player _player;
    private Spawn_Manager _spawnManager;
    private Animator _anim;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
      _player = GameObject.Find("Player").GetComponent<Player>();
      _audioSource=GetComponent<AudioSource>();
      if(_player==null)
      {
        Debug.LogError("The player is null");
      }
      _anim=GetComponent<Animator>();

      if (_anim==null)
      {
        Debug.LogError("The anim is null");
      }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y<=-5f)
        {
            float randomx = Random.Range(-8f,8f);
            transform.position= new Vector3(randomx,7f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
           Player player = other.transform.GetComponent<Player>();
           {
            if(player!=null) 
            {
                player.damage();
            }
           }

        }

        if (other.tag=="Laser")
        {
            Destroy(other.gameObject);
            _anim.SetTrigger("OnEnemyDeath");
            _speed=0;
            if(_player!=null)
            {
                _player.addscore();
            }
            _audioSource.Play();
            Destroy(this.gameObject,2.5f);

        }

    }
}
