import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

/**
 * Very easy routing - we have only 1 component
 */
const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeComponent, children: []},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
