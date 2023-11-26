import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Campeonato } from './../../../models/CampeonatoModel';

@Component({
  selector: "app-campeonato-listar",
  templateUrl: "./campeonato-listar.component.html",
  styleUrls: ["./campeonato-listar.component.css"],
})
export class CampeonatoListarComponent {
  colunasTabela: string[] = [
    "nome",
    "premiacao",
    "deletar",
    "alterar",
    "classificacao",
    "classificacaoDetalhada",
    "analisar"
  ];
  campeonatos: Campeonato[] = [];

  constructor(
    private client: HttpClient,
    private snackBar: MatSnackBar
  ) {
    
  }

  ngOnInit(): void {
    this.client
      .get<Campeonato[]>("https://localhost:7021/api/campeonato/listar")
      .subscribe({
        next: (campeonatos: Campeonato[]) => {
          console.table(campeonatos);
          this.campeonatos = campeonatos;
        },
        error: (erro: any) => {
          console.log(erro);
        },
      });
  }

  deletar(campeonatoId: number) {
    this.client
      .delete<Campeonato[]>(
        `https://localhost:7021/api/campeonato/deletar/${campeonatoId}`
      )
      .subscribe({
        next: (campeonatos: Campeonato[]) => {
          this.campeonatos = campeonatos;
          this.snackBar.open(
            "Campeonato deletado com sucesso!!",
            "CampManager",
            {
              duration: 1500,
              horizontalPosition: "right",
              verticalPosition: "top",
            }
          );
        },
        error: (erro: any) => {
          console.log(erro);
        },
      });
  }
}
