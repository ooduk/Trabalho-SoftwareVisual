import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRoute, Router } from "@angular/router";
import { Time } from "src/app/models/TimeModel";

@Component({
  selector: "app-time-alterar",
  templateUrl: "./time-alterar.component.html",
  styleUrls: ["./time-alterar.component.css"],
})
export class TimeAlterarComponent {
  timeId?: number = 0;
  nome: string = "";

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
          .get<Time>(
            `https://localhost:7021/api/time/consultar/${id}`
          )
          .subscribe({
            next: (time) => {
              this.client
                .get<Time[]>(
                  "https://localhost:7021/api/time/listar"
                )
                .subscribe({
                  next: (times) => {
                    this.timeId = time.timeId;
                    this.nome = time.nome;
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
    let time: Time = {
      nome: this.nome,
    };

    console.log(time);

    this.client
      .put<Time>(
        `https://localhost:7021/api/time/atualizar/${this.timeId}`,
        time
      )
      .subscribe({
        // A requisição funcionou
        next: (time) => {
          this.snackBar.open(
            "Time alterado com sucesso!!",
            "CampManager",
            {
              duration: 1500,
              horizontalPosition: "right",
              verticalPosition: "top",
            }
          );
          this.router.navigate(["pages/time/listar"]);
        },
        // A requisição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
