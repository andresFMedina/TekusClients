import { Service } from './service.model';
import { ServiceCountry } from './service-country';

export interface Country {
    id: number;
    name: string;
    serviceCountries: ServiceCountry[];
}
