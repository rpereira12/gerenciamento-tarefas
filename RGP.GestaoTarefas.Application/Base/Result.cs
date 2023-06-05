public class Result
{
    public Result(bool sucesso, object dados, string mensagem)
    {
        Sucesso = sucesso;
        Dados = dados;
        Mensagem = mensagem;
    }

    public Result(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }

    public Result(bool sucesso, object dados)
    {
        Sucesso = sucesso;
        Dados = dados;
    }

    public bool Sucesso { get; set; }
    public object Dados { get; set; }
    public string Mensagem { get; set; }
}
