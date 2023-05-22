using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Test.Backports;

[TestFixture]
class FormattableStringTest
{
    [Test]
    public void TestType()
    {
        IFormattable f = $"Test double 1234.56 -> {1234.56:N2}";
        Assert.IsTrue(f is FormattableString formattableString);
        Assert.AreEqual("Test double 1234.56 -> 1,234.56", f.ToString(null, CultureInfo.InvariantCulture));
        Assert.AreEqual("Test double 1234.56 -> 1.234,56", f.ToString(null, new CultureInfo("de-DE")));
    }
}
