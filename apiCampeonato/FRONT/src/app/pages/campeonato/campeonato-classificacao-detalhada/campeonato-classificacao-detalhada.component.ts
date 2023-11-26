import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Tabela } from "src/app/models/TabelaModels";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-campeonato-classificacao-detalhada",
  templateUrl: "./campeonato-classificacao-detalhada.component.html",
  styleUrls: ["./campeonato-classificacao-detalhada.component.css"],
})
export class CampeonatoClassificacaoDetalhadaComponent {
  colunasTabela: string[] = [
    "tabelaId",
    "timeNome",
    "pontos",
    "gols_marcados",
    "gols_contra",
    "vitorias",
    "empates",
    "derrotas"
  ];
  tabelas: Tabela[] = [];

  constructor(
    private client: HttpClient,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {
    
  }

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (parametros) => {
        let { id } = parametros;  
            this.client
              .get<Tabela[]>(
                `https://localhost:7021/api/campeonato/classificacaoDetalhes/${id}`
              )
              .subscribe({
                next: (tabelas) => {
                  console.table(tabelas)
                  this.tabelas = tabelas;
                },
                error: (erro) => {
                  console.log(erro);
                },
              });
          },
    });
  }

}
