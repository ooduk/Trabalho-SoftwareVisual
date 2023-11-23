import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Tabela } from "src/app/models/TabelaModels";
import { Time } from "src/app/models/TimeModel";
import { Campeonato } from './../../../models/CampeonatoModel';

@Component({
  selector: "app-tabela-cadastrar",
  templateUrl: "./tabela-cadastrar.component.html",
  styleUrls: ["./tabela-cadastrar.component.css"],
})
export class TabelaCadastrarComponent {
  
  timeId: number = 0;
  times: Time[] = [];
  CampeonatoId: number = 0;
  campeonatos: Campeonato[] = [];
  pontos: number = 0;
  gols_marcados: number = 0;
  gols_contra: number = 0;
  vitorias: number = 0;
  empates: number = 0;
  derrotas: number = 0;
  tabelas: Tabela[] = [];

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  cadastrar(): void {
    let tabela = new Tabela();
    tabela.CampeonatoId = this.CampeonatoId;
    tabela.TimeId = this.timeId;
    tabela.Pontos = this.pontos;
    tabela.Gols_marcados = this.gols_marcados;
    tabela.Gols_contra = this.gols_contra;
    tabela.Vitorias = this.vitorias;
    tabela.Empates = this.empates;
    tabela.Derrotas = this.derrotas;

    this.client
      .post<Tabela>(
        "https://localhost:7195/api/tabela/cadastrar",
        tabela
      )
      .subscribe({
        next: (tabela: Tabela) => {
          console.log('Resposta do servidor:', tabela);
          this.snackBar.open(
            `Tabela ${tabela.nome} cadastrada com sucesso!!`,
            "E-commerce",
            {
              duration: 1500,
              horizontalPosition: "right",
              verticalPosition: "top",
            }
          );
          this.router.navigate(["pages/tabela/listar"]);
        },
        error: (erro: any) => {
          console.error('Erro na requisição:', erro);
        },
      });
  }
}