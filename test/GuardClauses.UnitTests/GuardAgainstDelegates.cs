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
        Assert.Equal<int>(6,list.Length);
    }

    [Fact]
    public void ThrowsWhenDelegateIsNull()
    {
        Action? Chain = null;
        Assert.Throws<ArgumentNullException>(() => Guard.Against.AgainstChains(Chain));
    }

    [Fact]
    public void GetFirstChain()
    {
        Func<int> Chain = delegate { return 0; };
        Chain += delegate { return 1; };
        Chain += delegate { return 2; };
        Chain += delegate { return 3; };
        Chain += delegate { return 4; };
        Chain += delegate { return 5; };
    
        var chain = Guard.Against.AgainstChainSelection(Chain, 0);
        var chain3 = Guard.Against.AgainstChainSelection(Chain, 2);
        int result = (int)chain.DynamicInvoke();
        int result3 = (int)chain.DynamicInvoke();
    
        Assert.Equal<int>(0,result);
        Assert.Equal<int>(2,result3);
    }
}
