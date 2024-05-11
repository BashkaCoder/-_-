
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary> Менеджер диалогов. </summary>
public class DialogueManager : MonoBehaviour
{
    public int PhraseIndex { get { return phraseIndex; } }
    /// <summary> Диалоговая панель.</summary>
    [SerializeField] private Button[] dialoguePanels;

    /// <summary> Текст диалоговой панели.</summary>
    [SerializeField] private TMP_Text dialoguePanelText;

    /// <summary> Туториал.</summary>
    [SerializeField] private TutorialManager tutorial;

    /// <summary> Диалог, считанный из XML файла.</summary>
    private Dialogue dialogue;

    /// <summary> Индекс фразы в диалоге.</summary>
    private int phraseIndex;

    /// <summary> Задержка при вылетании символа в тексте.</summary>
    private const float textDelay = 0.03f;

    /// <remarks> Происходит выбор локализованного текста, инициализация обучения нулевым индексом.</remarks>
    public void SetDialogue(TextAsset dialogueXml)
    {
        phraseIndex = 0;

        // Очищаем диалоговую панель.
        dialoguePanelText.text = string.Empty;

        // Загружаем данные из XML файла. 
        dialogue = Dialogue.Load(dialogueXml);
    }

    /// <summary> Продолжить диалог.</summary>
    public void ContinueDialogue()
    {
        if (phraseIndex >= dialogue.Nodes.Length)
        {
            Debug.Log("End of dialogue");
            return;
        }
        StartCoroutine(WriteSentence(dialogue.Nodes[phraseIndex].Text));
    }

    /// <summary> Корутина, которая пишет предложение на диалоговой панели.</summary>
    /// <remarks> Корутина выдаёт по одному символу с определённой задержкой.</remarks>
    /// <returns> Возвращает предложение, которое отображается на диалоговой панели.</returns>
    public IEnumerator WriteSentence(string sentence)
    {
        // Очищаем диалоговую панель.
        dialoguePanelText.text = string.Empty;

        // Добавляем символы с задержкой.
        foreach (char symbol in sentence)
        {
            dialoguePanelText.text += symbol;
            yield return new WaitForSeconds(textDelay);
        }
    }

    /// <summary> Событие на нажатие кнопки диалоговой панели.</summary>
    public void OnClickDialogue()
    {
        // Если фраза отображена на панели полностью.
        if (dialoguePanelText.text == dialogue.Nodes[phraseIndex].Text)
        {
            // Если фраза завершающая, то заканчиваем диалог.
            if (dialogue.Nodes[phraseIndex].IsEnd)
            {
                phraseIndex++;
                tutorial.EndDialogue(phraseIndex);
            }
            else // Иначе продолжаем.
            {
                NextSentence();
            }
        }
        else
        {
            StopAllCoroutines();

            // Выдаваём сразу весь текст.
            dialoguePanelText.text = dialogue.Nodes[phraseIndex].Text;
        }
    }

    /// <summary> Следующее предложение.</summary>
    private void NextSentence()
    {
        if (phraseIndex < dialogue.Nodes.Length - 1)
        {
            phraseIndex++;
            StartCoroutine(WriteSentence(dialogue.Nodes[phraseIndex].Text));
        }
        else
        {
            tutorial.EndDialogue(phraseIndex);
        }
    }

    /// <summary> Включает и выключает кнопки на тёмных экранах. </summary>
    /// <param name="isEnabled"></param>
    public void SwitchDialoguePanel(bool isEnabled)
    {
        foreach (var dialoguePanel in dialoguePanels)
        {
            dialoguePanel.enabled = isEnabled;
        }
    }

    /// <summary> Пропустить предложение.</summary>
    public void SkipSentence()
    {
        StopAllCoroutines();

        phraseIndex++;
    }
}
