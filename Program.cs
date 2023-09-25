using System;
using System.Collections.Generic;

// Receiver
class FruitStock
{
    private Dictionary<string, int> stock = new Dictionary<string, int>();

    public void AddFruit(string fruit, int quantity)
    {
        if (stock.ContainsKey(fruit))
            stock[fruit] += quantity;
        else
            stock[fruit] = quantity;
        Console.WriteLine($"Added {quantity} {fruit}(s) to the stock.");
    }

    public void SellFruit(string fruit, int quantity)
    {
        if (stock.ContainsKey(fruit) && stock[fruit] >= quantity)
        {
            stock[fruit] -= quantity;
            Console.WriteLine($"Sold {quantity} {fruit}(s).");
        }
        else
        {
            Console.WriteLine($"Sorry, we don't have enough {fruit} in stock.");
        }
    }

    public void DisplayStock()
    {
        Console.WriteLine("Current Stock:");
        foreach (var item in stock)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}

// Command interface
interface ICommand
{
    void Execute();
}

// Concrete Command
class BuyFruitCommand : ICommand
{
    private FruitStock stock;
    private string fruit;
    private int quantity;

    public BuyFruitCommand(FruitStock stock, string fruit, int quantity)
    {
        this.stock = stock;
        this.fruit = fruit;
        this.quantity = quantity;
    }

    public void Execute()
    {
        stock.AddFruit(fruit, quantity);
    }
}

// Concrete Command
class SellFruitCommand : ICommand
{
    private FruitStock stock;
    private string fruit;
    private int quantity;

    public SellFruitCommand(FruitStock stock, string fruit, int quantity)
    {
        this.stock = stock;
        this.fruit = fruit;
        this.quantity = quantity;
    }

    public void Execute()
    {
        stock.SellFruit(fruit, quantity);
    }
}

// Invoker
class FruitShop
{
    private List<ICommand> commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (var command in commands)
        {
            command.Execute();
        }
        commands.Clear();
    }
}

class Program
{
    static void Main()
    {
        FruitStock stock = new FruitStock();
        FruitShop shop = new FruitShop();

        // Create and execute commands
        ICommand buyApples = new BuyFruitCommand(stock, "Apples", 10);
        ICommand buyBananas = new BuyFruitCommand(stock, "Bananas", 5);
        ICommand sellApples = new SellFruitCommand(stock, "Apples", 3);

        shop.AddCommand(buyApples);
        shop.AddCommand(buyBananas);
        shop.AddCommand(sellApples);

        // Execute the commands
        shop.ExecuteCommands();

        // Display stock
        stock.DisplayStock();
    }
}
