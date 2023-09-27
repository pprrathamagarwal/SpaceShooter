using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour

{
    [SerializeField]
    private Text _scoretext; 
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite [] _lives;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoretext.text= "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager==null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    public void Scoreupdate(int playerscore)
    {
         _scoretext.text= "Score: " + playerscore.ToString();
    }

    public void updatelives(int currlives)
    {
        _livesImg.sprite=_lives[currlives];
        if(currlives==0)
        {
                   GameOverSeq();
        }
    }
    
    void GameOverSeq()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverRoutine());

    }



    IEnumerator GameOverRoutine()
    {
        while(true)
        {
        _gameOverText.text = "GAME OVER";
        yield return new WaitForSeconds(0.5f);
        _gameOverText.text="";
        yield return new WaitForSeconds(0.5f);
        } 
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
