import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatListModule } from "@angular/material/list";
import { MatTableModule } from "@angular/material/table";
import { MatCardModule } from "@angular/material/card";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSnackBarModule } from "@angular/material/snack-bar";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {TimeAlterarComponent } from './pages/time/time-alterar/time-alterar.component';
import { TimeCadastrarComponent } from "./pages/time/time-cadastrar/time-cadastrar.component";
import { TimeListarComponent} from "./pages/time/time-listar/time-listar.component";
import { CampeonatoListarComponent} from "./pages/campeonato/campeonato-listar/campeonato-listar.component";
import { CampeonatoCadastrarComponent} from "./pages/campeonato/campeonato-cadastrar/campeonato-cadastrar.component";
import { CampeonatoAlterarComponent} from "./pages/campeonato/campeonato-alterar/campeonato-alterar.component";
// import { TabelaCadastrarComponent} from "./pages/tabela/tabela-cadastrar/tabela-cadastrar.component";
// import { ConfrontoAlterarComponent} from "./pages/confronto/confronto-alterar/confronto-alterar.component";
// import { ConfrontoCadastrarComponent} from "./pages/confronto/confronto-cadastrar/confronto-cadastrar.component";
// import { ConfrontoListarComponent} from "./pages/confronto/confronto-listar/confronto-listar.component";
// import { TabelaListarComponent} from "./pages/tabela/tabela-listar/tabela-listar.component";
// import { TabelaAlterarComponent} from "./pages/tabela/tabela-alterar/tabela-alterar.component";
@NgModule({
  declarations: [
    AppComponent,
    TimeListarComponent,
    TimeCadastrarComponent,
    TimeAlterarComponent,
    CampeonatoAlterarComponent,
    CampeonatoCadastrarComponent,
    CampeonatoListarComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatTableModule,
    MatCardModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
