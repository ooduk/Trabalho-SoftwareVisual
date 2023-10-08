namespace apiBackend.Controllers
{
    using ApiBackend.Data;
    using ApiBackend.Models;
    using Microsoft.AspNetCore.Mvc;
    using ApiBackend.DTOs;
    using Microsoft.EntityFrameworkCore;

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

                string retorno = $"Campeonato Cadastrado:\n\nNome {campeonato.Nome}\nPremiação: R${string.Format("{0:N}", campeonato.Premiacao)}";
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
            List<Campeonato> campeonatos = _ctx.Campeonatos.ToList();

            try
            {
                if (campeonatos.Count() != 0)
                {
                    string retorno = "Campeonato(s) Listado(s):\n\n";

                    foreach (var campeonato in campeonatos)
                    {
                        retorno += $"Nome: {campeonato.Nome}\nPremiação: R${string.Format("{0:N}", campeonato.Premiacao)}\n\n";
                    }
                    return Ok(retorno);
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
                Campeonato? CampeonatoCadastrado = _ctx.Campeonatos.FirstOrDefault(c => c.CampeonatoId == id);

                if (CampeonatoCadastrado != null)
                {
                    string retorno = $"Campeonato Consultado:\n\nNome: {CampeonatoCadastrado.Nome}\nPremiação: R${string.Format("{0:N}", CampeonatoCadastrado.Premiacao)}";
                    return Ok(retorno);
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

                    string retorno = $"Campeonato Atualizado:\n\nNome: {campeonatoEncontrado.Nome}\nPremiação: R${string.Format("{0:N}", campeonatoEncontrado.Premiacao)}";
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
                Campeonato? Campeonato = _ctx.Campeonatos.Find(id);

                if (Campeonato != null)
                {
                    _ctx.Campeonatos.Remove(Campeonato);
                    _ctx.SaveChanges();

                    return Ok("Campeonato Deletado!");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("analisar/{id}")]
        public IActionResult Analisar([FromRoute] int id)
        {
            try
            {
                Campeonato? campeonato = _ctx.Campeonatos.Find(id);

                if (campeonato != null)
                {
                    List<TabelaReturn> times = _ctx.Tabelas
                    .Where(t => t.CampeonatoId == id)
                    .Include(t => t.Time)
                    .Include(c => c.Campeonato)
                    .Select(t => new TabelaReturn
                    {
                        TimeNome = t.Time.Nome,
                        CampeonatoNome = t.Campeonato.Nome,
                        Pontos = t.Pontos,
                        Gols_marcados = t.Gols_marcados,
                        Gols_contra = t.Gols_contra,
                        Vitorias = t.Vitorias,
                        Empates = t.Empates,
                        Derrotas = t.Derrotas
                    })
                    .ToList();

                    if (times.Count() != 0)
                    {
                        var campeonatoNome = times.First().CampeonatoNome;

                        string retorno = $"Análise: {campeonatoNome}\n\n";

                        var timeVitorias = times.OrderByDescending(t => t.Vitorias).First().TimeNome;
                        var timeDerrotas = times.OrderByDescending(t => t.Derrotas).First().TimeNome;
                        var timeEmpates = times.OrderByDescending(t => t.Empates).First().TimeNome;
                        var timeMaisGolsMarcados = times.OrderByDescending(t => t.Gols_marcados).First().TimeNome;
                        var timeMenosGolsMarcados = times.OrderByDescending(t => t.Gols_marcados).Last().TimeNome;
                        var timeMaisGolsSofridos = times.OrderByDescending(t => t.Gols_contra).First().TimeNome;
                        var timeMenosGolsSofridos = times.OrderByDescending(t => t.Gols_contra).Last().TimeNome;

                        retorno += $"Time com Mais Vitórias: {timeVitorias}\nTime com Mais Derrotas: {timeDerrotas}\nTime com Mais Empates: {timeEmpates}\nTime com Mais Gols Marcados: {timeMaisGolsMarcados}\nTime com Menos Gols Marcados: {timeMenosGolsMarcados}\nTime com Mais Gols Sofridos: {timeMaisGolsSofridos}\nTime com Menos Gols Sofridos: {timeMenosGolsSofridos}";

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

        [HttpGet, Route("classificacao/{id}")]
        public IActionResult Classificacao([FromRoute] int id)
        {
            try
            {
                List<TabelaReturn> times = _ctx.Tabelas
                .Where(t => t.CampeonatoId == id)
                .Include(t => t.Time)
                .Include(c => c.Campeonato)
                .OrderByDescending(t => t.Pontos)
                .Select(t => new TabelaReturn
                {
                    TimeNome = t.Time.Nome,
                    CampeonatoNome = t.Campeonato.Nome,
                    Pontos = t.Pontos,
                })
                .ToList();

                if (times.Count() != 0)
                {
                    var campeonatoNome = times.First().CampeonatoNome;

                    string retorno = $"Classificação: {campeonatoNome}\n\n";

                    int classificacao = 1;

                    foreach (var time in times)
                    {
                        retorno += $"{classificacao}º {time.TimeNome} - {time.Pontos} Pontos\n";

                        classificacao++;
                    }
                    return Ok(retorno);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("classificacaoDetalhes/{id}")]
        public IActionResult ClassificacaoDetalhes([FromRoute] int id)
        {
            try
            {
                List<TabelaReturn> times = _ctx.Tabelas
                .Where(t => t.CampeonatoId == id)
                .Include(t => t.Time)
                .Include(c => c.Campeonato)
                .OrderByDescending(t => t.Pontos)
                .Select(t => new TabelaReturn
                {
                    TimeNome = t.Time.Nome,
                    CampeonatoNome = t.Campeonato.Nome,
                    Pontos = t.Pontos,
                    Gols_marcados = t.Gols_marcados,
                    Gols_contra = t.Gols_contra,
                    Vitorias = t.Vitorias,
                    Empates = t.Empates,
                    Derrotas = t.Derrotas
                })
                .ToList();

                if (times.Count() != 0)
                {
                    var campeonatoNome = times.First().CampeonatoNome;

                    string retorno = $"Classificação Detalhada: {campeonatoNome}\n\n";

                    int classificacao = 1;

                    foreach (var time in times)
                    {
                        retorno += $"Classificação: {classificacao}º\nTime: {time.TimeNome}\nPontos: {time.Pontos}\nGols Marcados: {time.Gols_marcados}\nGols Contra: {time.Gols_contra}\nVitórias: {time.Vitorias}\nDerrotas: {time.Derrotas}\nEmpates: {time.Empates}\n\n";

                        classificacao++;
                    }
                    return Ok(retorno);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("finalizar/{id}")]
        public IActionResult Finalizar([FromRoute] int id)
        {
            try
            {
                Campeonato? campeonato = _ctx.Campeonatos.Find(id);
                
                List<TabelaReturn> times = _ctx.Tabelas
                .Where(t => t.CampeonatoId == id)
                .Include(t => t.Time)
                .Include(c => c.Campeonato)
                .OrderByDescending(t => t.Pontos)
                .Take(3)
                .Select(t => new TabelaReturn
                {
                    TimeNome = t.Time.Nome,
                    CampeonatoNome = t.Campeonato.Nome,
                    Pontos = t.Pontos,
                })
                .ToList();

                if((campeonato == null) || (times.Count == 0)){
                    return NotFound();
                }

                var campeonatoNome = times.First().CampeonatoNome;
                string retorno = $"Classificação Final: {campeonatoNome}\n\n";

                var ganhador = times.First();
                retorno += $"Primeiro colocado: {ganhador.TimeNome} com {ganhador.Pontos} ponto(s)!\n\n";

                var ultimoColocado = times.Last();
                retorno += $"Último colocado: {ultimoColocado.TimeNome} com {ultimoColocado.Pontos} ponto(s)!\n\n";

                _ctx.Campeonatos.Remove(campeonato);
                _ctx.SaveChanges();

                retorno += "Campeonato Finalizado!";
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}