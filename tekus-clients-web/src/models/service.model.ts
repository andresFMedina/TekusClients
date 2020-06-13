import { Country } from './country.model';

export interface Service {
    id: number;
    name: string;
    price: number;
    providerId: number;
    countries?: Country[];
}
