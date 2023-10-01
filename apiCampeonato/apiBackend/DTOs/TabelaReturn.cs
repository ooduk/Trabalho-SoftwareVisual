namespace ApiBackend.DTOs;

public class TabelaReturn
{
    public int TabelaId { get; set; }    
    public string TimeNome { get; set; }
    public string CampeonatoNome { get; set; }  
    public int Pontos{ get; set; } 
    public int Gols_marcados{ get; set; }
    public int Gols_contra{ get; set; }
    public int Vitorias{ get; set; }
    public int Empates{ get; set; }
    public int Derrotas{ get; set; }
}