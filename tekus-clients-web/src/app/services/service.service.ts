import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Service } from 'src/models/service.model';
import { PagedResponse} from 'src/models/response.interface';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  urlApi = `${environment.url}Service`;

  constructor(
    private httpClient: HttpClient
  ) { }

  getServices(filter?: string, page: number = 1) {
    return this.httpClient.get<PagedResponse<Service>>(this.urlApi);
  }
}
