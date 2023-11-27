import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Campeonato } from "src/app/models/CampeonatoModel";
import { Confronto } from "src/app/models/ConfrontoModels";
import { Time } from "src/app/models/TimeModel";

@Component({
  selector: "app-confronto-cadastrar",
  templateUrl: "./confronto-cadastrar.component.html",
  styleUrls: ["./confronto-cadastrar.component.css"],
})
export class ConfrontoCadastrarComponent {
  timeCasaId: number = 0;
  timeForaId: number = 0;
  times: Time[] = [];
  campeonatoId: number = 0;
  campeonatos: Campeonato[] = [];
  gols_time_casa: number = 0;
  gols_time_fora: number = 0;

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.client
      .get<Time[]>("https://localhost:7021/api/time/listar")
      .subscribe({
        //A requição funcionou
        next: (times) => {
          this.times = times;
          console.table(this.times)

        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });

      this.client
      .get<Campeonato[]>("https://localhost:7021/api/campeonato/listar")
      .subscribe({
        //A requição funcionou
        next: (campeonatos) => {
          this.campeonatos = campeonatos;
          console.table(this.campeonatos);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }

  cadastrar(): void {
    let confronto: Confronto = {
      TimeCasaId: this.timeCasaId,
      TimeForaId: this.timeForaId,
      CampeonatoId: this.campeonatoId,
      Gols_time_casa: this.gols_time_casa,
      Gols_time_fora: this.gols_time_fora
    };

    this.client
      .post<Confronto>("https://localhost:7021/api/confronto/cadastrar", confronto)
      .subscribe({
        //A requição funcionou
        next: (confronto) => {
          this.snackBar.open("Confronto cadastrado com sucesso!", "CampManager", {
            duration: 1500,
            horizontalPosition: "right",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/confronto/listar"]);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
