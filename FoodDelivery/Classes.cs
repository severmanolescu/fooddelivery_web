using System;
using System.Collections.Generic;

public class Order
{
    public string link;
    public string name;
    public List<Data> orders;
}

public class Data
{
    public int index;
    public string address;
    public DateTime date;
    public List<Item> items;
    public string person;
    public string phone;
    public string status;
    public int price;
}

public class Item
{
    public int amount;
    public string name;
}

public class OrderDetails
{
    public Data data;
    public string restaurantID;
}

public class Restaurant
{
    public string image;
    public string discount;
    public List<Food> food;
}

public class Food
{
    public string name;
    public string price;
    public string image;
}