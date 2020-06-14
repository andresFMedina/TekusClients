import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Client } from 'src/models/client.model';
import { PagedResponse} from 'src/models/response.interface';


@Injectable({
  providedIn: 'root'
})
export class ClientService {

  urlApi = `${environment.url}Client`;

  constructor(
    private httpClient: HttpClient
  ) { }

  getClients(filter?: string, page: number = 1) {
    return this.httpClient.get<PagedResponse<Client>>(this.urlApi);
  }

}
