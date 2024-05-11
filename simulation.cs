using System.Collections.Generic;
using UnityEngine;

// Интерфейс для стратегий адаптации
public interface IAdaptationStrategy
{
    void Adapt(Market market);
}

// Стратегия адаптации: Увеличение цены при высоком спросе
public class IncreasePriceOnHighDemandStrategy : IAdaptationStrategy
{
    public void Adapt(Market market)
    {
        if (market.Demand > 100f)
        {
            market.Price *= 1.1f;
        }
    }
}

// Стратегия адаптации: Уменьшение цены при высоком предложении
public class DecreasePriceOnHighSupplyStrategy : IAdaptationStrategy
{
    public void Adapt(Market market)
    {
        if (market.Supply > 100f)
        {
            market.Price *= 0.9f;
        }
    }
}

// Класс для представления рынка
public class Market
{
    public float Price { get; set; }
    public float Demand { get; set; }
    public float Supply { get; set; }
}

// Движок рынка
public class MarketEngine
{
    private Market market;
    private List<IAdaptationStrategy> strategies;

    public MarketEngine()
    {
        Initialize();
    }

    public void Initialize()
    {
        market = new Market();
        strategies = new List<IAdaptationStrategy>();
    }

    public void AddStrategy(IAdaptationStrategy strategy)
    {
        strategies.Add(strategy);
    }

    public void RemoveStrategy(IAdaptationStrategy strategy)
    {
        strategies.Remove(strategy);
    }

    public void UpdateMarket()
    {
        foreach (var strategy in strategies)
        {
            strategy.Adapt(market);
        }
    }
}

// Анализатор данных
public class DataAnalyzer
{
    public void AnalyzeData(Market market)
    {
        market.GetStats();
    }
}

// Пользовательский интерфейс
public class UserInterface : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Text demandText;
    [SerializeField] private Text supplyText;
    [SerializeField] private Button simulateButton;

    private MarketEngine marketEngine;
    private MarketSimulator marketSimulator;

    void Start()
    {
        marketEngine = new MarketEngine();
        marketSimulator = new MarketSimulator(marketEngine);

        // Добавляем стратегии адаптации
        marketEngine.AddStrategy(new IncreasePriceOnHighDemandStrategy());
        marketEngine.AddStrategy(new DecreasePriceOnHighSupplyStrategy());

        // Настраиваем начальные значения рынка
        marketEngine.market.Price = 100f;
        marketEngine.market.Demand = 50f;
        marketEngine.market.Supply = 50f;

        simulateButton.onClick.AddListener(() => StartCoroutine(SimulateMarket(10f)));
    }

    void Update()
    {
        DisplayMarketData();
    }

    public void DisplayMarketData()
    {
        priceText.text = "Price: " + marketEngine.market.Price;
        demandText.text = "Demand: " + marketEngine.market.Demand;
        supplyText.text = "Supply: " + marketEngine.market.Supply;
    }

    IEnumerator SimulateMarket(float simulationTime)
    {
        simulateButton.interactable = false;
        yield return marketSimulator.SimulateMarket(simulationTime);
        simulateButton.interactable = true;
    }
}
// Симулятор рынка
public class MarketSimulator
{
    private MarketEngine marketEngine;

    public MarketSimulator(MarketEngine marketEngine)
    {
        this.marketEngine = marketEngine;
    }

    public IEnumerator SimulateMarket(float simulationTime)
    {
        float timer = 0f;

        while (timer < simulationTime)
        {
            marketEngine.UpdateMarket();
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
