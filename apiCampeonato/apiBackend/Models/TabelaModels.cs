namespace ApiBackend.Models;

public class Tabela
{
    public int TabelaId{ get; set; }
    public Campeonato? Campeonato { get; set; }
    public int CampeonatoId { get; set; }
    public Time? Time { get; set; }
    public int TimeId { get; set; }
    public int Pontos{ get; set; }
    public int Gols_marcados{ get; set; }
    public int Gols_contra{ get; set; }
    public int Vitorias{ get; set; }
    public int Empates{ get; set; }
    public int Derrotas{ get; set; }

}