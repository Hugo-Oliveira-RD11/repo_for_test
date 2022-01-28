namespace MyProject;

public class Cpf
{
    public bool ValidaCpf(string cpf)
    {
        if(String.IsNullOrWhiteSpace(cpf))
            return false;
        
        return true;
    }
}