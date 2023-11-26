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
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        public bool Resultado(int idTimeCasa, int idTimeFora, int idCampeonato, int golsCasa, int golsFora)
        {
            try
            {
                if (golsCasa > golsFora)
                {
                    Campeonato? campeonato = _ctx.Campeonatos.Find(idCampeonato);

                    Time? timeVenceu = _ctx.Times.Find(idTimeCasa);

                    Time? timePerdeu = _ctx.Times.Find(idTimeFora);

                    if ((campeonato == null) || (timeVenceu == null) || (timePerdeu == null))
                    {
                        return false;
                    }

                    Tabela? timeVenceuCadastro = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato && t.TimeId == idTimeCasa)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .FirstOrDefault();

                    Tabela? timePerdeuCadastro = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato && t.TimeId == idTimeFora)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .FirstOrDefault();

                    if (timeVenceuCadastro == null)
                    {
                        timeVenceuCadastro = new Tabela
                        {
                            Campeonato = campeonato,
                            Time = timeVenceu
                        };
                        _ctx.Tabelas.Add(timeVenceuCadastro);
                        _ctx.SaveChanges();
                    }

                    if (timePerdeuCadastro == null)
                    {
                        timePerdeuCadastro = new Tabela
                        {
                            Campeonato = campeonato,
                            Time = timePerdeu
                        };
                        _ctx.Tabelas.Add(timePerdeuCadastro);
                        _ctx.SaveChanges();
                    }

                    Vencer(timeVenceuCadastro, golsCasa, golsFora);
                    Perder(timePerdeuCadastro, golsFora, golsCasa);

                    return true;
                }
                else if (golsCasa < golsFora)
                {

                    Campeonato? campeonato = _ctx.Campeonatos.Find(idCampeonato);

                    Time? timeVenceu = _ctx.Times.Find(idTimeFora);

                    Time? timePerdeu = _ctx.Times.Find(idTimeCasa);

                    if ((campeonato == null) || (timeVenceu == null) || (timePerdeu == null))
                    {
                        return false;
                    }

                    Tabela? timeVenceuCadastro = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato && t.TimeId == idTimeFora)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .FirstOrDefault();

                    Tabela? timePerdeuCadastro = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato && t.TimeId == idTimeCasa)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .FirstOrDefault();

                    if (timeVenceuCadastro == null)
                    {
                        timeVenceuCadastro = new Tabela
                        {
                            Campeonato = campeonato,
                            Time = timeVenceu
                        };
                        _ctx.Tabelas.Add(timeVenceuCadastro);
                        _ctx.SaveChanges();
                    }

                     if (timePerdeuCadastro == null)
                    {
                        timePerdeuCadastro = new Tabela
                        {
                            Campeonato = campeonato,
                            Time = timePerdeu
                        };
                        _ctx.Tabelas.Add(timePerdeuCadastro);
                        _ctx.SaveChanges();
                    }

                    Vencer(timeVenceuCadastro, golsFora, golsCasa);
                    Perder(timePerdeuCadastro, golsCasa, golsFora);

                    return true;
                }
                else
                {
                    Campeonato? campeonato = _ctx.Campeonatos.Find(idCampeonato);

                    Time? timeEmpate1 = _ctx.Times.Find(idTimeCasa);

                    Time? timeEmpate2 = _ctx.Times.Find(idTimeFora);

                    if ((campeonato == null) || (timeEmpate1 == null) || (timeEmpate2 == null))
                    {
                        return false;
                    }

                    Tabela? timeEmpateCasa = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato && t.TimeId == idTimeCasa)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .FirstOrDefault();

                    Tabela? timeEmpateFora = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == idCampeonato && t.TimeId == idTimeFora)
                    .Include(t => t.Time)
                    .Include(t => t.Campeonato)
                    .FirstOrDefault();

                    if (timeEmpateCasa == null)
                    {
                        timeEmpateCasa = new Tabela
                        {
                            Campeonato = campeonato,
                            Time = timeEmpate1
                        };
                        _ctx.Tabelas.Add(timeEmpateCasa);
                        _ctx.SaveChanges();
                    }

                     if (timeEmpateFora == null)
                    {
                        timeEmpateFora = new Tabela
                        {
                            Campeonato = campeonato,
                            Time = timeEmpate2
                        };
                        _ctx.Tabelas.Add(timeEmpateFora);
                        _ctx.SaveChanges();
                    }

                    Empatar(timeEmpateFora, golsFora, golsCasa);
                    Empatar(timeEmpateCasa, golsCasa, golsFora);

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Vencer(Tabela time, int golsMarcados, int golsSofridos)
        {
            time.Vitorias += 1;
            time.Pontos += 3;
            time.Gols_marcados += golsMarcados;
            time.Gols_contra += golsSofridos;

            _ctx.Tabelas.Update(time);
            _ctx.SaveChanges();
        }

        public void Perder(Tabela time, int golsMarcados, int golsSofridos)
        {
            time.Derrotas += 1;
            time.Gols_marcados += golsMarcados;
            time.Gols_contra += golsSofridos;

            _ctx.Tabelas.Update(time);
            _ctx.SaveChanges();
        }

        public void Empatar(Tabela time, int golsMarcados, int golsSofridos)
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
