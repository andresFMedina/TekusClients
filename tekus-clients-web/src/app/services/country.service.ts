import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Country } from 'src/models/country.model';
import { PagedResponse} from 'src/models/response.interface';

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
