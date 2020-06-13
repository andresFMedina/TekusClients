import { Service } from './service.model';

export interface Country {
    id: number;
    name: string;
    services: Service[];
}
