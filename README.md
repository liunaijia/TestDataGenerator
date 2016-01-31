# Test Data Generator
> A library that generates test data in database.

## Features
* Fast inserting data
Test Data Generator uses bulk copy to insert a large number of data rows into database.
* Generate data on the basis of Regular Expression
Regular Expressions are usually used to validate input data format. By contrast, Test Data Generator uses Regular Expressions to define the data format rules then generates data that match these rules.
Let's say if I want to generate meaningful email addresses, I could achieve it by defining a regular expression `[a-z]{3,8}@(gmail|yahoo|outlook)\.com`. Comparing with writing a piece of code to generate an meaningful email address, using regular expression can save your time for coding.
