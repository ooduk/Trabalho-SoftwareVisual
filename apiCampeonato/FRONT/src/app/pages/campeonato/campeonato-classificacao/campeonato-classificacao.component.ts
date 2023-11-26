import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Campeonato } from '../../../models/CampeonatoModel';
import { Tabela } from "src/app/models/TabelaModels";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-campeonato-classificacao",
  templateUrl: "./campeonato-classificacao.component.html",
  styleUrls: ["./campeonato-classificacao.component.css"],
})
export class CampeonatoClassificacaoComponent {
  colunasTabela: string[] = [
    "classificacao",
    "timeNome",
    "pontos"
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
                `https://localhost:7021/api/campeonato/classificacao/${id}`
              )
              .subscribe({
                next: (tabelas) => {
                  console.table(tabelas)
                  this.tabelas = tabelas;
                  this.tabelas.forEach((time, index) => {
                    time.classificacao = index + 1;
                  });
                },
                error: (erro) => {
                  console.log(erro);
                },
              });
          },
    });
  }

}
