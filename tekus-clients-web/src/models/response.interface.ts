export interface Response {
  message: string;
  didError: boolean;
  errorMessge: string;
}

export interface SingleResponse<T> extends Response {
  model: T;
}
export interface ListResponse<T> extends Response {
  model: T[];
}

export interface PagedResponse<T> extends ListResponse<T> {
  currentPage: number;
  registerPerPages: number;
  totalRegister: number;
  totalPages: number;
  currentFilter: string;
}
