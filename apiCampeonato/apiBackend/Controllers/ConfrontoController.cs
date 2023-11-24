namespace apiBackend.Controllers
{
    using ApiBackend.Data;
    using ApiBackend.DTOs;
    using ApiBackend.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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

                if (Resultado(confrontoRequest.TimeCasaId, confrontoRequest.TimeForaId, confrontoRequest.CampeonatoId, confrontoRequest.Gols_time_casa, confrontoRequest.Gols_time_fora))
                {
                    _ctx.Confrontos.Add(confronto);
                    _ctx.SaveChanges();

                    string retorno = $"Confronto Cadastrado:\n\nCampeonato: {confrontoReturn.CampeonatoNome}\nResultado: {confrontoReturn.TimeCasaNome} {confrontoReturn.Gols_time_casa} x {confrontoReturn.Gols_time_fora} {confrontoReturn.TimeForaNome}";
                    return Created("", confrontoReturn);
                }
                
                return BadRequest();
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

                if (confrontos.Count() != 0)
                {
                    string retorno = "Confronto(s) Listado(s):\n\n";

                    foreach (var confrontoReturn in confrontos)
                    {
                        retorno += $"Campeonato: {confrontoReturn.CampeonatoNome}\nResultado: {confrontoReturn.TimeCasaNome} {confrontoReturn.Gols_time_casa} x {confrontoReturn.Gols_time_fora} {confrontoReturn.TimeForaNome}\n\n";
                    }
                    return Ok(confrontos);
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
                ConfrontoReturn? ConfrontoCadastrado = _ctx.Confrontos
                .Where(t => t.ConfrontoId == id)
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
                }).FirstOrDefault();

                if (ConfrontoCadastrado != null)
                {
                    string retorno = $"Confronto Consultado:\n\nCampeonato: {ConfrontoCadastrado.CampeonatoNome}\nResultado: {ConfrontoCadastrado.TimeCasaNome} {ConfrontoCadastrado.Gols_time_casa} x {ConfrontoCadastrado.Gols_time_fora} {ConfrontoCadastrado.TimeForaNome}";
                    return Ok(ConfrontoCadastrado);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public bool Resultado(int idTimeCasa, int idTimeFora, int idCampeonato, int golsCasa, int golsFora)
        {
            try
            {
                if (golsCasa > golsFora)
                {
                    List<Tabela> timesVenceu = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato)
                    .Where(t => t.TimeId == idTimeCasa)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .ToList();

                    List<Tabela> timesPerdeu = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato)
                    .Where(t => t.TimeId == idTimeFora)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .ToList();

                    if ((timesVenceu.Count != 1) || (timesPerdeu.Count != 1))
                    {
                        return false;
                    }

                    Vencer(timesVenceu, golsCasa, golsFora);
                    Perder(timesPerdeu, golsFora, golsCasa);

                    return true;
                }
                else if (golsCasa < golsFora)
                {
                    List<Tabela> timesVenceu = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato)
                    .Where(t => t.TimeId == idTimeFora)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .ToList();

                    List<Tabela> timesPerdeu = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato)
                    .Where(t => t.TimeId == idTimeCasa)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .ToList();

                    if ((timesVenceu.Count != 1) || (timesPerdeu.Count != 1))
                    {
                        return false;
                    }

                    Vencer(timesVenceu, golsFora, golsCasa);
                    Perder(timesPerdeu, golsCasa, golsFora);

                    return true;
                }
                else
                {
                    List<Tabela> timesEmpatouCasa = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato)
                    .Where(t => t.TimeId == idTimeCasa)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .ToList();

                    List<Tabela> timesEmpatouFora = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato)
                    .Where(t => t.TimeId == idTimeFora)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .ToList();

                    if ((timesEmpatouCasa.Count != 1) || (timesEmpatouFora.Count != 1))
                    {
                        return false;
                    }

                    Empatar(timesEmpatouFora, golsFora, golsCasa);
                    Empatar(timesEmpatouCasa, golsCasa, golsFora);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Vencer(List<Tabela> times, int golsMarcados, int golsSofridos)
        {
            foreach (var time in times)
            {
                time.Vitorias += 1;
                time.Pontos += 3;
                time.Gols_marcados += golsMarcados;
                time.Gols_contra += golsSofridos;

                _ctx.Tabelas.Update(time);
                _ctx.SaveChanges();
            }
        }

        public void Perder(List<Tabela> times, int golsMarcados, int golsSofridos)
        {
            foreach (var time in times)
            {
                time.Derrotas += 1;
                time.Gols_marcados += golsMarcados;
                time.Gols_contra += golsSofridos;

                _ctx.Tabelas.Update(time);
                _ctx.SaveChanges();
            }
        }

        public void Empatar(List<Tabela> times, int golsMarcados, int golsSofridos)
        {
            foreach (var time in times)
            {
                time.Empates += 1;
                time.Pontos += 1;
                time.Gols_marcados += golsMarcados;
                time.Gols_contra += golsSofridos;

                _ctx.Tabelas.Update(time);
                _ctx.SaveChanges();
            }
        }
    }
}
