using UnityEngine;

public class PlayerManager : BaseManager
{
    private AIManager _aiManager;
    [SerializeField] protected CanvasGroup _PlayerButtonGroup;
    protected override void Start()
    {
        base.Start();
        _aiManager = GetComponent<AIManager>(); //Checks to see if AIManager is attached
        if (_aiManager == null)
        {
            Debug.LogError("AIManager not found");
        }

        if (_PlayerButtonGroup == null)
        {
            Debug.LogError("CanvasGroup _buttonGroup not attached"); //Checks to see if _buttonGroup is attached
        }
        
    }

    public override void TakeTurn()
    {
        _PlayerButtonGroup.interactable = true; //give the ability to click on a button
    }

    protected override void EndTurn()
    {
        _PlayerButtonGroup.interactable = false; //remvoes the ability to click on the button
        _aiManager.TakeTurn();
    }

    public void Splash()
    {
        _aiManager.DealDamage(40.3f);
        EndTurn();
    }

    public void IronTail()
    {
        _aiManager.DealDamage(10f); //deals 10 damage
        EndTurn();
    }

    public void Rest()
    {
        Heal(30f); //heals player health by 50 and ends the turn
        EndTurn();
    }

    public void SelfDestruct()
    {
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80f);
        EndTurn();
    }
}
