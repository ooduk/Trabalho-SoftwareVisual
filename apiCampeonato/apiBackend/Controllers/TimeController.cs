using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/time")]

    public class TimeController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public TimeController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] Time time)
        {
            try
            {
                string retorno = $"Time Cadastrado:\n\nNome: {time.Nome}";
                _ctx.Times.Add(time);
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
            List<Time> times = _ctx.Times.ToList();

            string retorno = $"Time(s) Listado(s):\n\n";

            if (times.Count() != 0)
            {
                foreach (var time in times)
                {
                    retorno += $"Nome: {time.Nome}\n\n";
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
                foreach (Time TimeCadastrado in _ctx.Times.ToList())
                {
                    if (TimeCadastrado.TimeId == id)
                    {
                        string retorno = $"Time Consultado:\n\nNome: {TimeCadastrado.Nome}";
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
        public IActionResult Atualizar([FromBody] Time time, [FromRoute] int id)
        {
            try
            {

                Time? TimeEncontrado = _ctx.Times.FirstOrDefault(x => x.TimeId == id);

                if (TimeEncontrado != null)
                {
                    TimeEncontrado.Nome = time.Nome;
                    string retorno = $"Time Atualizado:\n\nNome: {TimeEncontrado.Nome}";
                    _ctx.Times.Update(TimeEncontrado);
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
                Time? time = _ctx.Times.Find(id);

                if (time != null)
                {
                    _ctx.Times.Remove(time);
                    _ctx.SaveChanges();
                    return Ok("Time Deletado!");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("historico/{id}")]

        public IActionResult Historico([FromRoute] int id)
        {
            try
            {
                Time? time = _ctx.Times.Find(id);

                if (time != null)
                {

                    List<ConfrontoReturn> confrontos = _ctx.Confrontos
                    .Include(c => c.Campeonato)
                    .Include(c => c.TimeCasa)
                    .Include(c => c.TimeFora)
                    .Where(c => c.TimeForaId == id || c.TimeCasaId == id)
                    .Select(c => new ConfrontoReturn
                    {
                        TimeCasaNome = c.TimeCasa.Nome,
                        TimeForaNome = c.TimeFora.Nome,
                        CampeonatoNome = c.Campeonato.Nome,
                        Gols_time_casa = c.Gols_time_casa,
                        Gols_time_fora = c.Gols_time_fora
                    })
                    .Take(5)
                    .ToList();


                    string retorno = "Histórico de Confronto(s):\n\n";

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

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}