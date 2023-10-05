using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.DTOs;
using Microsoft.EntityFrameworkCore;

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

                string retorno = $"Campeonato Cadastrado:\n\nNome {campeonato.Nome}\nPremiação: R${campeonato.Premiacao}";
                _ctx.Campeonatos.Add(campeonato);
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

            List<Campeonato> campeonatos = _ctx.Campeonatos.ToList();

            string retorno = "Campeonato(s) Listado(s):\n\n";

            if (campeonatos.Count() != 0)
            {
                foreach (var campeonato in campeonatos)
                {
                    retorno += $"Nome: {campeonato.Nome}\nPremiação: R${campeonato.Premiacao}\n\n";
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
                foreach (Campeonato CampeonatoCadastrado in _ctx.Campeonatos.ToList())
                {
                    if (CampeonatoCadastrado.CampeonatoId == id)
                    {
                        string retorno = $"Campeonato Consultado:\n\nNome: {CampeonatoCadastrado.Nome}\nPremiação: R${CampeonatoCadastrado.Premiacao}";
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

        public IActionResult Atualizar([FromBody] Campeonato campeonato, [FromRoute] int id)
        {
            try
            {

                Campeonato? campeonatoEncontrado = _ctx.Campeonatos.FirstOrDefault(x => x.CampeonatoId == id);

                if (campeonatoEncontrado != null)
                {
                    campeonatoEncontrado.Nome = campeonato.Nome;
                    campeonatoEncontrado.Premiacao = campeonato.Premiacao;
                    string retorno = $"Campeonato Atualizado:\n\nNome: {campeonatoEncontrado.Nome}\nPremiação: R${campeonatoEncontrado.Premiacao}";
                    _ctx.Campeonatos.Update(campeonatoEncontrado);
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

                    string retorno = $"Análise do {campeonatoNome}:\n\n";

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
                        retorno += $"{classificacao}º {time.TimeNome} - {time.Pontos} Ponto(s)\n";
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

    }
}