<h1 align=center>
<img src="media/logotype 1024.svg" width=50%>
</h1>

[![NuGet](https://img.shields.io/nuget/dt/Ardalis.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.GuardClauses)
[![Build Status](https://dev.azure.com/ardalis/GuardClauses/_apis/build/status/ardalis.GuardClauses?branchName=master)](https://dev.azure.com/ardalis/GuardClauses/_build/latest?definitionId=1&branchName=master)

NuGet: [Ardalis.GuardClauses](https://www.nuget.org/packages/Ardalis.GuardClauses)

# Guard Clauses
A simple package with guard clause extensions.

## Give a Star! :star:
If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Usage

```c#
    public void ProcessOrder(Order order)
    {
    	Guard.Against.Null(order, nameof(order));

        // process order here
    }
```

## Supported Guard Clauses

- **Guard.Against.Null** (throws if input is null)
- **Guard.Against.NullOrEmpty** (throws if string or array input is null or empty)
- **Guard.Against.NullOrWhiteSpace** (throws if string input is null, empty or whitespace)
- **Guard.Against.OutOfRange** (throws if integer/DateTime input is outside a provided range)
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

- [Guard Clauses (podcast: 7 minutes)](http://www.weeklydevtips.com/004)
- [Guard Clause](http://deviq.com/guard-clause/)
