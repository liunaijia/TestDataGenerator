# Test Data Generator
> A library that generates test data in database.

## Features
##### Fast inserting data
Test Data Generator uses bulk copy to insert a large number of data rows into database.

##### Generate data on the basis of Regular Expressions

Regular Expressions are usually used to validate input data format. By contrast, Test Data Generator uses Regular Expressions to define the data format rules then generates data that match these rules.

Let's say if I want to generate meaningful email addresses, I could achieve it by defining a regular expression `[a-z]{3,8}@(gmail|yahoo|outlook)\.com`. Comparing with writing a piece of code to generate an meaningful email address, using regular expression can save your time for coding.

##### Fetch data from other tables
Another general requirement when creating test data is that columns can have the foreign key constraint and these columns' value come from the data in other tables. This library helps you to handle this requirement.

## Quick Start Guide

##### Step1 Configurate connection string
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="order" connectionString="Data Source=localhost;Initial Catalog=order;Integrated Security=True" providerName="System.Data.SqlClient"/>
  </connectionStrings>
</configuration>
```

##### Step2 Use Test Data Generator API to generate data in database
```c#
using TestDataGenerator;
using TestDataGenerator.Data;
using TestDataGenerator.Generators;

var orderDatabase = DatabaseFactory.CreateDatabase("order");

Func<object> orderBuilder = () => new {
    OrderId = Guid.NewGuid(),
    Year = DataGenerator.From(2014, 2015, 2016).Random(),
    CustomerId = DataGenerator.From(customerDatabase, "select customer_id from customers").Random(),
    CustomerEmail = DataGenerator.FromRegex(@"[a-z]{3,10}@(gmail|yahoo|outlook)\.com").Random(),
    CreatedBy = DataGenerator.From(orderDatabase, "select id from staff").Random(),
    CreatedAt = DataGenerator.FromDateRangeInDeltaDays(-30).Random()
};

orderBuilder.Build(100000).InTable(orderDatabase, "orders");
```
This piece of code will create 100000 rows in orders table.
