using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> Класс управления общими звуками кнопок.</summary>
public class RandomButtonSoundManager : MonoBehaviour
{
    /// <summary> Ссылка на RandomButtonSoundManager.</summary>
    public static RandomButtonSoundManager Instance { get; private set; }

    /// <summary> Список обычных звуков кнопок.</summary>
    [SerializeField]
    private AudioClip[] buttonSounds;

    /// <summary> Список звуков карты.</summary>
    [SerializeField]
    private AudioClip[] mapSounds;

    /// <summary> Список звуков денег.</summary>
    [SerializeField]
    private AudioClip[] moneySounds;

    /// <summary> Список звуков добавления специализаций комнаты.</summary>
    [SerializeField]
    private AudioClip[] specializationAdding;

    /// <summary> Список звуков добавления специализаций комнаты.</summary>
    [SerializeField]
    private AudioClip[] specializationRemoving;

    /// <summary> Список звуков назначения сотрудников.</summary>
    [SerializeField]
    private AudioClip[] workerAssignmentSounds;

    /// <summary> Список назанчения продуктов.</summary>
    [SerializeField]
    private AudioClip[] productSelectSounds;

    /// <summary> Список звуков разрушения комнат.</summary>
    [SerializeField] AudioClip[] roomDeleteSounds;

    /// <summary> Список тэгов звуков.</summary>
    [SerializeField]
    private string mapTag = "Map";
    [SerializeField]
    private string moneyTag = "Money";
    [SerializeField]
    private string specAddTag = "specAdd";
    [SerializeField]
    private string workerAss = "workerAss";
    [SerializeField]
    private string presAss = "presAss";

    /// <summary> Ссылка на AudioSource.</summary>
    private AudioSource audioSource;

    /// <summary> Awake.</summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary> OnEnable.</summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StationSelectUI.OnEquipmentSelected += PlayRandomSpecializationSound;
        Room.OnDeleteEquipmentButtonClick += PlayRandomDeleteSpecializationSound;
        TileImageChanger.OnRoomDestroyed += PlayRandomRoomDestroySound;
        WorkerInstance.OnWorkerSelected += PlayRandomWorkerSelectSound;
        ItemInstance.OnItemSelected += PlayRandomProductSelectSound;
    }

    /// <summary> OnDisable.</summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StationSelectUI.OnEquipmentSelected -= PlayRandomSpecializationSound;
        Room.OnDeleteEquipmentButtonClick -= PlayRandomDeleteSpecializationSound;
        TileImageChanger.OnRoomDestroyed -= PlayRandomRoomDestroySound;
        WorkerInstance.OnWorkerSelected -= PlayRandomWorkerSelectSound;
        ItemInstance.OnItemSelected -= PlayRandomProductSelectSound;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddButtonListeners();
    }

    /// <summary> Добавление listener-а на кнопки.</summary>
    private void AddButtonListeners()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayRandomButtonSound(button));
        }
    }

    /// <summary> Метод выбора и активация звука.</summary>
    private void PlayRandomButtonSound(Button button)
    {
        AudioClip[] sounds;

        if (button.gameObject.CompareTag(mapTag))
        {
            sounds = mapSounds;
        }
        else if (button.gameObject.CompareTag(moneyTag))
        {
            sounds = moneySounds;
        }
        else if (button.gameObject.CompareTag(specAddTag))
        {
            sounds = specializationAdding;
        }
        else
        {
            sounds = buttonSounds;
        }

        int randomIndex = Random.Range(0, sounds.Length);

        audioSource.PlayOneShot(sounds[randomIndex]);
    }

    /// <summary> Выбор звуков назначения специализации.</summary>
    private void PlayRandomSpecializationSound(Room r, StationData s)
    {
        PlayRandomSound(specializationAdding);
    }

    /// <summary> Выбор звуков удаления специализации.</summary>
    private void PlayRandomDeleteSpecializationSound(Room r)
    {
        PlayRandomSound(specializationRemoving);
    }

    /// <summary> Выбор звуков разрушения комнаты.</summary>
    private void PlayRandomRoomDestroySound()
    {
        PlayRandomSound(roomDeleteSounds);
    }

    /// <summary> Выбор звуков назначения сотрудника.</summary>
    private void PlayRandomWorkerSelectSound(EmployeeData emp)
    {
        PlayRandomSound(workerAssignmentSounds);
    }

    /// <summary> Выбор звуков назначения продукта.</summary>
    private void PlayRandomProductSelectSound(ProductData _)
    {
        PlayRandomSound(productSelectSounds);
    }

    /// <summary> Проигрывание звука в зависимости от выбранного списка звуков.</summary>
    private void PlayRandomSound(AudioClip[] clips)
    {
        AudioClip rclip = Extensions.GetRandomElement(clips);
        audioSource.PlayOneShot(rclip);
    }
}
