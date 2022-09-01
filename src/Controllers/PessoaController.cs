using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase 
{

    [HttpGet]
    public Pessoa Get()
    {
        Pessoa pessoa = new Pessoa("Rudolf", 42, "12345678901");
        Contrato novoContrato = new Contrato("abc123", 50.67);
        
        pessoa.Contratos.Add(novoContrato);

        return pessoa;
    }

    [HttpPost]
    public Pessoa Post([FromBody] Pessoa pessoa)
    {
        return pessoa;
    }

    [HttpPut("{id}")]
    public string Update([FromRoute] int id, [FromBody] Pessoa pessoa) 
    {
        Console.WriteLine("Dados do id " + id + " atualizados");
        Console.WriteLine(pessoa);
        return "Dados do id " + id + " atualizados";
    }

    [HttpDelete("{id}")]
    public string Delete([FromRoute] int id) {
        return "Deletado pessoa de id " + id;
    }

}
