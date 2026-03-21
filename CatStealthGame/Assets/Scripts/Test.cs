using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    int lives;
    private void Awake()
    {
        
    }

    private int Died()
    {
        lives -= 1;
        return lives;
    }
}
