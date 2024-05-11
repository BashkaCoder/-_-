using UnityEngine;
using UnityEngine.UI;

/// <summary> Класс первого сценария обучения.</summary>
public class FirstCraftScenario : TutorialScenario
{
    /// <summary> Кнопка постройки.</summary>
    [SerializeField] private Button buildButton;
    /// <summary> Кнопка первой построенной комнаты.</summary>
    [SerializeField] private Button firstRoomButton;
    /// <summary> Кнопка изменения специализации.</summary>
    [SerializeField] private Button specializationSwitch;

    /// <summary> Инициализировать сценарий.</summary>
    public override void Initialize()
    {
        manager.DialogueManager.SetDialogue(dialogueXml);
    }

    /// <summary> Выполнить сценарий.</summary>
    public override void Execute(int phraseIndex)
    {
        switch (phraseIndex)
        {
            case 0: //Приветствую! Ваше бизнес-путешествие начинается здесь. Для начала построим наше предприятия
                {
                    manager.DialogueManager.SwitchDialoguePanel(true);
                    manager.Bat.MoveBat(beginWaypoint.position, midWaypoint.position, moveTime);
                    manager.FocusButton(buildButton);
                    break;
                }
            case 1: //Построим нашу первую комнату
                {
                    manager.FocusButton(firstRoomButton);
                    break;
                }
            case 2: //Поздравляю, вы построили первую комнату. Подсвеченные маркеры обозначают те структуры предприятия, которые вы можете изменить.
                {
                    break;
                }
            case 3: //С помощью этого элемента вы переключаете режим стройки/разрушения
                {
                    break;
                }
            case 4: //С помощью этого элемента вы переключаете режим комнат/специализаций. Давайте построим комнату производства зелья, нажмите на этот элемент.
                {
                    manager.FocusButton(specializationSwitch);
                    break;
                }
            case 5: //Нажмите на зеленый плюс, что добавить специализацию пустой комнате.
                {
                    break;
                }
            case 6: //Выберите оборудование
                {
                    break;
                }
            case 7: //Отлично! На это мы закончили процесс редактирования структуры предприятия. Нажмите на выделенный элемент, чтобы выйти из режима редактирования
                {
                    break;
                }
            case 8:
                {
                    break;
                }
        }
        manager.DialogueManager.ContinueDialogue();
    }
}
