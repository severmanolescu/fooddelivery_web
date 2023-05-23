using System.Collections.Generic;

public class RestaurantDetails
{
    public string city;
    public string name;
}

public class Order
{
    public string city;
    public string name;
    public List<Data> orders;
}

public class Data
{
    public int index;
    public string address;
    public string date;
    public string name;
    public string username;
    public string phone;
    public string deliveryStatus;
    public string price;
}

public class Item
{
    public int amount;
    public string name;
}

public class OrderDetails
{
    public Data data;
    public string name;
    public string city;
}

public class Restaurant
{
    public string discount;
    public string name;
    public string city;
    public List<Food> products;
    public List<Data> orders;
}

public class Food
{
    public string name;
    public string price;
    public string image;
}