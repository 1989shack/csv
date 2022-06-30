namespace Fmbm.Text.Tests;

public class CellTests
{
    [Fact]
    public void RoundTripReturnsSame()
    {
        var s = " 123 ";
        Assert.Equal(s, new Cell(s));

        var dt = DateTime.Parse("2022-06-27T16:04Z");
        Assert.Equal<DateTime>(dt, new Cell(dt));

        int i = 123;
        Assert.Equal<int>(i, new Cell(i));

        uint ui = 123;
        Assert.Equal<uint>(ui, new Cell(ui));

        long l = 123;
        Assert.Equal<long>(l, new Cell(l));

        ulong ul = 123;
        Assert.Equal<ulong>(ul, new Cell(ul));

        float f = 1234.5678F;
        Assert.Equal<float>(f, new Cell(f));

        double d = 1234.5678;
        Assert.Equal<double>(d, new Cell(d));

        decimal m = 1234.5678m;
        Assert.Equal<decimal>(m, new Cell(m));
    }


    [Fact]
    public void NastyString()
    {
        // more comprehenisve tests in CsvParserTests
        var text = "\r \" \",\" ,, \"\"\r\"";
        Assert.Equal(text, new Cell(text));
    }

    [Fact]
    public void SortableDateTime()
    {
        var date = DateTime.Parse("2002-03-04T05:06Z");
        Assert.Equal<DateTime>(date, new Cell("2002-03-04 5:06"));
    }

    [Fact]
    public void SortableDate()
    {
        var date = DateTime.Parse("2002-03-04");
        Assert.Equal<DateTime>(date, new Cell("2002-03-04"));
    }

    [Fact]
    public void DaftAmericanStyleDate()
    {
        var date = DateTime.Parse("2002-03-04");
        Assert.Equal<DateTime>(date, new Cell("03/04/02"));
    }

    [Fact]
    public void Integers()
    {
        int i = 1234;
        Assert.Equal<int>(i, new Cell("1234"));
        Assert.Equal<int>(i, new Cell("001234"));
        Assert.Equal<int>(i, new Cell("1,234"));
        Assert.Equal<int>(i, new Cell(" 1,234 "));
        Assert.Equal<int>(i, new Cell(" 1234 "));
        Assert.Equal<int>(-i, new Cell(" -1,234 "));

        uint u = 1234u;
        Assert.Equal<uint>(u, new Cell("1234"));
        Assert.Equal<uint>(u, new Cell("001234"));
        Assert.Equal<uint>(u, new Cell("1,234"));
        Assert.Equal<uint>(u, new Cell(" 1,234 "));
        Assert.Equal<uint>(u, new Cell(" 1234 "));
        Action uNeg = () => { var _ = (uint)new Cell(" -1,234 "); };
        Assert.Throws<OverflowException>(uNeg);

        long l = 1234;
        Assert.Equal<long>(l, new Cell("1234"));
        Assert.Equal<long>(l, new Cell("001234"));
        Assert.Equal<long>(l, new Cell("1,234"));
        Assert.Equal<long>(l, new Cell(" 1,234 "));
        Assert.Equal<long>(l, new Cell(" 1234 "));
        Assert.Equal<long>(-l, new Cell(" -1,234 "));

        ulong ul = 1234;
        Assert.Equal<ulong>(ul, new Cell("1234"));
        Assert.Equal<ulong>(ul, new Cell("001234"));
        Assert.Equal<ulong>(ul, new Cell("1,234"));
        Assert.Equal<ulong>(ul, new Cell(" 1,234 "));
        Assert.Equal<ulong>(ul, new Cell(" 1234 "));
        Action ulNeg = () => { var _ = (ulong)new Cell(" -1,234 "); };
        Assert.Throws<OverflowException>(ulNeg);
    }

    [Fact]
    public void NonInteger()
    {
        float f = 1234.567f;
        Assert.Equal<float>(f, new Cell("1234.567"));
        Assert.Equal<float>(f, new Cell("001234.56700"));
        Assert.Equal<float>(f, new Cell("1,234.567"));
        Assert.Equal<float>(f, new Cell(" 1,234.567 "));
        Assert.Equal<float>(f, new Cell(" 1234.567 "));
        Assert.Equal<float>(-f, new Cell(" -1,234.567 "));
        Assert.Equal<float>(-f, new Cell(" -01.2345670E+3 "));

        double d = 1234.567;
        Assert.Equal<double>(d, new Cell("1234.567"));
        Assert.Equal<double>(d, new Cell("001234.56700"));
        Assert.Equal<double>(d, new Cell("1,234.567"));
        Assert.Equal<double>(d, new Cell(" 1,234.567 "));
        Assert.Equal<double>(d, new Cell(" 1234.567 "));
        Assert.Equal<double>(-d, new Cell(" -1,234.567 "));
        Assert.Equal<double>(-d, new Cell(" -01.2345670E+3 "));

        decimal m = 1234.567m;
        Assert.Equal<decimal>(m, new Cell("1234.567"));
        Assert.Equal<decimal>(m, new Cell("001234.56700"));
        Assert.Equal<decimal>(m, new Cell("1,234.567"));
        Assert.Equal<decimal>(m, new Cell(" 1,234.567 "));
        Assert.Equal<decimal>(m, new Cell(" 1234.567 "));
        Assert.Equal<decimal>(-m, new Cell(" -1,234.567 "));
        Assert.Equal<decimal>(-m, new Cell(" -01.2345670E+3 "));
    }

    // [Fact]
    // public void ExplicitCastsToCell()
    // {
    //     Assert.Equal("Quick brown fox", ((Cell)"Quick brown fox").ToString());
    //     Assert.Equal("2022-06-29 10:34",
    //         ((Cell)DateTime.Parse("2022-06-29T10:34:30")).ToString());

    //     Assert.Equal<int>(new Cell(1234), (Cell)1234);
    //     Assert.Equal<uint>(new Cell(1234u), (Cell)1234u);
    //     Assert.Equal<long>(new Cell(1234L), (Cell)1234L);
    //     Assert.Equal<ulong>(new Cell(1234), (Cell)1234ul);

    //     Assert.Equal<float>(new Cell(1234.567f), (Cell)1234.567f);
    //     Assert.Equal<double>(new Cell(1234.567), (Cell)1234.567);
    //     Assert.Equal<decimal>(new Cell(1234.567m), (Cell)1234.567m);
    // }

    [Fact]
    public void FromAny_UnknownType_ToString()
    {
        byte b = 255;
        Assert.Equal(b, byte.Parse(new Cell(b)));
    }
}
