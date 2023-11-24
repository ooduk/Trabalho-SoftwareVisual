namespace apiBackend.Controllers
{
    using ApiBackend.Data;
    using ApiBackend.Models;
    using Microsoft.AspNetCore.Mvc;
    using ApiBackend.DTOs;
    using Microsoft.EntityFrameworkCore;

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
                _ctx.Times.Add(time);
                _ctx.SaveChanges();

                return Created("", time);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {
            try
            {
                List<Time> times = _ctx.Times.ToList();

                if (times.Count() != 0)
                {
                    return Ok(times);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("consultar/{id}")]
        public IActionResult Consultar([FromRoute] int id)
        {
            try
            {
                Time? TimeCadastrado = _ctx.Times.FirstOrDefault(t => t.TimeId == id);

                if (TimeCadastrado != null)
                {
                    return Ok(TimeCadastrado);
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

                    _ctx.Times.Update(TimeEncontrado);
                    _ctx.SaveChanges();

                    return Ok(TimeEncontrado);
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

                    var times = _ctx.Times.ToList();

                    return Ok(times);
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

                    if (confrontos.Count() != 0)
                    {
                        string retorno = "Histórico de Confronto(s):\n\n";

                        foreach (var confrontoReturn in confrontos)
                        {
                            retorno += $"Campeonato: {confrontoReturn.CampeonatoNome}\nResultado: {confrontoReturn.TimeCasaNome} {confrontoReturn.Gols_time_casa} x {confrontoReturn.Gols_time_fora} {confrontoReturn.TimeForaNome}\n\n";
                        }
                        return Ok(confrontos);
                    }
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