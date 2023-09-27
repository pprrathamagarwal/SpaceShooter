using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    
    private float _speed=3.5f;
    private float _speedmul=2f;
    [SerializeField]

    private GameObject _rengine, _lengine;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _TS;
    private float _canfire =-1f;

    
    private float _firerate = 0.15f;
    private int _lives = 3;
    private Spawn_Manager _spawnManager;
    [SerializeField]
    private bool _isTSactive=false;
    [SerializeField]
    private bool _isspeed=false;
    [SerializeField]
    private bool _isshield=false;
    [SerializeField]
    private GameObject _shieldViz;

    [SerializeField]
    private int _score=0;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _laseraudio;
    [SerializeField]
    private AudioSource _lasersource;

 

    // Start is called before the first frame update
    void Start()
    {
    _shieldViz.SetActive(false);
    transform.position= new Vector3(0,0,0);
     _spawnManager=GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
     _uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
     _lasersource=GetComponent<AudioSource>();
            if(_spawnManager==null)
            {
                Debug.LogError("The spawn manager is null");
            }  
            if(_uiManager==null)
            {
                Debug.LogError("The UI Manager is null");
            }  
            if(_lasersource==null)
            {
                Debug.LogError("The Laser Source is null");
            }
            else
            {
                _lasersource.clip= _laseraudio;
            }  
                        

    }

    // Update is called once per frame
    void Update()
    {
       Movement();
       if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
       {
        Shoot();
       }

       
    }

    void Movement()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(Horizontal,Vertical,0);
        if(_isspeed==false)
            {
            transform.Translate(direction * _speed * Time.deltaTime);
            }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime * _speedmul);
        }        
        transform.position = new Vector3 (transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);

        if (transform.position.x >= 11f)
        {
            transform.position= new Vector3(-11f,transform.position.y,0);
        }
        else if (transform.position.x <= -11f)
        {
            transform.position= new Vector3(11f,transform.position.y,0);
        }
    }
    void Shoot()
    {
        
       if(_isTSactive == false)
        { _canfire=Time.time+_firerate;
        Instantiate(_laserprefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
        }       
        else
        {
            Instantiate(_TS, transform.position, Quaternion.identity);
        }
        _lasersource.Play();
    }

    public void damage()
    {
        if(_isshield==true)
        {
            _isshield=false;
            _shieldViz.SetActive(false);
            return;
        }
        _lives-=1;
        if(_lives==2)
            {
                _rengine.SetActive(true);
            }
        if(_lives==1)
            {
                _lengine.SetActive(true);
                _rengine.SetActive(true);
            }
        

        _uiManager.updatelives(_lives);

        if (_lives <=0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TSactive()
    {
        _isTSactive = true;
        StartCoroutine(TSPowerRoutine());
    }
    IEnumerator TSPowerRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTSactive = false;
    }

    public void SpeedActive()
    {
        _isspeed = true;
        StartCoroutine(SpeedPowerRoutine());
    }
        public void ShieldActive()
    {
        _isshield = true;
        _shieldViz.SetActive(true);
    }

    public void addscore()
    {
        _score+=10;
        _uiManager.Scoreupdate(_score);
    }

        IEnumerator SpeedPowerRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isspeed = false;
    }


}
