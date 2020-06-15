import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RestoreService {
  private url = environment.url;

  constructor(
    private httpClient: HttpClient
  ) {}

  restoreDatabase() {
    return this.httpClient.delete(`${this.url}Restore`);
  }
}
