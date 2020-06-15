import { ServiceCountry } from './service-country';

export interface Service {
    id: number;
    name: string;
    price: number;
    providerId: number;
    serviceCountries: ServiceCountry[];
}

