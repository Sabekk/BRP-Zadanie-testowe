using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
	#region VARIABLES

	private static T _instance;

	#endregion

	#region PROPERTIES

	public static T Instance
	{
		get
		{
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	#endregion

	#region UNITY_METHODS

	protected virtual void Awake()
	{
		{
			if (_instance == null)
				_instance = this as T;
			else
			{
				Debug.LogWarning("Instance exist!", this);
				return;
			}
		}
	}

	#endregion
}
