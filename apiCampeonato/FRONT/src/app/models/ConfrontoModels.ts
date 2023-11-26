import { Time } from "@angular/common";
import { Campeonato } from "./CampeonatoModel";

export interface Confronto {
  ConfrontoId?: number;
  TimeCasaId: number;
  TimeCasa?: Time;
  TimeForaId: number;
  TimeFora?: Time;
  CampeonatoId: number;
  campeonato?: Campeonato;
  Gols_time_casa: number;
  Gols_time_fora: number;
}
