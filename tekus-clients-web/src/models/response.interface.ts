interface Response {
  message: string;
  didError: boolean;
  errorMessge: string;
}

interface SingleResponse<T> extends Response {
  model: T;
}
interface ListResponse<T> extends Response {
  model: T[];
}

interface PagedResponse<T> extends ListResponse<T> {
  currentPage: number;
  registerPerPages: number;
  totalRegister: number;
  totalPages: number;
  currentFilter: string;
}
