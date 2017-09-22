[![nuget](https://img.shields.io/nuget/dt/Ardalis.GuardClauses.svg)](https://www.nuget.org/packages/Ardalis.GuardClauses)

Nuget: [Ardalis.GuardClauses](https://www.nuget.org/packages/Ardalis.GuardClauses)

# Guard Clauses
A simple package with guard clause extensions.

## Usage

```c#
    public void ProcessOrder(Order order)
    {
        // shortcut
        Guard.AgainstNull(order, nameof(order));
	
	// equivalent
	Guard.Against.Null(order, nameof(order));

        // process order here
    }
```

## Supported Guard Clauses

- **AgainstNull** (throws if input is null) **Guard.Against.Null** is equivalent.
- **AgainstNullOrEmpty** (throws if string input is null or empty) **Guard.Against.NullOrEmpty** is equivalent.

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
