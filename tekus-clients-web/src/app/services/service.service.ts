import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Country } from 'src/models/country.model';
import { Service } from 'src/models/service.model';

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
