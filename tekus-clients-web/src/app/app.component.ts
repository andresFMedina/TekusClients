import { Component } from '@angular/core';
import { ClientService } from './services/client.service';
import { ServiceService } from './services/service.service';
import { CountryService } from './services/country.service';
import { Service } from 'src/models/service.model';
import { Client } from 'src/models/client.model';
import { Country } from 'src/models/country.model';
import { PagedResponse } from 'src/models/response.interface';
import { RestoreService } from './services/restore.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  clients: PagedResponse<Client>;
  services: PagedResponse<Service>;
  countries: PagedResponse<Country>;



  constructor(
    private clientService: ClientService,
    private serviceService: ServiceService,
    private countryService: CountryService,
    private restoreService: RestoreService
  )
  {
    this.getData();
  }

  restoreDatabase() {
    this.restoreService.restoreDatabase().subscribe((response) => {
      this.getData();
    }, (error) => {
      console.error(error);
    });
  }

  getData() {
    this.clientService.getClients().subscribe((response) => {
      this.clients = response;
    },
      (error) => {
        console.log(error);
      });
    this.serviceService.getServices().subscribe((response) => {
      this.services = response;
    },
      (error) => {
        console.log(error);
      });
    this.countryService.getCountries().subscribe((response) => {
      this.countries = response;
    },
      (error) => {
        console.log(error);
      });
  }
}
