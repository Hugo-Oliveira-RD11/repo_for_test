using Xunit;
using MyProject;

namespace MyProjectTest;

public class UnitTest1
{
    [Fact]
    public void TestvalidaCpf_ReturnTrue()
    {
        //arrange
        var cpf = "999.999.999-20";
        //var expected = true;
        var Validacpf = new Cpf();

        //act
        var resultado = Validacpf.ValidaCpf(cpf);

        //assert
        Assert.True(resultado);
    }

    [Fact]
    public void TestvalidaCpf_ReturnFalse()
    {
        //arrange
        var cpf = "";
        var expected = false;
        var Validacpf = new Cpf();

        //act 
        var resultado = Validacpf.ValidaCpf(cpf);

        //assert
        Assert.Equal(expected, resultado);
    }
}