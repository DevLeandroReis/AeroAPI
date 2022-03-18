using AeroAPI.Data;
using AeroAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroAPI.Controllers
{
    [ApiController]
    [Route("v1/api/Passageiros")]
    public class PassageiroController : ControllerBase
    {
        private readonly DataContext _context;

        public PassageiroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Passageiro>>> GetAsync()
        {
            try
            {
                var query = from passageiro in _context.Passageiros
                            select passageiro;

                var total = await query.AsNoTracking().CountAsync();
                var Passageiro = await query.AsNoTracking().ToListAsync();                          
                return Ok(new{
                    totaldata = total,
                    Passageiro
                });                                  
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet(template: "id/{id}")]
        public async Task<ActionResult<List<Passageiro>>> GetAsync(int id)
        {
            try
            {                
                var query = from passageiro in _context.Passageiros
                            where passageiro.Id == id
                            select passageiro;

                var total = await query.AsNoTracking().CountAsync();
                var Passageiro = await query.AsNoTracking().ToListAsync();
                if (total > 0)
                {
                    return Ok(new
                    {                     
                        totaldata = total,
                        Passageiro
                    });
                } else
                {
                    return NotFound(new{mensagem = "Passageiro não encontrado!"});
                }               
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }            
        }

        [HttpDelete(template: "id/{id}")]
        public async Task<ActionResult<string>> DeleteAsync(int id)
        {

            try
            {
                var query = from passageiros in _context.Passageiros
                            where passageiros.Id == id
                            select passageiros;
                var passageiro = query.AsQueryable().FirstOrDefault();
                if (passageiro != null)
                {
                    _context.Passageiros.Remove(passageiro);
                    await _context.SaveChangesAsync();
                    return Ok(new{mensagem = "Passageiro excluido com sucesso!"});                  
                }
                else
                {
                    return NotFound(new{mensagem = "Passageiro não encontrado!"});
                }
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost()]
        public async Task<ActionResult<string>> PostAsync([FromBody] Passageiro passageiro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Passageiros.Add(passageiro);
                await _context.SaveChangesAsync();

                return Ok(new{mensagem = "Passageiro cadastrado com sucesso!"});
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPut("id/{id}")]
        public async Task<ActionResult<string>> PutAsync([FromBody] Passageiro passageiro, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != passageiro.Id)
                {
                    return BadRequest("IDs incompatíveis");
                }
                var query = from passageiros in _context.Passageiros
                            where passageiros.Id == id
                            select passageiros;
                var passageiroToUpdate = query.AsQueryable().FirstOrDefault();

                if (passageiroToUpdate == null)
                {
                    return NotFound($"Não encontrado o passageiro com a Id = {id}");
                }
                else
                {
                    passageiroToUpdate.Nome = passageiro.Nome;
                    passageiroToUpdate.Idade = passageiro.Idade;
                    passageiroToUpdate.Celular = passageiro.Celular;
                    await _context.SaveChangesAsync();
                    return Ok(new { mensagem = "Passageiro alterado com sucesso!" });
                }
            }
            catch (System.Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        private bool PassageiroExists(int id)
        {
            return _context.Passageiros.Any(e => e.Id == id);
        }
    }
}
