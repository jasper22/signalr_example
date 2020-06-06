import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { SignalRBaseService } from '../services/signal-base.service';
import { HomeComponent } from './components/home/home.component';
import { HttpClientModule } from '@angular/common/http';

/**
 * Very default NgModule
 * It only publish SignalRBaseService
 */
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [SignalRBaseService],
  bootstrap: [AppComponent]
})
export class AppModule { }
