import { Component } from '@angular/core';
import { ClientService } from './services/client.service';
import { ServiceService } from './services/service.service';
import { CountryService } from './services/country.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(
    private clientService: ClientService,
    private serviceService: ServiceService,
    private countryService: CountryService
  ) {

  }
}
