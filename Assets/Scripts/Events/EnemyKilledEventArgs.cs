public class EnemyKilledEventArgs
{
    #region PROPERTIES

    public IEnemy Enemy { get; }
    public DamageType UsedDamage { get; }

    #endregion

    #region CONSTRUCTORS

    public EnemyKilledEventArgs(IEnemy enemy, DamageType usedDamage)
    {
        Enemy = enemy;
        UsedDamage = usedDamage;
    }

    #endregion

    #region METHODS

    public bool WasKilledByWeakness()
    {
        return (Enemy.Data.WeakType & UsedDamage) != 0;
    }

    #endregion

}