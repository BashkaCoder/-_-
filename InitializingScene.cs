using UnityEngine;

/// <summary> Инициализация сцены игры.</summary>
/// <remarks> Данный класс подготавливает сцену к игре.</remarks>
public class InitializingScene : MonoBehaviour
{
    #region Private Fields
    /// <summary> Узлы, которые необходимо закрыть.</summary>
    [SerializeField] private GameObject[] nodesToClose;

    /// <summary> Узлы, которые необходимо открыть.</summary>
    [SerializeField] private GameObject[] nodesToOpen;
    #endregion

    #region Private Methods
    /// <summary> При старте открываем и закрываем необходимые узлы и 
    /// задаем своё название замку.</summary>
    private void Start()
    {
        InitializeGame();
    }

    /// <summary> Инициализация игры.</summary>
    private void InitializeGame()
    {
        //GameInterface.CloseNodes(nodesToClose);
        //GameInterface.OpenNodes(nodesToOpen);
    }
    #endregion
}