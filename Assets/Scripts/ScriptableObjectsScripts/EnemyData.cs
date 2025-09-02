using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _score = 10;
    [SerializeField] private EnemyWeakType _weakType;
    [SerializeField] private float _scoreMultiplerForWeakness = 1.5f;

    #endregion

    #region PROPERTIES

    public string Name => _name;
    public string Dscription => _description;
    public Sprite Icon => _icon;
    public int Score => _score;
    public EnemyWeakType WeakType => _weakType;
    public float ScoreMultiplerForWeakness => _scoreMultiplerForWeakness;

    #endregion
}
