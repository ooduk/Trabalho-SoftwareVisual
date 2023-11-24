import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Time } from "src/app/models/TimeModel";

@Component({
  selector: "app-time-listar",
  templateUrl: "./time-listar.component.html",
  styleUrls: ["./time-listar.component.css"],
})
export class TimeListarComponent {
  colunasTabela: string[] = [
    "id",
    "nome"
  ];
  times: Time[] = [];

  constructor(
    private client: HttpClient,
    private snackBar: MatSnackBar
  ) {
    
  }

  ngOnInit(): void {
    this.client
      .get<Time[]>("https://localhost:7021/api/time/listar")
      .subscribe({
      
        next: (times: Time[]) => {
          console.table(times);
          this.times = times;
        },
    
        error: (erro: any) => {
          console.log(erro);
        },
      });
  }

  deletar(id: number) {
    this.client
      .delete<Time[]>(
        `https://localhost:7021/api/time/deletar/${id}`
      )
      .subscribe({
     
        next: (times: Time[]) => {
          console.log(id)
          this.times = times;
          this.snackBar.open(
            "Time deletado com sucesso!",
            "Times",
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
