using Microsoft.AspNetCore.Mvc;
using src.Models;

using Microsoft.EntityFrameworkCore;
using src.Persistence;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase 
{

    private DatabaseContext _context { get; set; }

    public PessoaController(DatabaseContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public List<Pessoa> Get()
    {
        return _context.Pessoas.Include(p => p.Contratos).ToList();
    }

    [HttpPost]
    public Pessoa Post([FromBody] Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        _context.SaveChanges();

        return pessoa;
    }

    [HttpPut("{id}")]
    public string Update([FromRoute] int id, [FromBody] Pessoa pessoa) 
    {
        _context.Pessoas.Update(pessoa);
        _context.SaveChanges();
        
        return "Dados do id " + id + " atualizados";
    }

    [HttpDelete("{id}")]
    public string Delete([FromRoute] int id) {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

        if (result != null) {
            _context.Pessoas.Remove(result);
            _context.SaveChanges();
        } else {
            return "Pessoa com id " + id + " não encontrada";
        }

        return "Deletado pessoa de id " + id;
    }

}
