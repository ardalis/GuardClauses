<h1 align=center>
<img src="media/logotype 1024.svg" width=50%>
</h1>

[![NuGet](https://img.shields.io/nuget/v/Ardalis.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.GuardClauses)[![NuGet](https://img.shields.io/nuget/dt/Ardalis.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.GuardClauses)
![publish Ardalis.GuardClauses to nuget](https://github.com/ardalis/GuardClauses/workflows/publish%20Ardalis.GuardClauses%20to%20nuget/badge.svg)

# Guard Clauses
A simple package with guard clause extensions.

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

## Usage

```c#
    public void ProcessOrder(Order order)
    {
    	Guard.Against.Null(order, nameof(order));

        // process order here
    }

    // OR

    public class Order
    {
        private string _name;
        private int _quantity;
        private long _max;
        private decimal _unitPrice;
        private DateTime _dateCreated;

        public Order(string name, int quantity, long max, decimal unitPrice, DateTime dateCreated)
        {
            _name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            _quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            _max = Guard.Against.Zero(max, nameof(max));
            _unitPrice = Guard.Against.Negative(unitPrice, nameof(unitPrice));
            _dateCreated = Guard.Against.OutOfSQLDateRange(dateCreated, nameof(dateCreated));
        }
    }
```

## Supported Guard Clauses

- **Guard.Against.Null** (throws if input is null)
- **Guard.Against.NullOrEmpty** (throws if string, guid or array input is null or empty)
- **Guard.Against.NullOrWhiteSpace** (throws if string input is null, empty or whitespace)
- **Guard.Against.OutOfRange** (throws if integer/DateTime/enum input is outside a provided range)
- **Guard.Against.OutOfSQLDateRange** (throws if DateTime input is outside the valid range of SQL Server DateTime values)
- **Guard.Against.Zero** (throws if number input is zero)

## Extending with your own Guard Clauses

To extend your own guards, you can do the following:

```c#
    // Using the same namespace will make sure your code picks up your 
    // extensions no matter where they are in your codebase.
    namespace Ardalis.GuardClauses
    {
        public static class FooGuard
        {
            public static void Foo(this IGuardClause guardClause, string input, string parameterName)
            {
                if (input?.ToLower() == "foo")
                    throw new ArgumentException("Should not have been foo!", parameterName);
            }
        }
    }

    // Usage
    public void SomeMethod(string something)
    {
        Guard.Against.Foo(something, nameof(something));
    }
```

## References

- [How to write clean validation clauses in .NET](https://www.youtube.com/watch?v=Tvx6DNarqDM) (Nick Chapas, YouTube, 9 minutes)
- [Guard Clauses (podcast: 7 minutes)](http://www.weeklydevtips.com/004)
- [Guard Clause](http://deviq.com/guard-clause/)

## Build Notes

- Remember to update the PackageVersion in the csproj file and then a build on master should automatically publish the new package to nuget.org.
- Add a release with form `1.3.2` to GitHub Releases in order for the package to actually be published to Nuget. Otherwise it will claim to have been successful but is lying to you.


