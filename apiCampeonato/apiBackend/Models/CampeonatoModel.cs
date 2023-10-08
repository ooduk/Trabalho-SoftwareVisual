namespace ApiBackend.Models;

public class Campeonato
{

    public int CampeonatoId { get; set; }

    public string? Nome { get; set; }

    public double Premiacao { get; set; }

    public List<Tabela> TabelaModels { get; set; }

}