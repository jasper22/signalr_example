import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { SignalRBaseService } from 'src/services/signal-base.service';
import { environment } from '../../../environments/environment';

/**
 * HomeComponent will actually use SignalR
 */
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  /**
   * Array of strings that hold emitted messages from server
   */
  public messages: string[] = [];

  /**
   * Constructor
   * @param signalR Service that implement low-level details of SignalR
   * @param httpClient Angular http-client
   */
  constructor(private signalR: SignalRBaseService, private httpClient: HttpClient) {
  }

  /**
   * Function will respond to 'component-init' in Component LifeCycle event hooks
   * Function will:
   *  1. Run command to connect to server SignalR
   *  2. Subscribe to RX Js subject that emit messages received via SignalR from server
   *  3. Send request to server function that will start SignalR messages emit process
   */
  ngOnInit(): void {
    this.signalR.connect();
    this.subscribeToData();
    this.startDataReceive();
  }

  /**
   * Function will subscribe to RX Js Subject from SignalRBaseService service
   */
  subscribeToData(): void {
    this.signalR.messagesStream$.subscribe(
      (receivedData) => {
        this.messages.push(receivedData.param1 + ' ' + receivedData.param2)
      },
      (err: any) => { console.log(`Error occurred ! ${JSON.stringify(err)}`)},
      () =>         { console.log("SignalR completed !")}
    );
  }

  /**
   * Function will trigger server side SignalR by sending GET request to predefined endpoint
   */
  startDataReceive(): void {
    this.httpClient.get(environment.serverAddress).subscribe(res => {
      console.log(` Received response to get command: ${res}`);
    });
  }
}
