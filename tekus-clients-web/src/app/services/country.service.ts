import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Service } from 'src/models/service.model';
import { Country } from 'src/models/country.model';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  urlApi = `${environment.url}Country`;

  constructor(
    private httpClient: HttpClient
  ) { }

  getCountries(filter?: string, page: number = 1) {
    return this.httpClient.get<PagedResponse<Country>>(this.urlApi);
  }
}
