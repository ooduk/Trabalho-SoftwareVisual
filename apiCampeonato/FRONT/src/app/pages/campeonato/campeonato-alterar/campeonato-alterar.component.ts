import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRoute, Router } from "@angular/router";
import { Campeonato } from './../../../models/CampeonatoModel';

@Component({
  selector: "app-campeonato-alterar",
  templateUrl: "./campeonato-alterar.component.html",
  styleUrls: ["./campeonato-alterar.component.css"],
})
export class CampeonatoAlterarComponent {
  campeonatoId: number = 0;
  nome: string = "";
  premiacao: string = "";

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (parametros) => {
        let { id } = parametros;
        this.client
          .get<Campeonato>(
            `https://localhost:7195/api/campeonato/consultar/${id}`
          )
          .subscribe({
            next: (campeonato) => {
              this.client
                .get<Campeonato[]>(
                  "https://localhost:7195/api/campeonato/listar"
                )
                .subscribe({
                  next: (campeonatos) => {
                    this.campeonatoId = campeonato.CampeonatoId;
                    this.nome = campeonato.Nome;
                    this.premiacao = campeonato.Premiacao;
                  },
                  error: (erro) => {
                    console.log(erro);
                  },
                });
            },
            // Requisição com erro
            error: (erro) => {
              console.log(erro);
            },
          });
      },
    });
  }

  alterar(): void {
    let campeonato: Campeonato = {
      Nome: this.nome,
      Premiacao: this.premiacao,
      CampeonatoId: 0
    };

    console.log(campeonato);

    this.client
      .put<Campeonato>(
        `https://localhost:7195/api/campeonato/atualizar/${this.campeonatoId}`,
        campeonato
      )
      .subscribe({
        // A requisição funcionou
        next: (campeonato) => {
          this.snackBar.open(
            "Campeonato alterado com sucesso!!",
            "E-commerce",
            {
              duration: 1500,
              horizontalPosition: "right",
              verticalPosition: "top",
            }
          );
          this.router.navigate(["pages/campeonato/listar"]);
        },
        // A requisição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
