using ApiBackend.Data;
using ApiBackend.DTOs;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/confronto")]

    public class ConfrontoController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public ConfrontoController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] ConfrontoRequest confrontoRequest)
        {

            try
            {
                Campeonato? campeonato = _ctx.Campeonatos.Find(confrontoRequest.CampeonatoId);

                Time? timeCasa = _ctx.Times.Find(confrontoRequest.TimeCasaId);

                Time? timeFora = _ctx.Times.Find(confrontoRequest.TimeForaId);

                if ((campeonato == null) || (timeCasa == null) || (timeFora == null))
                {
                    return NotFound();
                }

                Confronto confronto = new Confronto
                {
                    Campeonato = campeonato,
                    TimeCasa = timeCasa,
                    TimeFora = timeFora,
                    Gols_time_casa = confrontoRequest.Gols_time_casa,
                    Gols_time_fora = confrontoRequest.Gols_time_fora
                };

                ConfrontoReturn confrontoReturn = new ConfrontoReturn
                {
                    TimeCasaNome = confronto.TimeCasa.Nome,
                    TimeForaNome = confronto.TimeFora.Nome,
                    CampeonatoNome = confronto.Campeonato.Nome,
                    Gols_time_casa = confronto.Gols_time_casa,
                    Gols_time_fora = confronto.Gols_time_fora
                };

                string retorno = $"Confronto Cadastrado:\n\nCampeonato: {confrontoReturn.CampeonatoNome}\nResultado: {confrontoReturn.TimeCasaNome} {confrontoReturn.Gols_time_casa} x {confrontoReturn.Gols_time_fora} {confrontoReturn.TimeForaNome}";

                _ctx.Confrontos.Add(confronto);
                _ctx.SaveChanges();
                return Created("", retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {
            List<ConfrontoReturn> confrontos = _ctx.Confrontos
            .Include(c => c.Campeonato)
            .Include(c => c.TimeCasa)
            .Include(c => c.TimeFora)
            .Select(c => new ConfrontoReturn
            {
                TimeCasaNome = c.TimeCasa.Nome,
                TimeForaNome = c.TimeFora.Nome,
                CampeonatoNome = c.Campeonato.Nome,
                Gols_time_casa = c.Gols_time_casa,
                Gols_time_fora = c.Gols_time_fora
            })
            .ToList();

            string retorno = "Confronto(s) Listado(s):\n\n";

            if (confrontos.Count() != 0)
            {
                foreach (var confrontoReturn in confrontos)
                {
                    retorno += $"Campeonato: {confrontoReturn.CampeonatoNome}\nResultado: {confrontoReturn.TimeCasaNome} {confrontoReturn.Gols_time_casa} x {confrontoReturn.Gols_time_fora} {confrontoReturn.TimeForaNome}\n\n";
                }
                return Ok(retorno);
            }

            return NotFound();
        }

        [HttpGet, Route("consultar/{id}")]
        public IActionResult Consultar([FromRoute] int id)
        {
            try
            {
                foreach (ConfrontoReturn ConfrontoCadastrado in _ctx.Confrontos
                .Include(c => c.Campeonato)
                .Include(c => c.TimeCasa)
                .Include(c => c.TimeFora)
                .Select(c => new ConfrontoReturn
                {
                    ConfrontoId = c.ConfrontoId,
                    TimeCasaNome = c.TimeCasa.Nome,
                    TimeForaNome = c.TimeFora.Nome,
                    CampeonatoNome = c.Campeonato.Nome,
                    Gols_time_casa = c.Gols_time_casa,
                    Gols_time_fora = c.Gols_time_fora
                }))
                {
                    if (ConfrontoCadastrado.ConfrontoId == id)
                    {
                        string retorno = $"Confronto Consultado:\n\nCampeonato: {ConfrontoCadastrado.CampeonatoNome}\nResultado: {ConfrontoCadastrado.TimeCasaNome} {ConfrontoCadastrado.Gols_time_casa} x {ConfrontoCadastrado.Gols_time_fora} {ConfrontoCadastrado.TimeForaNome}";

                        return Ok(retorno);
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
        public IActionResult Atualizar([FromBody] ConfrontoRequest confrontoRequest, [FromRoute] int id)
        {
            try
            {

                Confronto? ConfrontoEncontrado = _ctx.Confrontos.FirstOrDefault(x => x.ConfrontoId == id);

                Campeonato? campeonato = _ctx.Campeonatos.Find(confrontoRequest.CampeonatoId);

                Time? timeCasa = _ctx.Times.Find(confrontoRequest.TimeCasaId);

                Time? timeFora = _ctx.Times.Find(confrontoRequest.TimeForaId);

                if ((campeonato == null) || (timeCasa == null) || (timeFora == null))
                {
                    return NotFound();
                }
                
                if (ConfrontoEncontrado != null)
                {
                    ConfrontoEncontrado.Campeonato = campeonato;
                    ConfrontoEncontrado.TimeCasa = timeCasa;
                    ConfrontoEncontrado.TimeFora = timeFora;
                    ConfrontoEncontrado.Gols_time_casa = confrontoRequest.Gols_time_casa;
                    ConfrontoEncontrado.Gols_time_fora = confrontoRequest.Gols_time_fora;

                    ConfrontoReturn confrontoReturn = new ConfrontoReturn
                    {
                        TimeCasaNome = ConfrontoEncontrado.TimeCasa.Nome,
                        TimeForaNome = ConfrontoEncontrado.TimeFora.Nome,
                        CampeonatoNome = ConfrontoEncontrado.Campeonato.Nome,
                        Gols_time_casa = ConfrontoEncontrado.Gols_time_casa,
                        Gols_time_fora = ConfrontoEncontrado.Gols_time_fora
                    };

                    string retorno = $"Confronto Atualizado:\n\nCampeonato: {confrontoReturn.CampeonatoNome}\nResultado: {confrontoReturn.TimeCasaNome} {confrontoReturn.Gols_time_casa} x {confrontoReturn.Gols_time_fora} {confrontoReturn.TimeForaNome}";

                    _ctx.Confrontos.Update(ConfrontoEncontrado);
                    _ctx.SaveChanges();
                    return Ok(retorno);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete, Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {

            try
            {
                Confronto? confronto = _ctx.Confrontos.Find(id);

                if (confronto != null)
                {
                    _ctx.Confrontos.Remove(confronto);
                    _ctx.SaveChanges();
                    return Ok("Confronto Deletado!");
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
