using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _score = 10;
    [SerializeField] private DamageType _weakType;
    [SerializeField] private float _scoreMultiplerForWeakness = 1.5f;

    #endregion

    #region PROPERTIES

    public string Name => _name;
    public string Dscription => _description;
    public Sprite Icon => _icon;
    public DamageType WeakType => _weakType;
    public float Score => _score;
    public float ScoreMultiplerForWeakness => _scoreMultiplerForWeakness;

    #endregion
}
