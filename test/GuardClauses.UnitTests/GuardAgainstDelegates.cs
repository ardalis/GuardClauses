using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;
public class GuardAgainstDelegates
{

    [Fact]
    public void GetChainsOfDelegate()
    {
        // Assert
        Action Chain = Console.WriteLine;
        Chain += delegate {  };
        Chain += delegate {  };
        Chain += delegate {  };
        Chain += delegate {  };
        Chain += delegate {  };
        var list = Guard.Against.AgainstChains(Chain);

        Assert.NotNull(list);
        Assert.True(list.Length == 6);
    }

    [Fact]
    public void ThrowsWhenDelegateIsNull()
    {
        Action? Chain = null;
        Assert.Throws<ArgumentNullException>(() => Guard.Against.AgainstChains(Chain));
    }
}
