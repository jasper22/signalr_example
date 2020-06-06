import { Injectable, OnInit, OnDestroy } from '@angular/core';

import * as signalR  from '@microsoft/signalr'

import { Subject } from 'rxjs';
import { ServerMesage } from 'src/data/server-mesage';

import { environment } from '../../src/environments/environment';

/**
 * This service will actually use server side SignalR
 */
@Injectable({
  providedIn: 'root'
})
export class SignalRBaseService implements OnDestroy {

    /**
     * This RxJs Subject will emit new messages received via SignalR from server
     */
    public messagesStream$: Subject<ServerMesage> = new Subject<ServerMesage>();

    /**
     * Actual connection to SignalR on server
     */
    private connection: signalR.HubConnection;

    /**
     * Empty constrcutor
     */
    constructor() {
    }

    /**
     * Function will connect to server SignalR
     * It uses 'environment.signalRHubAddress' URL to know where to connect
     * In case connection fail the error will be printed to consule
     */
     connect(): void {
      this.connection = new signalR.HubConnectionBuilder()
                                  .configureLogging(signalR.LogLevel.Debug)
                                  .withUrl(environment.signalRHubAddress)
                                  .withAutomaticReconnect()
                                  .build();

      this.connection.start()
                    .then(function() {console.log("SignalR started"); })
                    .catch(function(err) {console.error(`Could not start SignalR server. Error is: ${err}`)});

      // Just a test
      // this.connection.on("NotifyAll", (param1: string, param2: string) => {
      //   this.messagesStream$.next({"param1": param1, "param2": param2});
      // });
    }

    /**
     * When this object get destroyed it should close RX Js subject and unsibscribe all listeners
     * It also stop() connection to server side SignalR
     */
    ngOnDestroy(): void {
      this.messagesStream$.complete();
      this.messagesStream$.unsubscribe();

      this.connection.stop();
    }
  }
