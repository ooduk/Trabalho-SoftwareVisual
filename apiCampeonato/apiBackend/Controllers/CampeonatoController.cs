using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/campeonato")]

    public class CampeonatoController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public CampeonatoController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] Campeonato campeonato)
        {
            try
            {
                _ctx.Campeonatos.Add(campeonato);
                _ctx.SaveChanges();
                return Created("", campeonato);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {

            List<Campeonato> campeonatos = _ctx.Campeonatos.ToList();
            return campeonatos.Count == 0 ? NotFound() : Ok(campeonatos);

        }

        [HttpGet, Route("consultar/{id}")]

        public IActionResult Consultar([FromRoute] int id)
        {

            try
            {
                foreach (Campeonato CampeonatoCadastrado in _ctx.Campeonatos.ToList())
                {
                    if (CampeonatoCadastrado.CampeonatoId == id)
                    {
                        return Ok(CampeonatoCadastrado);
                    }
                }

                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut, Route("atualizar/{id}")]

        public IActionResult Atualizar([FromBody] Campeonato campeonato, [FromRoute] int id)
        {
            try
            {

                Campeonato? campeonatoEncontrado = _ctx.Campeonatos.FirstOrDefault(x => x.CampeonatoId == id);

                if (campeonatoEncontrado != null)
                {
                    campeonatoEncontrado.Nome = campeonato.Nome;
                    campeonatoEncontrado.Premiacao = campeonato.Premiacao;
                    _ctx.Campeonatos.Update(campeonatoEncontrado);
                    _ctx.SaveChanges();
                    return Ok(campeonato);
                }

                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}