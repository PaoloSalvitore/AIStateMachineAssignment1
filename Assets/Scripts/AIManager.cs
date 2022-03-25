using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AIManager : BaseManager
{
    [SerializeField] GameObject _combatCanvas;
    [SerializeField] protected CanvasGroup _buttonGroup;
    public enum State
    {
        HighHP, //Different states depending on AI health
        LowHP,
        Dead,
    }

    public State currentState; //What state the AI is currently in
    protected PlayerManager _playerManager; //Access to the Player script
    [SerializeField] protected Animator _anim; //AI animator

    protected override void Start()
    {
        base.Start();

        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.LogError("PlayerManager not found");
        }
    }


    public override void TakeTurn()
    {
        if (_health <= 0f)
        {
            currentState = State.Dead; //If the AIs health is 0 or less the AI is dead
        }
        switch (currentState)
        {
            case State.HighHP:
                HighHPState();
                break;
            case State.LowHP:
                LowHPState();
                break;
            case State.Dead:
                DeadState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void EndTurn()
    {
        StartCoroutine(WaitAndEndTurn()); //waits an end turn
    }

    private IEnumerator WaitAndEndTurn()
    {
        yield return new WaitForSecondsRealtime(2f);
        _playerManager.TakeTurn();
    }

    void LowHPState()//what to do when the AI is low health s
    {
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 1:
                SelfDestruct();
                break;
            case int i when i > 1 && i <=8:
                Rest();
                break;
            case int i when i > 8 && i <= 9:
                Splash();
                break;
        }

        if (_health > 60f)
        {
            currentState = State.HighHP;
        }
    }
    
    void HighHPState()
    {
        if (_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        
        //random.range for ints
        //min number ==  inclusive
        //max number == exclusive
        
        //20% chance to use splash
        //70% chance to use iron tail
        //10% chance to use self destruct
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 1:
                Splash();
                break;
            case int i when i > 1 && i <=8:
                IronTail();
                break;
            case int i when i > 8 && i <= 9:
                SelfDestruct();
                break;
        }
    }
    
    void DeadState() //AI is dead
    {
        Debug.Log("AI IS DEAD YOU WIN");
        _combatCanvas.SetActive(false);
       Time.timeScale = 0;
    }

    public void Splash() //If the AI chooses splash
    {
        Debug.Log("Ai casts Splash");
        _playerManager.DealDamage(40.3f);
        _anim.SetTrigger("Splash");
        EndTurn();
    }

    public void IronTail()//If the AI chooses Iron Tail
    {
        Debug.Log("Ai casts Iron Tail");
        _playerManager.DealDamage(10f);
        EndTurn();
    }

    public void Rest()//if the AI chooses Rest
    {
        Debug.Log("Ai Rests");
        Heal(50f);
        EndTurn();
    }

    public void SelfDestruct() //If the AI choses SelfDestruct
    {
        Debug.Log("Ai casts Self Destruct");
        DealDamage(_maxHealth);
        _playerManager.DealDamage(80f);
        EndTurn();
    }
}
