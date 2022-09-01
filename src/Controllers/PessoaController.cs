using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

using src.Models;
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
    public ActionResult<List<Pessoa>> Get()
    {
        var result = _context.Pessoas.Include(p => p.Contratos).ToList();

        if (!result.Any())
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
    {
        try
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            return BadRequest(new
            {
                message = "Solicitação inválida ao tentar cadastrar pessoa",
                status = HttpStatusCode.BadRequest
            });
        }

        return Created("/Pessoa/{id}", pessoa);
    }

    [HttpPut("{id}")]
    public ActionResult<Object> Update
    (
        [FromRoute] int id, 
        [FromBody] Pessoa pessoa
    )
    {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

        if (result is null)
        {
            return NotFound(new 
            {
                message = "Não encontrada pessoa de ID " + id,
                status = HttpStatusCode.NotFound
            });
        }

        try
        {
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            return BadRequest(new 
            {
                message = "Solicitação inválida ao tentar atualizar " +
                    "pessoa de id " + id,
                status = HttpStatusCode.BadRequest
            });
        }

        
        return Ok(new 
        {
            message = $"Dados da pessoa de ID {id} atualizados",
            status = HttpStatusCode.OK
        });
    }

    [HttpDelete("{id}")]
    public ActionResult<Object> Delete([FromRoute] int id) {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

        if (result is null)
        {
            return BadRequest(new 
            {
                message = "Conteúdo inexistente, solicitação inválida",
                status = HttpStatusCode.BadRequest
            });
        }
        
        _context.Pessoas.Remove(result);
        _context.SaveChanges();
        
        return Ok(new 
        { 
            message = "Deletado pessoa de ID " + id,
            status = HttpStatusCode.OK
        });
    }

}
