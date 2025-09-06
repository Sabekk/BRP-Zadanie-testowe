using UnityEngine;

public class SoulEnemy : UISelectableButtonWithAction, IEnemy
{
    [SerializeField] private GameObject InteractionPanelObject;
    [SerializeField] private GameObject ActionsPanelObject;
    [SerializeField] private SpriteRenderer EnemySpriteRenderer;

    private SpawnPoint _enemyPosition;

    public EnemyData Data { get; private set; }
    public bool InCombat{ get; private set; }

    public override void OnDeselect()
    {
        base.OnDeselect();
        DeactiveCombatWithEnemy();
    }

    public void SetupEnemy(EnemyData data, SpawnPoint spawnPoint)
    {
        Data = data;
        EnemySpriteRenderer.sprite = Data.Icon;
        _enemyPosition = spawnPoint;
        gameObject.SetActive(true);
        InCombat = false;
    }

    public SpawnPoint GetEnemyPosition()
    {
        return _enemyPosition;
    }

    public GameObject GetEnemyObject()
    {
        return this.gameObject;
    }

    private void ActiveCombatWithEnemy()
    {
        ActiveInteractionPanel(false);
        ActiveActionPanel(true);
        InCombat = true;
    }

    private void DeactiveCombatWithEnemy()
    {
        ActiveInteractionPanel(true);
        ActiveActionPanel(false);
        InCombat = false;
    }

    private void ActiveInteractionPanel(bool active)
    {
        InteractionPanelObject.SetActive(active);
    }

    private void ActiveActionPanel(bool active)
    {
        ActionsPanelObject.SetActive(active);
    }

    private void UseBow()
    {
        GameEvents.EnemyKilled?.Invoke(new EnemyKilledEventArgs(this, DamageType.BOW));
    }

    private void UseSword()
    {
        GameEvents.EnemyKilled?.Invoke(new EnemyKilledEventArgs(this, DamageType.SWORD));
    }

    #region OnClicks

    public void Combat_OnClick()
    {
        ActiveCombatWithEnemy();
    }

    public void Bow_OnClick()
    {
        UseBow();
    }

    public void Sword_OnClick()
    {
        UseSword();
    }

    #endregion
}


public interface IEnemy
{
    EnemyData Data { get; }
    SpawnPoint GetEnemyPosition();
    GameObject GetEnemyObject();
}
