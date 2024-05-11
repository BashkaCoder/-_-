using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Класс, отвечающий за реализацию банковской системы.</summary>
public class Bank : MonoBehaviour
{
    /// <summary> Ползунок выбора месяцев.</summary>
    [SerializeField] private Scrollbar monthScrollbar;
    /// <summary> Ползунок выбора суммы кредита.</summary>
    [SerializeField] private Scrollbar sumScrollbar;

    /// <summary> Минимальное количество месяцев.</summary>
    private int monthAmountPanel = 3;
    /// <summary> Значение суммы кредита.</summary>
    private int sumAmountPanel = 0;
    /// <summary> Значение предстоящего платежа.</summary>
    private int payment = 0;

    /// <summary> Значение ежемесячного платежа кредита.</summary>
    private float percent = 0.03f;

    /// <summary> Текст для количества месяцев.</summary>
    [SerializeField] private TMP_Text monthAmountText;
    /// <summary> Текст для суммы кредита.</summary>
    [SerializeField] private TMP_Text sumText;
    /// <summary> Поле ввода суммы.</summary>
    [SerializeField] private TMP_InputField sumInputField;
    /// <summary> Текст для суммы следующего платежа.</summary>
    [SerializeField] private TMP_Text paymentAmountText;
    /// <summary> Текст для суммы задолженности.</summary>
    [SerializeField] private TMP_Text debtAmountText;
    /// <summary> Объект хранения даты платежей.</summary>
    [SerializeField] private GameObject paymentDates;
    /// <summary> Объект хранения даты займа.</summary>
    [SerializeField] private GameObject loanDate;
    /// <summary> Объект хранения даты овердафта.</summary>
    [SerializeField] private GameObject overdraftDate;
    /// <summary> Объект хранения кнопок выплаты.</summary>
    [SerializeField] private GameObject repayButtons;
    /// <summary> Поле ввода суммы погашения кредита.</summary>
    [SerializeField] private TMP_InputField debtInputField;
    /// <summary> Ползунок выбора суммы погашения кредита.</summary>
    [SerializeField] private Scrollbar debtScrollbar;

    /// <summary> Объект хранения отсутствия задолженностей.</summary>
    [SerializeField] private GameObject noooDebts;

    /// <summary> Значение лимита кредита.</summary>
    private int debtLimit;
    /// <summary> Флаг значения задолженности.</summary>
    private bool inputDebtState;
    /// <summary> Флаг ввода значения задолженности.</summary>
    private bool inputLoanState;
    /// <summary> Значение задолженности.</summary>
    private int debt;
    /// <summary> Значение текущей задолженности.</summary>
    private int currDebt;
    /// <summary> Ограничени суммы кредита.</summary>
    private const int LOAN_LIMIT = 50000;


    /// <summary> Сущность Банк.</summary>
    public static Bank Instance;

    /// <summary> Список задолженностей.</summary>
    public List<Loan> LoanList = new();
    /// <summary> Список овердрафтов.</summary>
    public List<Overdraft> OverdraftList = new();

    /// <summary> Класс, реализующий задолженность.</summary>
    public class Loan
    {
        /// <summary> Значение задолженности.</summary>
        public int Amount;
        /// <summary> Значение месяцев.</summary>
        public int monthAmount;
        /// <summary> Значение текущей суммы.</summary>
        public int CurrentAmount;
        /// <summary> Конструктор класса.</summary>
        public Loan(int amount, int month)
        {
            Amount = amount;
            monthAmount = month;
            CurrentAmount = amount;
        }

    }

    /// <summary> Класс, реализующий овердрафт.</summary>
    public class Overdraft // а че с ним делать ебанный в рот
    {
        /// <summary> Значение суммы.</summary>
        public int Amount;
        /// <summary> Значение месяцев.</summary>
        public int monthAmount = 2;
        /// <summary> Конструктор класса.</summary>
        public Overdraft(int amount)
        {
            Amount = amount;
        }
    }

    /// <summary> Срабатывает при старе приложения.</summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    /// <summary> Инициализация класса.</summary>
    void Start()
    {
        monthScrollbar.onValueChanged.AddListener((monthValue) => OnPercentScrollbar(monthValue));
        sumScrollbar.onValueChanged.AddListener(value => OnSumScrollbar(value));
        debtScrollbar.onValueChanged.AddListener(value => OnDebtScrollbar(value));

        debtAmountText.text = CalculateDebt().ToString();
        CheckDates();
        debt = CalculateDebt();

        OnDebtInputFieldChanged();
    }

    /// <summary> Событие изменения задолженности.</summary>
    private void OnDebtScrollbar(float value)
    {

        if (!inputDebtState)
        {
            debt = CalculateDebt();
            if (debt > Player.Instance.Money.Value)
                debt = Player.Instance.Money.Value;
            debtInputField.text = ((int)(value * debt)).ToString();
            currDebt = (int)(value * debt);
        }
    }

    /// <summary> Проверить даты.</summary>
    private void CheckDates()
    {
        if (!CheckLoans() && !CheckOverdraft())
        {
            paymentDates.SetActive(false);
        }
        else
        {
            paymentDates.SetActive(true);
            loanDate.SetActive(CheckLoans());
            overdraftDate.SetActive(CheckOverdraft());
        }
    }

    /// <summary> Срабатывает при включении.</summary>
    private void OnEnable()
    {
        Money.OnMoneyChanged += ChangeDebtLimit;
    }
    /// <summary> Срабатывает при выключении.</summary>
    private void OnDisable()
    {
        Money.OnMoneyChanged -= ChangeDebtLimit;
    }
    /// <summary> Изменить лимит кредита.</summary>
    private void ChangeDebtLimit()
    {
        debtLimit = Player.Instance.Money.Value;
    }
    /// <summary> Проверить займы.</summary>
    private bool CheckLoans()
    {
        if (LoanList.Count == 0)
        {
            return false;
        }
        else return true;
    }
    /// <summary> Проверить овердрафт.</summary>
    private bool CheckOverdraft()
    {
        if (OverdraftList.Count == 0)
        {
            return false;
        }
        else return true;
    }
    /// <summary> Посчитать долг.</summary>
    private int CalculateDebt()
    {
        return CalculateSumLoans() + CalculateSumOD();
    }
    /// <summary> Посчитать суммарный долг.</summary>
    private int CalculateSumLoans()
    {
        int sumLoan = 0;
        foreach (var loan in LoanList)
        {
            sumLoan += loan.Amount;
        }
        return sumLoan;
    }
    /// <summary> Посчитать одноразовую сумму.</summary>
    private int CalculateSumOD()
    {
        int sumOD = 0;
        foreach (var overdraft in OverdraftList)
        {
            sumOD += overdraft.Amount;
        }
        return sumOD;
    }

    /// <summary> Событие изменения процента ставки.</summary>
    private void OnPercentScrollbar(float monthValue)
    {
        monthAmountPanel = (int)(monthValue * monthScrollbar.numberOfSteps) + 3;
        monthAmountText.text = monthAmountPanel.ToString();
        CalculateMonthlyPaymentPanel();
    }
    /// <summary> Создать новый долг.</summary>
    public void CreateNewDebt(int amount)
    {
        OverdraftList.Add(new Overdraft(amount));
        debtAmountText.text = CalculateDebt().ToString();
        CheckDates();
    }
    /// <summary> Событие изменения суммы кредита.</summary>
    public void OnSumScrollbar(float sumValue)
    {
        if (!inputLoanState)
        {
            sumAmountPanel = (int)(sumValue * LOAN_LIMIT - CalculateSumLoans());
            sumInputField.text = sumAmountPanel.ToString();
            CalculateMonthlyPaymentPanel();
        }
    }
    /// <summary> Событие включения поля суммы.</summary>
    public void OnLoanInputSelected()
    {
        inputLoanState = true;
    }

    /// <summary> Событие выключения поля суммы.</summary>
    public void OnLoanInputDeselected()
    {
        inputLoanState = false;
    }

    /// <summary> Событие включения поля долга.</summary>
    public void OnDebtInputSelected()
    {
        inputLoanState = true;
    }
    /// <summary> Событие выключения поля долга.</summary>
    public void OnDebtInputDeselected()
    {
        inputLoanState = false;
    }
    /// <summary> Событие изменения поля суммы.</summary>
    public void OnSumInputFieldValueChanged()
    {
        print(inputLoanState);
        if (inputLoanState)
        {
            if (int.TryParse(sumInputField.text, out var amount))
            {
                if (amount > LOAN_LIMIT - CalculateSumLoans())
                    sumInputField.text = (LOAN_LIMIT - CalculateSumLoans()).ToString();
            }
            else sumInputField.text = "0";
            float sum = float.Parse(sumInputField.text) / (LOAN_LIMIT - CalculateSumLoans());
            sumScrollbar.value = sum;
            sumAmountPanel = amount;
            CalculateMonthlyPaymentPanel();
        }
    }

    /// <summary> Событие изменения поля долга.</summary>
    public void OnDebtInputFieldChanged()
    {
        debt = CalculateDebt();

        if (debt > debtLimit)
            debt = debtLimit;
        if (inputDebtState)
            if (int.TryParse(debtInputField.text, out var amount))
            {
                print(amount);
                if (amount > debt)
                {
                    debtInputField.text = debt.ToString();
                    amount = debt;
                }
                currDebt = amount;
            }
    }
    /// <summary> Просчитать ежемесяный платеж.</summary>
    private void CalculateMonthlyPaymentPanel()
    {
        payment = ((int)Math.Ceiling((float)(sumAmountPanel / monthAmountPanel) + sumAmountPanel * percent));
        paymentAmountText.text = payment.ToString();
    }

    /// <summary> Событие нажатия кнопки взятия кредита.</summary>
    public void OnTakeLoanButtonClick()
    {
        LoanList.Add(new Loan(sumAmountPanel, monthAmountPanel));

        debtAmountText.text = CalculateDebt().ToString();
        print(CalculateDebt());
        CheckDates();
        Player.Instance.Money.AddMoney(sumAmountPanel);
        print(Player.Instance.Money.Value);
        repayButtons.SetActive(true);
    }

    /// <summary> Событие выплаты кредита.</summary>
    public void OnRepayDebtsButtonClick()
    {
        currDebt = CalculateDebt();
        if (Player.Instance.Money.Value > currDebt)
        {
            OverdraftList.Clear();
            LoanList.Clear();
            debtAmountText.text = currDebt.ToString();
            CheckDates();
            repayButtons.SetActive(false);
            noooDebts.SetActive(true);
            Player.Instance.Money.RemoveMoney(currDebt);
        }
        else
        {
            for (int i = 0; i < OverdraftList.Count; i++)
            {
                if (OverdraftList[i].Amount <= currDebt)
                {
                    currDebt -= OverdraftList[i].Amount;
                    Player.Instance.Money.RemoveMoney(OverdraftList[i].Amount);
                    OverdraftList.RemoveAt(i);
                }
                else
                {
                    OverdraftList[i].Amount -= currDebt;
                    Player.Instance.Money.RemoveMoney(currDebt);
                    return;
                }
            }
            for (int i = 0; i < LoanList.Count; i++)
            {
                if (LoanList[i].Amount <= currDebt)
                {
                    currDebt -= LoanList[i].Amount;
                    Player.Instance.Money.RemoveMoney(LoanList[i].Amount);
                    LoanList.RemoveAt(i);
                }
                else
                {
                    LoanList[i].Amount -= currDebt;
                    Player.Instance.Money.RemoveMoney(currDebt);
                    return;
                }
            }
        }
    }
}
