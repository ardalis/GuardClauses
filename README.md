![nuget](https://img.shields.io/nuget/dt/Ardalis.GuardClauses.svg)

Nuget: [Ardalis.GuardClauses](https://www.nuget.org/packages/Ardalis.GuardClauses)

# Guard Clauses
A simple package with guard clause extensions.

## Usage

```c#
    public void ProcessOrder(Order order)
    {
        Guard.AgainstNull(order, nameof(order));

        // process order here
    }
```

## Supported Guard Clauses

- **AgainstNull** (throws if input is null)
- **AgainstNullOrEmpty** (throws if string input is null or empty)

## References

- [Guard Clauses (podcast: 7 minutes)](http://www.weeklydevtips.com/004)
- [Guard Clause](http://deviq.com/guard-clause/)
