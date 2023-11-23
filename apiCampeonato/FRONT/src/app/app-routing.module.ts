import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TimeListarComponent } from "./pages/time/time-listar/time-listar.component";
import { TimeCadastrarComponent } from "./pages/time/time-cadastrar/time-cadastrar.component";
import { TimeAlterarComponent } from "./pages/time/time-alterar/time-alterar.component";

const routes: Routes = [
  {
    path: "",
    component: TimeListarComponent,
  },
  {
    path: "pages/time/listar",
    component: TimeListarComponent,
  },
  {
    path: "pages/time/cadastrar",
    component: TimeCadastrarComponent,
  },
  {
    path: "pages/time/alterar/:id",
    component: TimeAlterarComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
