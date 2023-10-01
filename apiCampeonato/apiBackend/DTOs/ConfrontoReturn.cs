namespace ApiBackend.DTOs;

public class ConfrontoReturn
{
    public int ConfrontoId { get; set; }    
    public string TimeCasaNome { get; set; }
    public string TimeForaNome { get; set; }
    public string CampeonatoNome { get; set; }
    public int Gols_time_casa { get; set;}
    public int Gols_time_fora { get; set;} 
}